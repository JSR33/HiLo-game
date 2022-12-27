using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace HiLoGame.Backend.Installers
{
    /// <inheritdoc/>
    public class SwaggerInstaller : IInstaller
    {
        /// <inheritdoc/>
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(_ =>
            {
                _.SwaggerDoc("v1", new OpenApiInfo { Title = "HiLoGame.API", Version = "v1" });

                _.ExampleFilters();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                _.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerExamplesFromAssemblyOf<Program>();
        }
    }
}
