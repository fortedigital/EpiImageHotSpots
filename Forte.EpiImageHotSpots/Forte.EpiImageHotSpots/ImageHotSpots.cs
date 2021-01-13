using System.Collections.Generic;
using EPiServer.Core;

namespace Forte.EpiImageHotSpots
{
    public class ImageHotSpots
    {
        public List<ImageHotSpot> Value { get; set; }
    }


    public class ImageHotSpot
    {
        public ContentReference ContentReference { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
