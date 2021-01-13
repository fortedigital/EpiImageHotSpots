using System;
using EPiServer.Core;
using EPiServer.PlugIn;
using Newtonsoft.Json;

namespace Forte.EpiImageHotSpots
{
    
    [PropertyDefinitionTypePlugIn]
    public class ImageHotSpotsProperty : PropertyLongString
    {
        public override object Value
        {
            get
            {
                if (!(base.Value is string value))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<ImageHotSpots>(value);
            }

            set
            {
                if (value is ImageHotSpots)
                {
                    base.Value = JsonConvert.SerializeObject(value);
                }
                else
                {
                    base.Value = value;
                }
            }
        }


        public override object SaveData(PropertyDataCollection properties)
        {
            return this.LongString;
        }

        public override Type PropertyValueType => typeof(ImageHotSpots);
    }
}
