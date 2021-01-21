using EPiServer.Core;
using EPiServer.Shell;

namespace Forte.EpiImageHotSpots
{
    public interface IImageHotSpotBlock : IContentData
    {
        
    }
    
    [UIDescriptorRegistration]
    public class IImageHotSpotBlockUIDescriptor : UIDescriptor<IImageHotSpotBlock>
    {
        
    }
}
