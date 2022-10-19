using EPiServer.Core;

namespace Forte.EpiImageHotSpots;

public class ImageHotSpot
{
    public ContentReference ContentReference { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
}