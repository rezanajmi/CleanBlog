using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Events.Category;
using CleanBlog.Application.Events.Post;
using CleanBlog.Application.Events.User;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.Entities.Identity;
using CleanBlog.Infrastructure.Bus;
using CleanBlog.Infrastructure.Caching;
using CleanBlog.Infrastructure.CommandData;
using CleanBlog.Infrastructure.Identity;
using CleanBlog.Infrastructure.Logging.EventSourcing;
using CleanBlog.Infrastructure.QueryData;
using CleanBlog.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanBlog.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CleanBlogDbContext>();

            services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+'#!/^%{}*[]&$?:";
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<CleanBlogDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = false,
                      ValidIssuer = configuration["JwtSettings:Issuer"],
                      ValidateAudience = false,
                      ValidAudience = configuration["JwtSettings:Audience"],
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      RequireSignedTokens = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSettings:Key"]))
                  };
              });

            services.AddSingleton<IEventSourcing, EventStoreLogger>();

            if (configuration.GetValue<bool>("Caching:IsActive") == true)
            {
                services.AddScoped<ICache, InMemoryCache>();
                var cacheDb = configuration.GetValue<string>("Caching:Database");
                if (cacheDb == "InMemory")
                {
                    services.AddMemoryCache();
                }
                else if (cacheDb == "Redis")
                {
                    services.AddDistributedMemoryCache();
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = configuration.GetConnectionString("Redis");
                    });
                }
            }

            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

            services.AddScoped(typeof(IAsyncCommandRepository<,>), typeof(AsyncCommandRepository<,>));

            services.AddSingleton<IAsyncQueryRepository, AsyncQueryRepository>();

            services.AddScoped<ICurrentUser, CurrentUser>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddScoped<IJwtService, JwtService>();

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddSingleton<IBus, RabbitMQBus>();

            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
        }

        public async static void RegisterInfrastructureApps(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var eventBus = scope.ServiceProvider.GetRequiredService<IBus>();

                await eventBus.Subscribe<CategoryCreatedBusEvent>();
                await eventBus.Subscribe<CategoryUpdatedBusEvent>();
                await eventBus.Subscribe<PostCreatedBusEvent>();
                await eventBus.Subscribe<PostDeletedBusEvent>();
                await eventBus.Subscribe<PostUpdatedBusEvent>();
                await eventBus.Subscribe<CommentCreatedBusEvent>();
                await eventBus.Subscribe<UserCreatedBusEvent>();
                await eventBus.Subscribe<UserUpdatedBusEvent>();

                var dbInit = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
                await dbInit.MigrateAsync();
                await dbInit.SeedDataAsync();
            }
        }
    }
}
