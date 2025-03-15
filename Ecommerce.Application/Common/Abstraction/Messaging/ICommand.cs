using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Common.Abstraction.Messaging;

public interface ICommand : IRequest<BaseResult>;

public interface ICommand<TResponse> : IRequest<TResponse>;
