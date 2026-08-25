using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.ChatCommands;

namespace TNRD.Zeepkist.RTM.Commands;

public class StartCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm start";
    public string Description => "Starts the random track mode";

    public void Handle(string arguments)
    {
        ServiceHelper.GetService<IRandomTrackModeService>().Start();
    }
}
