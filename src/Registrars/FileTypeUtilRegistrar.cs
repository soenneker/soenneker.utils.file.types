using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Utils.File.Abstract;
using Soenneker.Utils.File.Registrars;
using Soenneker.Utils.File.Types.Abstract;

namespace Soenneker.Utils.File.Types.Registrars;

/// <summary>
/// A utility library for various operations on specific file types
/// </summary>
public static class FileTypeUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IFileTypeUtil"/> as a scoped service. <para/>
    /// </summary>
    /// <remarks>Also tries to add <see cref="IFileUtil"/> as scoped.</remarks>
    public static IServiceCollection AddFileTypeUtilAsScoped(this IServiceCollection services)
    {
        services.AddFileUtilAsScoped()
            .TryAddScoped<IFileTypeUtil, FileTypeUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IFileTypeUtil"/> as a singleton service. <para/>
    /// </summary>
    /// <remarks>Also tries to add <see cref="IFileUtil"/> as singleton.</remarks>
    public static IServiceCollection AddFileTypeUtilAsSingleton(this IServiceCollection services)
    {
        services.AddFileUtilAsSingleton()
            .TryAddSingleton<IFileTypeUtil, FileTypeUtil>();

        return services;
    }
}