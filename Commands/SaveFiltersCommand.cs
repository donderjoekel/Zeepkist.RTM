using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.ChatCommands;

namespace TNRD.Zeepkist.RTM.Commands;

public class SaveFiltersCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm save filters";
    public string Description => "Saves the current filters to the config file";

    public void Handle(string arguments)
    {
        ServiceHelper.GetService<RandomTrackModeFilterService>().SetAsDefaults();
    }
}