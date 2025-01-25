namespace Ecommerce.Application.Common.Abstraction.Messaging;

public interface ICommand : IRequest<BaseResult>;
public interface ICommand<TResponse> : IRequest<BaseResult<TResponse>>;

