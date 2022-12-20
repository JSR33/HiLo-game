namespace HiLoGame.Backend.Installers
{
    /// <summary>
    /// Service installer extension
    /// </summary>
    public interface IInstaller
    {
        /// <summary>
        /// Install services in assembly
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        void InstallerServices(IServiceCollection services, IConfiguration configuration);
    }
}
