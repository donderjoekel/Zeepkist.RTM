using System;
using System.IO;
using BepInEx;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using TNRD.Zeepkist.RTM.Commands;
using TNRD.Zeepkist.RTM.Configuration;
using TNRD.Zeepkist.RTM.Core;
using TNRD.Zeepkist.RTM.GraphQl;
using TNRD.Zeepkist.RTM.Logging;
using TNRD.Zeepkist.RTM.Messaging;
using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Patching;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.External.Cysharp.Threading.Tasks;
using ZeepSDK.Messaging;
using ZeepSDK.Storage;

namespace TNRD.Zeepkist.RTM;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("ZeepSDK")]
public class Plugin : BaseUnityPlugin
{
    private IHost _host;

    private void Awake()
    {
        StartHost().Forget();
    }

    private void OnDestroy()
    {
        StopHost().Forget();
    }

    private async UniTaskVoid StartHost()
    {
        try
        {
            IHostBuilder builder = Host.CreateDefaultBuilder();
            builder.UseContentRoot(Path.GetDirectoryName(Info.Location)!);
            builder.UseSerilog((context, provider, configuration) =>
            {
                configuration.Enrich.FromLogContext();
                configuration.Enrich.FromGlobalLogContext();
                configuration.WriteTo.BepInEx(Logger, restrictedToMinimumLevel: LogEventLevel.Information);
            });
            builder.ConfigureServices(
                services =>
                {
                    services.AddHostedService<Patcher>();
                    services.AddSingleton<BaseUnityPlugin>(this);
                    services.AddSingleton(this);
                    services.AddSingleton(Config);
                    services.AddSingleton(Logger);
                    services.AddSingleton(Info);
                    services.AddEagerService<UnhandledExceptionLoggerService>();
                    services.AddEagerService<ConfigService>();
                    services.AddEagerService<CommandsService>();
                    services.AddSingleton(_ => StorageApi.CreateModStorage(this));
                    services.AddSingleton<ServiceHelper>();
                    services.AddHttpClient();
                    services.AddSingleton<GraphQlClient>();
                    services.AddSingleton<IRandomTrackModeService, RandomTrackModeService>();
                    services.AddSingleton<RandomTrackModeGraphQlService>();
                    services.AddSingleton<RandomTrackModeFilterService>();
                    services.AddSingleton<ITaggedMessenger, MessengerService>();
                });
            _host = builder.Build();
            await _host.StartAsync();

            // Plugin startup logic
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }
        catch (Exception e)
        {
            Logger.LogError("Failed to start plugin");
            Logger.LogError(e);
        }
    }

    private async UniTaskVoid StopHost()
    {
        await _host.StopAsync();
        _host.Dispose();
    }
}
