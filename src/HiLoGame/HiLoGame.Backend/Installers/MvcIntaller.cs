using FluentValidation.AspNetCore;
using HiLoGame.Backend.Filters;

namespace HiLoGame.Backend.Installers
{
    /// <inheritdoc/>
    public class MvcIntaller : IInstaller
    {
        /// <inheritdoc/>
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddAutoMapper(typeof(Program));

            services.AddFluentValidationAutoValidation();

            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });
        }
    }
}
