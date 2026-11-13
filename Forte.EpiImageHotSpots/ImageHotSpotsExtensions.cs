using System.Collections.Generic;
using System.Linq;
using EPiServer.Core;

namespace Forte.EpiImageHotSpots;

public static class ImageHotSpotsExtensions
{
    /// <summary>
    /// Returns only the hot spots whose content reference is still present in <see cref="IImageHotSpotsBlock.Blocks"/>
    /// (compared ignoring version).
    /// </summary>
    public static IEnumerable<ImageHotSpot> GetValidHotSpots(this IImageHotSpotsBlock block)
    {
        if (block?.HotSpots == null)
        {
            return Enumerable.Empty<ImageHotSpot>();
        }

        var blockReferences = block.Blocks?.Items?
            .Select(x => x.ContentLink)
            .Where(x => !ContentReference.IsNullOrEmpty(x))
            .Select(x => x.ToReferenceWithoutVersion())
            .ToHashSet() ?? new HashSet<ContentReference>();

        return block.HotSpots
            .Where(h => !ContentReference.IsNullOrEmpty(h.ContentReference)
                        && blockReferences.Contains(h.ContentReference.ToReferenceWithoutVersion()))
            .ToList();
    }

    /// <summary>
    /// Removes hot spots that point to blocks no longer present in <see cref="IImageHotSpotsBlock.Blocks"/>.
    /// The property is only assigned when something was actually removed.
    /// </summary>
    /// <returns>true if any hot spot was removed.</returns>
    public static bool PruneHotSpots(this IImageHotSpotsBlock block)
    {
        if (block?.HotSpots == null)
        {
            return false;
        }

        var all = block.HotSpots.ToList();
        var valid = block.GetValidHotSpots().ToList();
        if (valid.Count == all.Count)
        {
            return false;
        }

        block.HotSpots = new ImageHotSpots(valid);
        return true;
    }
}
