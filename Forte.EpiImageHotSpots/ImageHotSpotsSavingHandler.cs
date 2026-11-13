using System.Threading;
using System.Threading.Tasks;
using EPiServer;
using EPiServer.Core;
using Microsoft.Extensions.Hosting;

namespace Forte.EpiImageHotSpots;

/// <summary>
/// Guarantees that saved HotSpots never reference blocks that are no longer in Blocks,
/// regardless of what the editor widget sent.
/// </summary>
internal class ImageHotSpotsSavingHandler : IHostedService
{
    private readonly IContentEvents contentEvents;

    public ImageHotSpotsSavingHandler(IContentEvents contentEvents)
    {
        this.contentEvents = contentEvents;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        contentEvents.SavingContent += OnSavingContent;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        contentEvents.SavingContent -= OnSavingContent;
        return Task.CompletedTask;
    }

    private static void OnSavingContent(object sender, ContentEventArgs e)
    {
        if (e.Content is IImageHotSpotsBlock block)
        {
            block.PruneHotSpots();
        }
    }
}
