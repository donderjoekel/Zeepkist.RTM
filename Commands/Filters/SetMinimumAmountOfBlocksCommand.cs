using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.ChatCommands;

namespace TNRD.Zeepkist.RTM.Commands.Filters;

public class SetMinimumAmountOfBlocksCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm set min blocks";
    public string Description => "Sets the minimum amount of blocks";

    public void Handle(string arguments)
    {
        ServiceHelper.GetService<RandomTrackModeFilterService>().SetMinimumAmountOfBlocks(int.Parse(arguments));
    }
}