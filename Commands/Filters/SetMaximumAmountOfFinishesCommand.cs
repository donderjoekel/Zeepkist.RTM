using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.ChatCommands;

namespace TNRD.Zeepkist.RTM.Commands.Filters;

public class SetMaximumAmountOfFinishesCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm set max finishes";
    public string Description => "Sets the maximum amount of finishes";

    public void Handle(string arguments)
    {
        ServiceHelper.GetService<RandomTrackModeFilterService>().SetMaximumAmountOfFinishes(int.Parse(arguments));
    }
}