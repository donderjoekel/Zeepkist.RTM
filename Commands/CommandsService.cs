using TNRD.Zeepkist.RTM.Commands.Filters;
using TNRD.Zeepkist.RTM.Core;
using ZeepSDK.ChatCommands;

namespace TNRD.Zeepkist.RTM.Commands;

public class CommandsService : IEagerService
{
    public CommandsService()
    {
        ChatCommandApi.RegisterLocalChatCommand<AddCommand>();
        ChatCommandApi.RegisterLocalChatCommand<StartCommand>();
        ChatCommandApi.RegisterLocalChatCommand<StopCommand>();

        ChatCommandApi.RegisterLocalChatCommand<LoadDefaultsCommand>();
        ChatCommandApi.RegisterLocalChatCommand<ExcludeLevelCommand>();
        ChatCommandApi.RegisterLocalChatCommand<SaveFiltersCommand>();
        ChatCommandApi.RegisterLocalChatCommand<SavePlaylistCommand>();

        ChatCommandApi.RegisterLocalChatCommand<SetMinimumAuthorTimeCommand>();
        ChatCommandApi.RegisterLocalChatCommand<SetMaximumAuthorTimeCommand>();
        ChatCommandApi.RegisterLocalChatCommand<SetMinimumCheckpointsCommand>();
        ChatCommandApi.RegisterLocalChatCommand<SetMaximumCheckpointsCommand>();
        ChatCommandApi.RegisterLocalChatCommand<SetMinimumAmountOfRecordsCommand>();
        ChatCommandApi.RegisterLocalChatCommand<SetMaximumAmountOfRecordsCommand>();
        ChatCommandApi.RegisterLocalChatCommand<SetMinimumAmountOfBlocksCommand>();
        ChatCommandApi.RegisterLocalChatCommand<SetMaximumAmountOfBlocksCommand>();
        ChatCommandApi.RegisterLocalChatCommand<SetMinimumAmountOfFinishesCommand>();
        ChatCommandApi.RegisterLocalChatCommand<SetMaximumAmountOfFinishesCommand>();
    }
}