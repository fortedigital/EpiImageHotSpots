using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace Forte.EpiImageHotSpots
{
    [ContentType(GUID = "6D188F84-E2EE-4954-B535-B09309878813")]
    public class ImageHotSpotsBlockBase : BlockData
    {
        [Required]
        [Display(Order = 1)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference Image { get; set; }
        
        [Display(Order = 2)]
        [AllowedTypes(typeof(IImageHotSpotBlock))]
        public virtual ContentArea Blocks { get; set; }
        
        [UIHint(ImageHotSpotsEditorDescriptor.UiHint)]
        [BackingType(typeof(ImageHotSpotsProperty))]
        [Display(Order = 3, Name = "Preview")]
        public virtual ImageHotSpots HotSpots { get; set; }
    }
}
