namespace HiLoGame.Backend.Installers
{
    /// <inheritdoc/>
    public static class InstallerExtension
    {
        /// <inheritdoc/>
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Program).Assembly.ExportedTypes.Where(x =>
               typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
            installers.ForEach(installer => installer.InstallerServices(services, configuration));
        }
    }
}
