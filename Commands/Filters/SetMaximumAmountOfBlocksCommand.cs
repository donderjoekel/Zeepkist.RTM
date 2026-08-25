using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.ChatCommands;

namespace TNRD.Zeepkist.RTM.Commands.Filters;

public class SetMaximumAmountOfBlocksCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm set max blocks";
    public string Description => "Sets the maximum amount of blocks";

    public void Handle(string arguments)
    {
        ServiceHelper.GetService<RandomTrackModeFilterService>().SetMaximumAmountOfBlocks(int.Parse(arguments));
    }
}