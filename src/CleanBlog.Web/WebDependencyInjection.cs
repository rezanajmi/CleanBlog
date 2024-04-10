using CleanBlog.Web.Configurations;
using CleanBlog.Web.Mappings;

namespace CleanBlog.Web;

public static class WebDependencyInjection
{
    public static void AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddApiVersioning(o =>
        {
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            o.ReportApiVersions = true;

            // configs for using versioning by header or queryString
            //o.ApiVersionReader = ApiVersionReader.Combine(
                //new QueryStringApiVersionReader("api-version"),
                //new HeaderApiVersionReader("X-Version"),
                //new MediaTypeApiVersionReader("ver")
                //);

        }).AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddSwaggerGen();
        services.ConfigureOptions<SwaggerConfigs>();

        services.AddCors(builder =>
        {
            builder.AddDefaultPolicy(opt =>
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyHeader();
                opt.AllowAnyMethod();
            });
        });
    }
}