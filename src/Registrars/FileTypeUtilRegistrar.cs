using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Utils.File.Types.Abstract;
using Soenneker.Utils.FileSync.Abstract;
using Soenneker.Utils.FileSync.Registrars;

namespace Soenneker.Utils.File.Types.Registrars;

/// <summary>
/// A utility library for various operations on specific file types
/// </summary>
public static class FileTypeUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IFileTypeUtil"/> as a scoped service. <para/>
    /// </summary>
    /// <remarks>Also tries to add <see cref="IFileUtilSync"/> as scoped.</remarks>
    public static void AddFileTypeUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IFileTypeUtil, FileTypeUtil>();
        services.AddFileUtilSyncAsScoped();
    }

    /// <summary>
    /// Adds <see cref="IFileTypeUtil"/> as a singleton service. <para/>
    /// </summary>
    /// <remarks>Also tries to add <see cref="IFileUtilSync"/> as singleton.</remarks>
    public static void AddFileTypeUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IFileTypeUtil, FileTypeUtil>();
        services.AddFileUtilSyncAsSingleton();
    }
}