global using System.Data;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
global using Dapper;
global using Ecommerce.Application.Common.Repository;
global using Ecommerce.Application.Common.Services;
global using Ecommerce.Domain.AggregatesModel.AppUserAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.CartAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.CategoryAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.OrderAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.ProductAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.ProductConfirmAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.StoreAggregate.Entities;
global using Ecommerce.Domain.Enumerations;
global using Ecommerce.Infrastructure.Data;
global using Ecommerce.Infrastructure.EntityConfiguration;
global using Ecommerce.Infrastructure.Repository;
global using Ecommerce.Shared.DomainDesign.Abstraction;
global using Ecommerce.Shared.Infrastructure;
global using Humanizer;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.Data.SqlClient;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Polly;
