using System.Collections.Generic;
using ZeepkistClient;
using ZeepSDK.External.Cysharp.Threading.Tasks;
using ZeepSDK.External.FluentResults;
using ZeepSDK.Messaging;
using ZeepSDK.Multiplayer;
using ZeepSDK.Racing;

namespace TNRD.Zeepkist.RTM.Mode;

public interface IRandomTrackModeService
{
    void Start();
    void Stop();
    void Add(int amount);
}

public class RandomTrackModeService : IRandomTrackModeService
{
    private readonly ITaggedMessenger _messenger;
    private readonly RandomTrackModeFilterService _filterService;
    private readonly RandomTrackModeGraphQlService _graphQlService;

    private bool _started;

    public RandomTrackModeService(ITaggedMessenger messenger,
        RandomTrackModeFilterService filterService,
        RandomTrackModeGraphQlService graphQlService)
    {
        _messenger = messenger;
        _filterService = filterService;
        _graphQlService = graphQlService;
    }

    void IRandomTrackModeService.Start()
    {
        if (!MultiplayerApi.IsPlayingOnline)
            return;

        if (_started)
            return;

        Start();
    }

    void IRandomTrackModeService.Stop()
    {
        if (!MultiplayerApi.IsPlayingOnline)
            return;

        if (!_started)
            return;

        Stop();
    }

    void IRandomTrackModeService.Add(int amount)
    {
        if (!MultiplayerApi.IsPlayingOnline)
            return;

        if (!_started)
            return;

        Add(amount);
    }

    private void Start()
    {
        _started = true;
        _filterService.LoadDefaults();
        RacingApi.LevelLoaded += OnLevelLoaded;
        MultiplayerApi.DisconnectedFromGame += OnDisconnectedFromGame;
        _messenger.Log("Started");
        FillPlaylist(10);
    }

    private void OnDisconnectedFromGame()
    {
        Stop();
    }

    private void Stop()
    {
        _started = false;
        RacingApi.LevelLoaded -= OnLevelLoaded;
        _messenger.Log("Stopped");
    }

    private void Add(int amount)
    {
        if (amount > 20)
        {
            _messenger.LogWarning("You can only add 20 tracks at a time");
            return;
        }

        FillPlaylist(amount);
    }

    private void OnLevelLoaded()
    {
        FillPlaylist(1);
    }

    private void FillPlaylist(int amount)
    {
        FillPlaylistAsync(amount).Forget();
    }

    private async UniTaskVoid FillPlaylistAsync(int amount)
    {
        const int maxAttempts = 3;
        int attempts = 0;
        int levelsAdded = 0;

        for (; attempts < maxAttempts; attempts++)
        {
            Result<List<RandomTrackModeGraphQlService.Level>> result = await _graphQlService.GetLevels(amount);

            if (result.IsFailed)
            {
                _messenger.LogError("Failed to get random levels");
                return;
            }

            ZeepkistLobby currentLobby = ZeepkistNetwork.CurrentLobby;

            foreach (RandomTrackModeGraphQlService.Level level in result.Value)
            {
                bool shouldMakeNextLevel = currentLobby.CurrentPlaylistIndex == currentLobby.Playlist.Count - 1;

                MultiplayerApi.AddLevelToPlaylist(new PlaylistItem(level.FileUid,
                        ulong.Parse(level.WorkshopId),
                        level.Name,
                        level.FileAuthor),
                    shouldMakeNextLevel);
            }

            if (result.Value.Count == amount)
                break;

            levelsAdded += result.Value.Count;
        }

        MultiplayerApi.UpdateServerPlaylist();

        if (attempts >= maxAttempts && levelsAdded != amount)
        {
            if (levelsAdded == 0)
                _messenger.LogError("Failed to add random levels, try changing your filters");
            else
                _messenger.LogWarning("Only added " + levelsAdded + " of " + amount + " random levels");
        }
        else
        {
            _messenger.LogSuccess("Random level(s) added");
        }
    }
}