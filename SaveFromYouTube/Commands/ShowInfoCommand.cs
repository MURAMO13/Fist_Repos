using SaveFromYouTube.Interfaces;
using SaveFromYouTube.Services;

namespace SaveFromYouTube.Commands;
/// <summary>
/// Command to show information about a video from YouTube
/// </summary>
public class ShowInfoCommand : IYouTobeCommands
{
    private readonly YouTubeService _youTubeService;
    public ShowInfoCommand(YouTubeService youTubeService)
    {
        _youTubeService = youTubeService;
    }
    /// <summary>
    /// Execute the show info about a video command
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task Execute(string url)
    {
        await _youTubeService.TryGetInfo(url);
              _youTubeService.ShowVideoInfo();
    }
}
