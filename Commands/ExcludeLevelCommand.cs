using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.ChatCommands;
using ZeepSDK.Level;

namespace TNRD.Zeepkist.RTM.Commands;

public class ExcludeLevelCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm exclude level";
    public string Description => "Excludes a level from being added to the playlist in the future";

    public void Handle(string arguments)
    {
        ServiceHelper.GetService<RandomTrackModeFilterService>().ExcludeLevel(LevelApi.CurrentHash);
    }
}