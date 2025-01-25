namespace Ecommerce.Application.Common.Abstraction.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, BaseResult<TResponse>>
    where TQuery : IQuery<TResponse>;
