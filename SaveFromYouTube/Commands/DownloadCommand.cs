using SaveFromYouTube.Interfaces;
using SaveFromYouTube.Services;

namespace SaveFromYouTube.Commands;

/// <summary>
/// Command to download a video from YouTube
/// </summary>
public class DownloadCommand : IYouTobeCommands
{
    private readonly YouTubeService _youTubeService;
    public DownloadCommand(YouTubeService youTubeService)
    {
        _youTubeService = youTubeService;
    }

    /// <summary>
    /// Execute the download command
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task Execute(string url)
    {
        await _youTubeService.TryGetInfo(url);
        await _youTubeService.Download(url);
    }
}
