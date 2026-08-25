using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.ChatCommands;

namespace TNRD.Zeepkist.RTM.Commands.Filters;

public class SetMinimumAmountOfFinishesCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm set min finishes";
    public string Description => "Sets the minimum amount of finishes";

    public void Handle(string arguments)
    {
        ServiceHelper.GetService<RandomTrackModeFilterService>().SetMinimumAmountOfFinishes(int.Parse(arguments));
    }
}