﻿using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Cms.Shell.Extensions;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using EPiServer.Web.Routing;

namespace Forte.EpiImageHotSpots
{
    [EditorDescriptorRegistration(TargetType = typeof(ImageHotSpots))]
    [EditorDescriptorRegistration(TargetType = typeof(ImageHotSpots),
        UIHint = UiHint)]
    public class ImageHotSpotsEditorDescriptor : EditorDescriptor
    {
        private readonly IUrlResolver urlResolver;
        private readonly IContentLoader contentLoader;
        private readonly IContentVersionRepository contentVersionRepository;
        public const string UiHint = "imagehotspots";
        private const string HotSpotsProperty = "imagehotspots/Editor";

        public ImageHotSpotsEditorDescriptor(IUrlResolver urlResolver, 
            IContentLoader contentLoader, 
            IContentVersionRepository contentVersionRepository)
        {
            this.urlResolver = urlResolver;
            this.contentLoader = contentLoader;
            this.contentVersionRepository = contentVersionRepository;
            this.ClientEditingClass = HotSpotsProperty;
        }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);
            
            var owner = metadata.FindOwnerContent();

            if (ContentReference.IsNullOrEmpty(owner.ContentLink))
                return;
            
            var lastVersionReference = contentVersionRepository.List(owner.ContentLink)
                .OrderBy(x => x.Saved)
                .Last();

            var lastVersion = this.contentLoader.Get<ImageHotSpotsBlockBase>(lastVersionReference.ContentLink);

            
            this.AddImageUrl(metadata, lastVersion);
            this.AddBockPoints(metadata, lastVersion);
        }

        private void AddBockPoints(ExtendedMetadata metadata, ImageHotSpotsBlockBase hotSpotsBlock)
        {
            var blocks = hotSpotsBlock.Blocks?.FilteredItems.Select(x => new
            {
                ContentReference = x.ContentLink.ID,
                Name = this.contentLoader.Get<IContent>(x.ContentLink).Name,
                X = hotSpotsBlock.HotSpots?.Value?.FirstOrDefault(h => h.ContentReference == x.ContentLink)?.X,
                Y = hotSpotsBlock.HotSpots?.Value?.FirstOrDefault(h => h.ContentReference == x.ContentLink)?.Y
            });
            
            metadata.EditorConfiguration.Add("blocks", blocks);
        }

        private void AddImageUrl(ExtendedMetadata metadata, ImageHotSpotsBlockBase hotSpotsBlock)
        {
            var url = this.urlResolver.GetUrl(hotSpotsBlock.Image);            
            metadata.EditorConfiguration.Add("imageUrl", url);
        }
    }
}
