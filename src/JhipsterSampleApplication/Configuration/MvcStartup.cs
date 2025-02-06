using JHipsterNet.Web.Pagination.Binders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JhipsterSampleApplication.Configuration;

public static class WebConfiguration
{
    private static bool s_includeBlazor;

    public static void ConfigureWebModule(this IConfiguration configuration)
    {
        s_includeBlazor = configuration.GetValue<bool>("INCLUDE_BLAZOR");
    }

    public static IServiceCollection AddWebModule(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddMvc();

        //https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-2.2
        services.AddHealthChecks();

        services.AddControllers(options => { options.ModelBinderProviders.Insert(0, new PageableBinderProvider()); })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        });


        return services;
    }

    public static IApplicationBuilder UseApplicationWeb(this IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseDefaultFiles();
        if (s_includeBlazor)
        {
            // Only serve blazor files if config INCLUDE_BLAZOR is true in appsettings.json or in environment variable.
            app.UseBlazorFrameworkFiles();
        }
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            if (s_includeBlazor)
            {
                // Only add fallback for blazor's index.html if config INCLUDE_BLAZOR is true in appsettings.json or in environment variable.
                endpoints.MapFallbackToFile("index.html");
            }
        });


        app.UseHealthChecks("/health");


        return app;
    }

}
