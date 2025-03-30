namespace Ecommerce.Infrastructure.Backgrounds;

[DisallowConcurrentExecution]
public class ProcessOutBoxMessageJob(
    IPublisher publisher,
    EcommerceDbContext ecommerceDbContext,
    ILogger<ProcessOutBoxMessageJob> logger
) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var messages = await ecommerceDbContext
                .OutBoxMessages.Where(x => x.ProcessedOnUtc == null)
                .Take(20)
                .ToListAsync();
            if (messages.Count == 0)
                return;
            foreach (OutBoxMessage outBoxMessage in messages)
            {
                INotification? domainEvent = null;

                try
                {
                    domainEvent = JsonConvert.DeserializeObject<INotification>(
                        outBoxMessage.EventData,
                        new Newtonsoft.Json.JsonSerializerSettings
                        {
                            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                        }
                    );
                }
                catch (JsonSerializationException e)
                {
                    Console.WriteLine(e.ToString());
                    throw;
                }

                if (domainEvent is null)
                {
                    logger.LogWarning(
                        "Deserialization failed for OutboxMessage {MessageId}",
                        outBoxMessage.Id
                    );
                    return;
                }
                AsyncRetryPolicy retryPolicy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(50 * attempt));

                PolicyResult result = await retryPolicy.ExecuteAndCaptureAsync(
                    () => publisher.Publish(domainEvent, context.CancellationToken)
                );
                outBoxMessage.ProcessedOnUtc = DateTime.UtcNow;
                outBoxMessage.Errors = result.FinalException?.ToString();
                ecommerceDbContext.OutBoxMessages.Update(outBoxMessage);
            }
            await ecommerceDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
