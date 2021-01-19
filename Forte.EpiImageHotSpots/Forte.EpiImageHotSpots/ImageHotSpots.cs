using System.Collections;
using System.Collections.Generic;
using EPiServer.Core;

namespace Forte.EpiImageHotSpots
{
    public class ImageHotSpots : IEnumerable<ImageHotSpot>
    {
        protected List<ImageHotSpot> Value { get; set; }
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
