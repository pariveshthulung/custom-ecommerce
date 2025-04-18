global using System.Reflection;
global using System.Security.Claims;
global using AutoMapper;
global using Ecommerce.Application.Common.Abstraction.Messaging;
global using Ecommerce.Application.Common.Model;
global using Ecommerce.Application.Common.Repository;
global using Ecommerce.Application.Common.Services;
global using Ecommerce.Application.DomainEvents.Base;
global using Ecommerce.Domain.AggregatesModel.AppUserAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.CartAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.CategoryAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.EventAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.EventAggregate.Enumerations;
global using Ecommerce.Domain.AggregatesModel.OrderAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.ProductAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.ProductConfirmAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.StoreAggregate.Entities;
global using Ecommerce.Domain.AggregatesModel.StoreAggregate.Events;
global using Ecommerce.Domain.Enumerations;
global using Ecommerce.Shared.Application.Mapping;
global using Ecommerce.Shared.DomainDesign.Abstraction;
global using Ecommerce.Shared.Wrappers;
global using FluentValidation;
global using MediatR;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
