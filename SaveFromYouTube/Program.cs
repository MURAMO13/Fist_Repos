using SaveFromYouTube.Commands;
using SaveFromYouTube.Interfaces;
using SaveFromYouTube.Services;

namespace SaveFromYouTube;
public class Program
{
    static async Task Main(string[] args)
    {
        YouTubeService client = new YouTubeService();

        IYouTobeCommands showInfoCommand = new ShowInfoCommand(client);
        IYouTobeCommands downloadCommand = new DownloadCommand(client);

        while (true)
        {
            Console.WriteLine("Commands:    show,   download,   exit");
            Console.WriteLine("Enter the command: ");
            
            string? command = Console.ReadLine();
            
            if (command == "show")
            {
                Console.WriteLine("Enter the URL: ");
                
                string? url = Console.ReadLine();
               
                Console.WriteLine(Environment.NewLine);
                
                await showInfoCommand.Execute(url);
            }
            else if (command == "download")
            {
                Console.WriteLine("Enter the URL: ");
               
                string? url = Console.ReadLine();
                
                Console.WriteLine(Environment.NewLine);
                
                await downloadCommand.Execute(url);
            }
            else if (command == "exit")
            {

                Console.WriteLine(Environment.NewLine);
                break;
            }
        }


    }
}
