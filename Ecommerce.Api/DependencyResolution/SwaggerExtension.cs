// using Ecommerce.Api.Routing;
// using Microsoft.OpenApi.Models;
// using Swashbuckle.AspNetCore.SwaggerGen;

// namespace Ecommerce.Api.DependencyResolution;

// public static class SwaggerExtensions
// {
//     public static SwaggerGenOptions AddEcommerceSwaggerGen(this SwaggerGenOptions options)
//     {
//         options.EnableAnnotations();

//         options.SwaggerDoc(
//             ApiGrouping.EcommerceApiGroupingName,
//             new OpenApiInfo
//             {
//                 Version = "v1",
//                 Title = "Market Volatility Predictor",
//                 Description = "Market Volatility Predictor Web API"
//             }
//         );

//         return options;
//     }

//     public static List<SwaggerEndpointDefinition> UseMarketVolatilityPredictorSwaggerEndpoints(
//         this List<SwaggerEndpointDefinition> swaggerEndpointDefinitions
//     )
//     {
//         swaggerEndpointDefinitions.Add(
//             new SwaggerEndpointDefinition(
//                 $"/swagger/{ApiGroupings.MarketVolatilityPredictorApiGroupingsName}/swagger.json",
//                 "Market Volatility Predictor (MVP)"
//             )
//         );

//         return swaggerEndpointDefinitions;
//     }
// }
