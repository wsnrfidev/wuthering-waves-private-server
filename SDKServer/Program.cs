using SDKServer.Handlers;
using SDKServer.Middleware;

namespace SDKServer;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "Own Fate | SDK Server";
        Console.WriteLine("\r\n________                 ___________       __          \r\n\\_____  \\__  _  ______   \\_   _____/____ _/  |_  ____  \r\n /   |   \\ \\/ \\/ /    \\   |    __) \\__  \\\\   __\\/ __ \\ \r\n/    |    \\     /   |  \\  |     \\   / __ \\|  | \\  ___/ \r\n\\_______  /\\/\\_/|___|  /  \\___  /  (____  /__|  \\___  >\r\n        \\/           \\/       \\/        \\/          \\/ \r\n    \\/ \r\n\r\n\t\t\t\t\t\t\t\t\t\t\t\tSDK Server\n");

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://*:5500");
        builder.Logging.AddSimpleConsole();

        WebApplication app = builder.Build();
        app.UseMiddleware<NotFoundMiddleware>();

        app.MapGet("/api/login", LoginHandler.Login);
        app.MapGet("/index.json", ConfigHandler.GetBaseConfig);

        app.MapGet("/dev/client/mtZyW6ZYIu1pE0TCHUbXcM1oU8vx4hnb/Windows/KeyList_0.9.0.json", HotPatchHandler.OnKeyListRequest);
        app.MapGet("/dev/client/mtZyW6ZYIu1pE0TCHUbXcM1oU8vx4hnb/Windows/config.json", HotPatchHandler.OnConfigRequest);
        app.MapGet("/dev/client/mtZyW6ZYIu1pE0TCHUbXcM1oU8vx4hnb/Windows/client_key/0.9.0/CtBIsHPiwhwOqqBYxj/PakData", HotPatchHandler.OnPakDataRequest);

        await app.RunAsync();
    }
}
