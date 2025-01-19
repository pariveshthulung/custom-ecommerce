namespace Ecommerce.Shared.DomainDesign.Extentions;

public interface IRegisterableService;
public interface IScopedService : IRegisterableService;
public interface ITransientService : IRegisterableService;
public interface ISingletonService : IRegisterableService;
