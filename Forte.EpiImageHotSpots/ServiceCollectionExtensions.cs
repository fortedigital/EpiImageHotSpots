using System.Linq;
using EPiServer.Shell.Modules;
using Forte.EpiImageHotSpots.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forte.EpiImageHotSpots;

// ReSharper disable once UnusedType.Global
public static class ServiceCollectionExtensions
{
    
    // ReSharper disable once UnusedMember.Global
    public static IServiceCollection AddForteImageHotSpots(this IServiceCollection services) 
        => services.Configure<ProtectedModuleOptions>(options =>
        {
            if (!options.Items.Any(x => x.Name.Equals(Constants.ModuleName)))
            {
                options.Items.Add(
                    new ModuleDetails
                    {
                        Name = Constants.ModuleName,
                    }
                );
            }
        });
}
