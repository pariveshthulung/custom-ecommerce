namespace Ecommerce.Application.Common.Abstraction.Messaging;

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, BaseResult<TResponse>>
    where TCommand : ICommand<TResponse>;
