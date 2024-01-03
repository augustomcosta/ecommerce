using System.Text.Json.Serialization;
using ecommerce_api.Data.Context;
using ecommerce_api.Data.Identity;
using ecommerce_api.Data.RepositoriesImpl;
using ecommerce_api.Data.UnityOfWork;
using ecommerce_api.Data.UnityOfWork.Interfaces;
using ecommerce_api.Domain.Repositories;
using ecommerce_api.Domain.Repositories.Interfaces;
using ecommerce_api.Domain.Services;
using ecommerce_api.Domain.Services.Interfaces;
using ecommerce_api.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace ecommerce_api.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        });
        services.AddDbContext<AppIdentityDbContext>(x =>
        {
            x.UseNpgsql(config.GetConnectionString("IdentityConnection"));
        });

        services.AddSingleton<IConnectionMultiplexer>(c => {
            var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
            return ConnectionMultiplexer.Connect(options);
        });
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUnityOfWork, UnityOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse()
                {
                    Errors = errors
                };
        
                return new BadRequestObjectResult(errorResponse);
            };
        });

        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
        });

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
            
        return services;
    }
}