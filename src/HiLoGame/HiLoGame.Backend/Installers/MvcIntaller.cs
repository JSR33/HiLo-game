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

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7190");
                    });
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
