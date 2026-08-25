using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.ChatCommands;

namespace TNRD.Zeepkist.RTM.Commands.Filters;

public class SetMaximumAuthorTimeCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm set max at";
    public string Description =>"Sets the maximum allowed author time for a track";
    public void Handle(string arguments)
    {
        float? seconds;

        if (float.TryParse(arguments, out float s))
        {
            seconds = s;
        }
        else
        {
            seconds = null;
        }
        
        ServiceHelper.GetService<RandomTrackModeFilterService>().SetMaximumAuthorTime(seconds);
    }
}