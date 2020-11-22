using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Jhipster.Configuration {
    public static class SwaggerConfiguration {
        public static IServiceCollection AddSwaggerModule(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v2", new OpenApiInfo {Title = "Jhipster API", Version = "0.0.1"});
            });

            return services;
        }

        public static IApplicationBuilder UseApplicationSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c => {
                c.RouteTemplate = "{documentName}/api-docs";
            });
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/v2/api-docs", "Jhipster API");
            });
            return app;
        }
    }
}
