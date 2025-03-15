namespace Ecommerce.Application.Common.Abstraction.Messaging;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>;
