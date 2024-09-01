
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System;
using System.Reflection;
using Application.Data;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Implementations.Repositories;
using Application.Implementations.Services;
using Application.Models;
using Microsoft.Extensions.Caching.Memory;
using Hangfire;
using Hangfire.MySql;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMemoryCache, MemoryCache>();
            services.AddScoped<ISystemmoduleService, SystemmodueService>();
            services.AddScoped<ISubmoduleService, SubmoduleService>();
            services.AddScoped<ISystempermissionService,SystempermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleSystemPermissionService, RoleSystemPermissionService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IMaritalStatusService, MaritalStatusService>();
            services.AddScoped<INationalityService, NationalityService>();
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped<ITitleService, TitleService>();
            services.AddScoped<IVisitorService, VisitorService>();
            services.AddScoped<IVisitorNumberService, VisitorNumberService>();
            services.AddScoped<IVisitorLoggerService, VisitorLoggerService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IReportService, ReportService>();

            return services;
        }
        public static IServiceCollection AddPersistanceServicesDependecy(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionstring = configuration.GetConnectionString("DefaultConnection");
            string hangfireConnectionString = configuration.GetConnectionString("QueueDb")!;
            services.AddDbContextFactory<AppDbContext>(options =>
            {
                options.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring));
            });





            services.AddHangfire(config => config
                           .UseSimpleAssemblyNameTypeSerializer()
                           .UseRecommendedSerializerSettings()
                            .UseStorage(
                                new MySqlStorage(
                                    hangfireConnectionString,
                                    new MySqlStorageOptions
                                    {
                                        QueuePollInterval = TimeSpan.FromSeconds(10),
                                        JobExpirationCheckInterval = TimeSpan.FromHours(1),
                                        CountersAggregateInterval = TimeSpan.FromMinutes(5),
                                        PrepareSchemaIfNecessary = true,
                                        DashboardJobListLimit = 25000,
                                        TransactionTimeout = TimeSpan.FromMinutes(1),
                                        TablesPrefix = "Hangfire",
                                    }
                                ))
                            );
            services.AddHangfireServer();
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<ConfigurationSettings>(configuration.GetSection("ConfigurationSettings"));
            services.AddIdentity<ApplicationUser, Role>()
                    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
