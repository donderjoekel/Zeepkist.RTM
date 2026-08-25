using System;
using Microsoft.Extensions.Logging;
using TNRD.Zeepkist.RTM.Core;
using ZeepSDK.External.Cysharp.Threading.Tasks;

namespace TNRD.Zeepkist.RTM.Logging;

public class UnhandledExceptionLoggerService : IEagerService
{
    private readonly ILogger<UnhandledExceptionLoggerService> _logger;

    public UnhandledExceptionLoggerService(ILogger<UnhandledExceptionLoggerService> logger)
    {
        _logger = logger;
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        UniTaskScheduler.DispatchUnityMainThread = false;
        UniTaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
    }

    private void OnUnobservedTaskException(Exception exception)
    {
        _logger.LogError(exception, "Unobserved task exception");
    }

    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        _logger.LogError(e.ExceptionObject as Exception, "Unhandled exception");
    }
}