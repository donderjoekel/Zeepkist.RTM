using System.Collections.Generic;
using TNRD.Zeepkist.RTM.Configuration;
using ZeepSDK.Messaging;

namespace TNRD.Zeepkist.RTM.Mode;

public class RandomTrackModeFilterService
{
    private readonly ConfigService _configService;
    private readonly ITaggedMessenger _messenger;

    public float? MinimumAuthorTime { get; private set; }
    public float? MaximumAuthorTime { get; private set; }
    public int? MinimumCheckpoints { get; private set; }
    public int? MaximumCheckpoints { get; private set; }
    public int? MinimumRecords { get; private set; }
    public int? MaximumRecords { get; private set; }
    public int? MinimumAmountOfBlocks { get; private set; }
    public int? MaximumAmountOfBlocks { get; private set; }
    public int? MinimumAmountOfFinishes { get; private set; }
    public int? MaximumAmountOfFinishes { get; private set; }
    public List<string> ExcludedLevels { get; private set; }

    public RandomTrackModeFilterService(ConfigService configService, ITaggedMessenger messenger)
    {
        _configService = configService;
        _messenger = messenger;

        LoadDefaults();
    }

    public void SetAsDefaults()
    {
        _configService.EnableMaxAuthorTime.Value = MaximumAuthorTime.HasValue;
        _configService.EnableMinAuthorTime.Value = MinimumAuthorTime.HasValue;
        _configService.EnableMaxCheckpoints.Value = MaximumCheckpoints.HasValue;
        _configService.EnableMinCheckpoints.Value = MinimumCheckpoints.HasValue;
        _configService.EnableMaxAmountOfRecords.Value = MaximumRecords.HasValue;
        _configService.EnableMinAmountOfRecords.Value = MinimumRecords.HasValue;
        _configService.EnableMaxAmountOfBlocks.Value = MaximumAmountOfBlocks.HasValue;
        _configService.EnableMinAmountOfBlocks.Value = MinimumAmountOfBlocks.HasValue;
        _configService.EnableMaxAmountOfFinishes.Value = MaximumAmountOfFinishes.HasValue;
        _configService.EnableMinAmountOfFinishes.Value = MinimumAmountOfFinishes.HasValue;

        if (MaximumAuthorTime.HasValue)
            _configService.MaxAuthorTime.Value = MaximumAuthorTime.Value;
        if (MinimumAuthorTime.HasValue)
            _configService.MinAuthorTime.Value = MinimumAuthorTime.Value;
        if (MaximumCheckpoints.HasValue)
            _configService.MaxCheckpoints.Value = MaximumCheckpoints.Value;
        if (MinimumCheckpoints.HasValue)
            _configService.MinCheckpoints.Value = MinimumCheckpoints.Value;
        if (MaximumRecords.HasValue)
            _configService.MaxAmountOfRecords.Value = MaximumRecords.Value;
        if (MinimumRecords.HasValue)
            _configService.MinAmountOfRecords.Value = MinimumRecords.Value;
        if (MaximumAmountOfBlocks.HasValue)
            _configService.MaxAmountOfBlocks.Value = MaximumAmountOfBlocks.Value;
        if (MinimumAmountOfBlocks.HasValue)
            _configService.MinAmountOfBlocks.Value = MinimumAmountOfBlocks.Value;
        if (MaximumAmountOfFinishes.HasValue)
            _configService.MaxAmountOfFinishes.Value = MaximumAmountOfFinishes.Value;
        if (MinimumAmountOfFinishes.HasValue)
            _configService.MinAmountOfFinishes.Value = MinimumAmountOfFinishes.Value;

        _messenger.LogSuccess("Default filters set");
    }

    public void LoadDefaults()
    {
        MinimumAuthorTime = _configService.EnableMinAuthorTime.Value
            ? _configService.MinAuthorTime.Value
            : null;
        MaximumAuthorTime = _configService.EnableMaxAuthorTime.Value
            ? _configService.MaxAuthorTime.Value
            : null;
        MinimumCheckpoints = _configService.EnableMinCheckpoints.Value
            ? _configService.MinCheckpoints.Value
            : null;
        MaximumCheckpoints = _configService.EnableMaxCheckpoints.Value
            ? _configService.MaxCheckpoints.Value
            : null;
        MinimumRecords = _configService.EnableMinAmountOfRecords.Value
            ? _configService.MinAmountOfRecords.Value
            : null;
        MaximumRecords = _configService.EnableMaxAmountOfRecords.Value
            ? _configService.MaxAmountOfRecords.Value
            : null;
        MinimumAmountOfBlocks = _configService.EnableMinAmountOfBlocks.Value
            ? _configService.MinAmountOfBlocks.Value
            : null;
        MaximumAmountOfBlocks = _configService.EnableMaxAmountOfBlocks.Value
            ? _configService.MaxAmountOfBlocks.Value
            : null;
        MinimumAmountOfFinishes = _configService.EnableMinAmountOfFinishes.Value
            ? _configService.MinAmountOfFinishes.Value
            : null;
        MaximumAmountOfFinishes = _configService.EnableMaxAmountOfFinishes.Value
            ? _configService.MaxAmountOfFinishes.Value
            : null;
        ExcludedLevels = _configService.EnableMapExclusion.Value
            ? [.._configService.MapExclusion.Value.Split(',')]
            : [];
    }

    public void SetMinimumAuthorTime(float? seconds)
    {
        MinimumAuthorTime = seconds;

        if (MinimumAuthorTime == null)
            _messenger.Log("Minimum author time disabled");
        else
            _messenger.Log($"Minimum author time set to {MinimumAuthorTime} seconds");
    }

    public void SetMaximumAuthorTime(float? seconds)
    {
        MaximumAuthorTime = seconds;

        if (MaximumAuthorTime == null)
            _messenger.Log("Maximum author time disabled");
        else
            _messenger.Log($"Maximum author time set to {MaximumAuthorTime} seconds");
    }

    public void SetMinimumCheckpoints(int? amount)
    {
        MinimumCheckpoints = amount;

        if (MinimumCheckpoints == null)
            _messenger.Log("Minimum checkpoints disabled");
        else
            _messenger.Log($"Minimum checkpoints set to {MinimumCheckpoints}");
    }

    public void SetMaximumCheckpoints(int? amount)
    {
        MaximumCheckpoints = amount;

        if (MaximumCheckpoints == null)
            _messenger.Log("Maximum checkpoints disabled");
        else
            _messenger.Log($"Maximum checkpoints set to {MaximumCheckpoints}");
    }

    public void SetMinimumRecords(int? amount)
    {
        MinimumRecords = amount;

        if (MinimumRecords == null)
            _messenger.Log("Minimum records disabled");
        else
            _messenger.Log($"Minimum records set to {MinimumRecords}");
    }

    public void SetMaximumRecords(int? amount)
    {
        MaximumRecords = amount;

        if (MaximumRecords == null)
            _messenger.Log("Maximum records disabled");
        else
            _messenger.Log($"Maximum records set to {MaximumRecords}");
    }

    public void SetMinimumAmountOfBlocks(int? amount)
    {
        MinimumAmountOfBlocks = amount;

        if (MinimumAmountOfBlocks == null)
            _messenger.Log("Minimum amount of blocks disabled");
        else
            _messenger.Log($"Minimum amount of blocks set to {MinimumAmountOfBlocks}");
    }

    public void SetMaximumAmountOfBlocks(int? amount)
    {
        MaximumAmountOfBlocks = amount;

        if (MaximumAmountOfBlocks == null)
            _messenger.Log("Maximum amount of blocks disabled");
        else
            _messenger.Log($"Maximum amount of blocks set to {MaximumAmountOfBlocks}");
    }

    public void SetMinimumAmountOfFinishes(int? amount)
    {
        MinimumAmountOfFinishes = amount;

        if (MinimumAmountOfFinishes == null)
            _messenger.Log("Minimum amount of finishes disabled");
        else
            _messenger.Log($"Minimum amount of finishes set to {MinimumAmountOfFinishes}");
    }

    public void SetMaximumAmountOfFinishes(int? amount)
    {
        MaximumAmountOfFinishes = amount;

        if (MaximumAmountOfFinishes == null)
            _messenger.Log("Maximum amount of finishes disabled");
        else
            _messenger.Log($"Maximum amount of finishes set to {MaximumAmountOfFinishes}");
    }

    public void ExcludeLevel(string levelHash)
    {
        ExcludedLevels.Add(levelHash);

        if (string.IsNullOrEmpty(_configService.MapExclusion.Value))
            _configService.MapExclusion.Value = levelHash;
        else if (!_configService.MapExclusion.Value.Contains(levelHash))
            _configService.MapExclusion.Value += $",{levelHash}";

        _messenger.Log($"Level '{levelHash}' excluded");
    }
}