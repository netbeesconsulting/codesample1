using Microsoft.Extensions.DependencyInjection;
using Stocky.Application.Service;
using Stocky.Application.Service.Admin;
using Stocky.Application.Service.Admin.Interface;
using Stocky.Application.Service.Security;
using Stocky.Application.Service.Security.Interface;

namespace Stocky.Configurations
{
    public static class IocConfiguration
    {
        public static IServiceCollection ConfigureIoc(this IServiceCollection services)
        {
            services.AddTransient<ISecurityService, SecurtiyService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IBaseServiceInjector, BaseServiceInjector>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentService, PaymentService>();

            return services;
        }
    }
}
