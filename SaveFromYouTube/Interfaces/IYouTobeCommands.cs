namespace SaveFromYouTube.Interfaces;
/// <summary>
/// Interface for working with YouTube
/// </summary>
public interface IYouTobeCommands
{
    Task Execute(string url);
}
