using CometHandler.Configurations;
using CometHandler.Interfaces.Repositories;
using CometHandler.Interfaces.Services;
using CometHandler.Jobs;
using CometHandler.Models.Context;
using CometHandler.Repositories;
using CometHandler.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.AspNetCore;

namespace CometHandler.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var configSection = configuration.GetSection(AppConfiguration.Key);

        services.AddHttpClient();

        services.Configure<AppConfiguration>(configSection);
        services.AddScoped<IQueryingService, QueryingService>();
        services.AddScoped<ICometService, CometService>();
        services.AddScoped<ICometRepository, CometRepository>();
        services.AddTransient<CometProcessingJob>();

        services.AddInfrastructure(configuration);

        return services;
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres")))
            .AddScheduler(configuration);
    }

    private static IServiceCollection AddScheduler(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddQuartz(configurator =>
        {
            var jobs = configuration.GetSection(JobConfiguration.Key).Get<IEnumerable<JobConfiguration>>();

            if (jobs is null) throw new ArgumentException("No job configuration specified");

            AddJobs(configurator, jobs);
        }).AddQuartzServer();
    }


    private static void AddJobs(IServiceCollectionQuartzConfigurator quartzConfigurator,
        IEnumerable<JobConfiguration> jobs)
    {
        var dictionary =
            jobs.ToDictionary(configuration => configuration.Name, configuration => configuration.Schedule);

        quartzConfigurator.AddJob<CometProcessingJob>(CometProcessingJob.JobKey)
            .AddTrigger(options =>
            {
                options.ForJob(CometProcessingJob.JobKey)
                    .WithCronSchedule(dictionary[CometProcessingJob.JobKey.Name]);
            });
    }
}