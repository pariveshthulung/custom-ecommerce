namespace Ecommerce.Api.Routing;

public class EcommerceRouteAttribute : RouteAttribute
{
    private const string _routePrefix = "api/ecommerce";

    public EcommerceRouteAttribute(string template)
        : base($"{_routePrefix}{template}") { }
}
