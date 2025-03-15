namespace Ecommerce.Shared.Application.Mapping;

public abstract class BaseMappingProfile : Profile
{
    public BaseMappingProfile(Assembly assembly)
    {
        ApplyMappingsFromAssembly(assembly);
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly
            .GetExportedTypes()
            .Where(i =>
                i.GetInterfaces()
                    .Any(i =>
                        i.IsGenericType
                        && (
                            i.GetGenericTypeDefinition() == typeof(IMapFrom<>)
                            || i.GetGenericTypeDefinition() == typeof(IMapTo<>)
                        )
                    )
            );
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo =
                type.GetMethod("Mapping")
                ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping")
                ?? type.GetInterface("IMapTo`1")!.GetMethod("Mapping");

            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}
