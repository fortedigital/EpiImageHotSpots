using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Core;
using Newtonsoft.Json;

namespace Forte.EpiImageHotSpots
{
    [JsonObject]
    public class ImageHotSpots : IEnumerable<ImageHotSpot>
    {
        [JsonProperty]
        protected List<ImageHotSpot> Value { get; set; }

        public ImageHotSpots()
        {
            this.Value = new List<ImageHotSpot>();
        }

        public ImageHotSpots(IEnumerable<ImageHotSpot> hotspots)
        {
            this.Value = hotspots.ToList();
        }

        public IEnumerator<ImageHotSpot> GetEnumerator()
        {
            return this.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) this.Value).GetEnumerator();
        }
    }

    public class ImageHotSpot
    {
        public ContentReference ContentReference { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
