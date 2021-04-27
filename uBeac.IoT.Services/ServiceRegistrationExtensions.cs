using uBeac.IoT.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services) 
        {
            services.AddScoped<IFirmwareService, FirmwareService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
