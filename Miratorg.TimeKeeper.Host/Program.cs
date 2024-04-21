using Miratorg.TimeKeeper.Host.Extensions;
using Miratorg.Microservices;
using System.Text;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var host = new ServiceHostRunner();
        await host.Run(args, services => services.AddHostComponents(), app => app.ConfigureApp());
    }
}
