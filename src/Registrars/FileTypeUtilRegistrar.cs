using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Utils.File.Abstract;
using Soenneker.Utils.File.Registrars;
using Soenneker.Utils.File.Types.Abstract;

namespace Soenneker.Utils.File.Types.Registrars;

public static class FileTypeUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IFileTypeUtil"/> as a scoped service. <para/>
    /// </summary>
    /// <remarks>Also tries to add <see cref="IFileUtil"/> as scoped.</remarks>
    public static void AddFileTypeUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IFileTypeUtil, FileTypeUtil>();
        services.AddFileUtilAsScoped();
    }

    /// <summary>
    /// Adds <see cref="IFileTypeUtil"/> as a singleton service. <para/>
    /// </summary>
    /// <remarks>Also tries to add <see cref="IFileUtil"/> as singleton.</remarks>
    public static void AddFileTypeUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IFileTypeUtil, FileTypeUtil>();
        services.AddFileUtilAsSingleton();
    }
}