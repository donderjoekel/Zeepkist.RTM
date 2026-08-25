using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.ChatCommands;

namespace TNRD.Zeepkist.RTM.Commands;

public class LoadDefaultsCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm default filters";
    public string Description => "Loads the default filters that have been configured through the settings menu";

    public void Handle(string arguments)
    {
        ServiceHelper.GetService<RandomTrackModeFilterService>().LoadDefaults();
    }
}