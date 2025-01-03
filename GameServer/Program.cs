﻿using Core.Config;
using Core.Extensions;
using GameServer.Controllers.ChatCommands;
using GameServer.Controllers.Combat;
using GameServer.Controllers.Factory;
using GameServer.Controllers.Manager;
using GameServer.Extensions;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Kcp;
using GameServer.Network.Messages;
using GameServer.Network.Rpc;
using GameServer.Settings;
using GameServer.Systems.Entity;
using GameServer.Systems.Event;
using GameServer.Systems.Notify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GameServer;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "Own Fate | Game Server";
        Console.WriteLine("\r\n________                 ___________       __          \r\n\\_____  \\__  _  ______   \\_   _____/____ _/  |_  ____  \r\n /   |   \\ \\/ \\/ /    \\   |    __) \\__  \\\\   __\\/ __ \\ \r\n/    |    \\     /   |  \\  |     \\   / __ \\|  | \\  ___/ \r\n\\_______  /\\/\\_/|___|  /  \\___  /  (____  /__|  \\___  >\r\n        \\/           \\/       \\/        \\/          \\/ \r\n     \\/ \r\n\r\n\t\t\t\t\t\t\t\t\t\t\t\tGame Server\n");
        
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Logging.AddConsole();

        builder.SetupConfiguration();
        builder.Services.UseLocalResources()
                        .AddControllers()
                        .AddCommands()
                        .AddSingleton<ConfigManager>()
                        .AddSingleton<KcpGateway>().AddScoped<PlayerSession>()
                        .AddScoped<MessageManager>().AddSingleton<EventHandlerFactory>()
                        .AddScoped<RpcManager>().AddScoped<IRpcEndPoint, RpcSessionEndPoint>()
                        .AddSingleton<SessionManager>()
                        .AddScoped<EventSystem>().AddScoped<EntitySystem>().AddScoped<IGameActionListener, NotifySystem>()
                        .AddScoped<EntityFactory>()
                        .AddScoped<ModelManager>().AddScoped<ControllerManager>()
                        .AddScoped<CombatManager>().AddScoped<ChatCommandManager>()
                        .AddHostedService<WWGameServer>();

        IHost host = builder.Build();

        ILogger logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger("Own Fate");
        logger.LogInformation("Support: wsnrfidev");
        logger.LogInformation("Preparing server...");

        host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted.Register(() =>
        {
            logger.LogInformation("Server started! Let's play Own Fate !");
        });

        await host.RunAsync();
    }

    private static void SetupConfiguration(this HostApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("gameplay.json");
        builder.Services.Configure<GatewaySettings>(builder.Configuration.GetRequiredSection("Gateway"));
        builder.Services.Configure<PlayerStartingValues>(builder.Configuration.GetRequiredSection("StartingValues"));
        builder.Services.Configure<GameplayFeatureSettings>(builder.Configuration.GetRequiredSection("Features"));
    }
}
