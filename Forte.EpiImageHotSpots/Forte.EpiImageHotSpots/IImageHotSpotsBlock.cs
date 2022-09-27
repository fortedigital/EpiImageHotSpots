using EPiServer.Core;

namespace Forte.EpiImageHotSpots;

public interface IImageHotSpotsBlock : IContentData
{
    ContentReference Image { get; set; }
    ContentArea Blocks { get; set; }
    ImageHotSpots HotSpots { get; set; }
}
