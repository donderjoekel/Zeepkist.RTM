using System;

namespace TNRD.Zeepkist.RTM.Utilities;

public class ServiceHelper
{
    private static ServiceHelper _instance;
    private readonly IServiceProvider _provider;

    public ServiceHelper(IServiceProvider provider)
    {
        _provider = provider;
        _instance = this;
    }
    
    public static TService GetService<TService>()
    {
        return (TService) _instance._provider.GetService(typeof(TService));
    }

    public static object GetService(Type serviceType)
    {
        return _instance._provider.GetService(serviceType);
    }
}
