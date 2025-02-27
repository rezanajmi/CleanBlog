using CleanBlog.Application.Behaviors;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanBlog.Application
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Behaviors for both Commands and Queries
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));

            // Behaviors for Commands
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(EventSourcingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            if (configuration.GetValue<bool>("Caching:IsActive") == true)
            {
                services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            }
        }
    }
}
