using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.ChatCommands;

namespace TNRD.Zeepkist.RTM.Commands.Filters;

public class SetMaximumCheckpointsCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm set max cp";
    public string Description =>"Sets the maximum allowed checkpoints for a track";
    public void Handle(string arguments)
    {
        int? amount;

        if (int.TryParse(arguments, out int a))
        {
            amount = a;
        }
        else
        {
            amount = null;
        }
        
        ServiceHelper.GetService<RandomTrackModeFilterService>().SetMaximumCheckpoints(amount);
    }
}