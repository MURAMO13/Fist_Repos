using System.Diagnostics;
using System.Net.Http.Headers;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace SaveFromYouTube.Services;

/// <summary>
/// Service for working with YouTube
/// </summary>
public class YouTubeService
{
    private Video? _video;
    private IStreamInfo? _videoStreamInfo, _audioStreamInfo;
    private StreamManifest? _streamManifest;

    private readonly static HttpClient _client = new HttpClient();

    private static YoutubeClient _youTubeClient;

    /// <summary>
    /// Get video info
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task TryGetInfo(string url)
    {

        SetHeaders(_client);

        _youTubeClient = new YoutubeClient(_client);

        _video = await _youTubeClient.Videos.GetAsync(url);
        
        

    }

    /// <summary>
    /// Show video info
    /// </summary>
    public void ShowVideoInfo()
    {
        Console.WriteLine($"Title: {_video.Title}");
        Console.WriteLine($"Author: {_video.Author}");
        Console.WriteLine($"Description: {_video.Description}");
    }
    /// <summary>
    /// Download video
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task Download(string url)
    {
        var _streamManifest = await _youTubeClient.Videos.Streams.GetManifestAsync(url);

        _audioStreamInfo = _streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

        _videoStreamInfo = _streamManifest.GetVideoOnlyStreams()
                                          .Where(s => s.Container == Container.Mp4)
                                          .GetWithHighestVideoQuality();
        string videoFileName = $"{_video.Title}_video.{_videoStreamInfo.Container}";

        await _youTubeClient.Videos.Streams.DownloadAsync(_videoStreamInfo, videoFileName);

        // Works better with a delay
        await Task.Delay(30000);

        string audioFileName = $"{_video.Title}_audio.{_audioStreamInfo.Container}";

        await _youTubeClient.Videos.Streams.DownloadAsync(_audioStreamInfo, audioFileName);

        await MergeAudioAndVideoAsync(videoFileName, audioFileName, $"{_video.Title}_merged.mp4");

        Console.WriteLine("Video download complete.");
    }

    /// <summary>
    /// Merge audio and video
    /// </summary>
    /// <param name="videoFile"></param>
    /// <param name="audioFile"></param>
    /// <param name="outputFile"></param>
    /// <returns></returns>
    private async Task MergeAudioAndVideoAsync(string videoFile, string audioFile, string outputFile)
    {

        var arguments = $"-i \"{videoFile}\" -i \"{audioFile}\" -c copy \"{outputFile}\"";

        // Создаем процесс для вызова ffmpeg
        using (Process process = new Process())
        {
            process.StartInfo.FileName = "ffmpeg";
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.Start();

            // Читаем вывод ffmpeg (обычно ошибки попадают в StandardError)
            string output = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();

            Console.WriteLine(output);
        }




    }

    /// <summary>
    /// Set headers
    /// </summary>
    /// <param name="client"></param>
    private void SetHeaders(HttpClient client)
    {
        client.DefaultRequestHeaders.UserAgent.Clear();
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(Windows NT 10.0; Win64; x64)"));
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppleWebKit", "537.36"));
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(KHTML, like Gecko)"));
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Chrome", "132.0.0.0"));
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Safari", "537.36"));
    }

   
}
