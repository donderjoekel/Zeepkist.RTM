using BepInEx.Configuration;
using TNRD.Zeepkist.RTM.Core;
using UnityEngine;

namespace TNRD.Zeepkist.RTM.Configuration;

public class ConfigService : IEagerService
{
    public ConfigEntry<bool> EnableMinAuthorTime { get; private set; }
    public ConfigEntry<bool> EnableMaxAuthorTime { get; private set; }
    public ConfigEntry<bool> EnableMinAmountOfRecords { get; private set; }
    public ConfigEntry<bool> EnableMaxAmountOfRecords { get; private set; }
    public ConfigEntry<bool> EnableMinCheckpoints { get; private set; }
    public ConfigEntry<bool> EnableMaxCheckpoints { get; private set; }
    public ConfigEntry<bool> EnableMapExclusion { get; private set; }
    public ConfigEntry<bool> EnableMinAmountOfBlocks { get; private set; }
    public ConfigEntry<bool> EnableMaxAmountOfBlocks { get; private set; }
    public ConfigEntry<bool> EnableMinAmountOfFinishes { get; private set; }
    public ConfigEntry<bool> EnableMaxAmountOfFinishes { get; private set; }

    public ConfigEntry<float> MinAuthorTime { get; private set; }
    public ConfigEntry<float> MaxAuthorTime { get; private set; }
    public ConfigEntry<int> MinAmountOfRecords { get; private set; }
    public ConfigEntry<int> MaxAmountOfRecords { get; private set; }
    public ConfigEntry<int> MinCheckpoints { get; private set; }
    public ConfigEntry<int> MaxCheckpoints { get; private set; }
    public ConfigEntry<string> MapExclusion { get; private set; }
    public ConfigEntry<int> MinAmountOfBlocks { get; private set; }
    public ConfigEntry<int> MaxAmountOfBlocks { get; private set; }
    public ConfigEntry<int> MinAmountOfFinishes { get; private set; }
    public ConfigEntry<int> MaxAmountOfFinishes { get; private set; }

    public ConfigService(ConfigFile config)
    {
        ConfigAuthorTimeFilters(config);
        ConfigRecordFilters(config);
        ConfigCheckpointFilters(config);
        ConfigMapExclusion(config);
        ConfigBlockFilters(config);
        ConfigFinishesFilters(config);
    }

    private void ConfigAuthorTimeFilters(ConfigFile config)
    {
        EnableMinAuthorTime = config.Bind(
            "1. Filters - Author Time",
            "1. Enable Min Author Time",
            false,
            "Should the min author time filter be enabled");
        MinAuthorTime = config.Bind(
            "1. Filters - Author Time",
            "2. Min Author Time",
            0f,
            "The minimum author time in seconds");

        EnableMaxAuthorTime = config.Bind(
            "1. Filters - Author Time",
            "3. Enable Max Author Time",
            false,
            "Should the max author time filter be enabled");
        MaxAuthorTime = config.Bind(
            "1. Filters - Author Time",
            "4. Max Author Time",
            0f,
            "The maximum author time in seconds");
    }

    private void ConfigRecordFilters(ConfigFile config)
    {
        EnableMinAmountOfRecords = config.Bind(
            "2. Filters - Records",
            "1. Enable Min Amount Of Records",
            false,
            "Should the min amount of records filter be enabled");
        MinAmountOfRecords = config.Bind(
            "2. Filters - Records",
            "2. Min Amount Of Records",
            0,
            "The minimum amount of records");

        EnableMaxAmountOfRecords = config.Bind(
            "2. Filters - Records",
            "3. Enable Max Amount Of Records",
            false,
            "Should the max amount of records filter be enabled");
        MaxAmountOfRecords = config.Bind(
            "2. Filters - Records",
            "4. Max Amount Of Records",
            0,
            "The maximum amount of records");
    }

    private void ConfigCheckpointFilters(ConfigFile config)
    {
        EnableMinCheckpoints = config.Bind(
            "3. Filters - Checkpoints",
            "1. Enable Min Checkpoints",
            false,
            "Should the min checkpoints filter be enabled");
        MinCheckpoints = config.Bind(
            "3. Filters - Checkpoints",
            "2. Min Checkpoints",
            0,
            "The minimum amount of checkpoints");

        EnableMaxCheckpoints = config.Bind(
            "3. Filters - Checkpoints",
            "3. Enable Max Checkpoints",
            false,
            "Should the max checkpoints filter be enabled");
        MaxCheckpoints = config.Bind(
            "3. Filters - Checkpoints",
            "4. Max Checkpoints",
            0,
            "The maximum amount of checkpoints");
    }

    private void ConfigMapExclusion(ConfigFile config)
    {
        EnableMapExclusion = config.Bind(
            "4. Filters - Map Exclusion",
            "1. Enable Map Exclusion",
            false,
            "Should the map exclusion filter be enabled");
        MapExclusion = config.Bind(
            "4. Filters - Map Exclusion",
            "2. Map Exclusion",
            "",
            "The map exclusion filter");
    }

    private void ConfigBlockFilters(ConfigFile config)
    {
        EnableMinAmountOfBlocks = config.Bind(
            "5. Filters - Blocks",
            "1. Enable Min Amount Of Blocks",
            false,
            "Should the min amount of blocks filter be enabled");
        MinAmountOfBlocks = config.Bind(
            "5. Filters - Blocks",
            "2. Min Amount Of Blocks",
            0,
            "The minimum amount of blocks");

        EnableMaxAmountOfBlocks = config.Bind(
            "5. Filters - Blocks",
            "3. Enable Max Amount Of Blocks",
            false,
            "Should the max amount of blocks filter be enabled");
        MaxAmountOfBlocks = config.Bind(
            "5. Filters - Blocks",
            "4. Max Amount Of Blocks",
            0,
            "The maximum amount of blocks");
    }

    private void ConfigFinishesFilters(ConfigFile config)
    {
        EnableMinAmountOfFinishes = config.Bind(
            "6. Filters - Finishes",
            "1. Enable Min Amount Of Finishes",
            false,
            "Should the min amount of finishes filter be enabled");
        MinAmountOfFinishes = config.Bind(
            "6. Filters - Finishes",
            "2. Min Amount Of Finishes",
            0,
            "The minimum amount of finishes");

        EnableMaxAmountOfFinishes = config.Bind(
            "6. Filters - Finishes",
            "3. Enable Max Amount Of Finishes",
            false,
            "Should the max amount of finishes filter be enabled");
        MaxAmountOfFinishes = config.Bind(
            "6. Filters - Finishes",
            "4. Max Amount Of Finishes",
            0,
            "The maximum amount of finishes");
    }
}
