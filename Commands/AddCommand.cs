using TNRD.Zeepkist.RTM.Mode;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepSDK.ChatCommands;

namespace TNRD.Zeepkist.RTM.Commands;

public class AddCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm add";
    public string Description => "Adds a given amount of random tracks to the queue. Usage: /rtm add <amount>";

    public void Handle(string arguments)
    {
        IRandomTrackModeService randomTrackModeService = ServiceHelper.GetService<IRandomTrackModeService>();

        if (int.TryParse(arguments, out int amount))
        {
            randomTrackModeService.Add(amount);
        }
        else
        {
            randomTrackModeService.Add(1);
        }
    }
}
