using Application.Data;
using Domain.BaseRates;
using Domain.Customers;
using Domain.Invoices;
using Domain.Payments;
using Domain.Primitives;
using Domain.RatesPerCubicMeter;
using Domain.Readings;
using Domain.ServiceAddresses;
using Domain.Services;
using Domain.WaterMeters;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.BaseRates;
using Infrastructure.Persistence.Repositories.Customers;
using Infrastructure.Persistence.Repositories.Invoices;
using Infrastructure.Persistence.Repositories.Payments;
using Infrastructure.Persistence.Repositories.RatesPerCubicMeter;
using Infrastructure.Persistence.Repositories.Readings;
using Infrastructure.Persistence.Repositories.ServiceAddresses;
using Infrastructure.Persistence.Repositories.WaterMeters;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Database")));

            services.AddScoped<IApplicationDbContext>(options => options.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(options => options.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IBaseRateRepository, BaseRateRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IRatePerCubicMeterRepository, RatePerCubicMeterRepository>();
            services.AddScoped<IReadingRepository, ReadingRepository>();
            services.AddScoped<IServiceAddressRepository, ServiceAddressRepository>();
            services.AddScoped<IWaterMeterRepository, WaterMeterRepository>();
            services.AddScoped<IDueDateCalculator, DueDateCalculator>();
            services.AddScoped<IInvoiceCalculationService, InvoiceCalculationService>();
            services.AddScoped<IInvoiceNumberCalculationService, InvoiceNumberCalculationService>();

            return services;
        }
    }
}
