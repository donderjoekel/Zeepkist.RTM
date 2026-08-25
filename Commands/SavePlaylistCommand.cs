using System;
using TNRD.Zeepkist.RTM.Messaging;
using TNRD.Zeepkist.RTM.Utilities;
using ZeepkistClient;
using ZeepkistNetworking;
using ZeepSDK.ChatCommands;
using ZeepSDK.Messaging;
using ZeepSDK.Playlist;

namespace TNRD.Zeepkist.RTM.Commands;

public class SavePlaylistCommand : ILocalChatCommand
{
    public string Prefix => "/";
    public string Command => "rtm save playlist";
    public string Description => "Saves the current playlist to disk";

    public void Handle(string arguments)
    {
        PlaylistSaveJSON playlist = PlaylistApi.CreatePlaylist("RTM - " + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"));
        IPlaylistEditor editor = PlaylistApi.CreateEditor(playlist);

        foreach (OnlineZeeplevel onlineZeeplevel in ZeepkistNetwork.CurrentLobby.Playlist)
        {
            editor.AddLevel(onlineZeeplevel);
        }

        editor.Save();
        ServiceHelper.GetService<ITaggedMessenger>().LogSuccess("Playlist saved");
    }
}