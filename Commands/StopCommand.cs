using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.ChatCommands;

namespace TNRD.Zeepkist.RTM.Commands;

public class StopCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm stop";
    public string Description => "Stops the random track mode";

    public void Handle(string arguments)
    {
        ServiceHelper.GetService<IRandomTrackModeService>().Stop();
    }
}
