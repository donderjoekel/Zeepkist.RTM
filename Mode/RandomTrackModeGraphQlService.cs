using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TNRD.Zeepkist.RTM.GraphQl;
using ZeepSDK.External.Cysharp.Threading.Tasks;
using ZeepSDK.External.FluentResults;

namespace TNRD.Zeepkist.RTM.Mode;

public class RandomTrackModeGraphQlService
{
    private const string Query =
        "query GetRandomLevels($minAuthorTime: Float,$maxAuthorTime: Float,$minCheckpoints: Int,$maxCheckpoints: Int,$minRecords: Int,$maxRecords: Int,$excludedHashes: [String],$sampleSize: Int,$minFinishes: Int,$minBlocks: Int,$maxFinishes: Int,$maxBlocks: Int){zRtm(pMinAuthorTime: $minAuthorTime,pMaxAuthorTime: $maxAuthorTime,pMinCheckpoints: $minCheckpoints,pMaxCheckpoints: $maxCheckpoints,pMinRecords: $minRecords,pMaxRecords: $maxRecords,pMinBlocks: $minBlocks,pMaxBlocks: $maxBlocks,pMinFinishes: $minFinishes,pMaxFinishes: $maxFinishes,pExcludedHashes: $excludedHashes,pSampleSize: $sampleSize){edges{node{name fileUid fileAuthor workshopId}}}}";

    private readonly GraphQlClient _client;
    private readonly RandomTrackModeFilterService _filterService;

    public RandomTrackModeGraphQlService(GraphQlClient client, RandomTrackModeFilterService filterService)
    {
        _client = client;
        _filterService = filterService;
    }

    public async UniTask<Result<List<Level>>> GetLevels(int amount)
    {
        Result<Root> result = await _client.PostAsync<Root>(
            Query,
            new
            {
                minAuthorTime = _filterService.MinimumAuthorTime,
                maxAuthorTime = _filterService.MaximumAuthorTime,
                minCheckpoints = _filterService.MinimumCheckpoints,
                maxCheckpoints = _filterService.MaximumCheckpoints,
                minRecords = _filterService.MinimumRecords,
                maxRecords = _filterService.MaximumRecords,
                minBlocks = _filterService.MinimumAmountOfBlocks,
                maxBlocks = _filterService.MaximumAmountOfBlocks,
                minFinishes = _filterService.MinimumAmountOfFinishes,
                maxFinishes = _filterService.MaximumAmountOfFinishes,
                excludedHashes = _filterService.ExcludedLevels,
                sampleSize = amount
            });

        if (result.IsFailed)
            return result.ToResult();

        return Result.Ok(MapToLevels(result.Value));
    }

    private static List<Level> MapToLevels(Root root)
    {
        return root.Data.zRtm.Edges.Select(edge => new Level
        {
            Name = edge.Node.Name,
            FileUid = edge.Node.FileUid,
            FileAuthor = edge.Node.FileAuthor,
            WorkshopId = edge.Node.WorkshopId
        }).ToList();
    }

    public class Level
    {
        public string Name { get; set; }
        public string FileUid { get; set; }
        public string FileAuthor { get; set; }
        public string WorkshopId { get; set; }
    }

    [UsedImplicitly]
    private class Root
    {
        public Data Data { get; set; }
    }

    [UsedImplicitly]
    private class Data
    {
        // ReSharper disable once InconsistentNaming
        public ZRtm zRtm { get; set; }
    }

    [UsedImplicitly]
    private class ZRtm
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        public List<Edge> Edges { get; set; }
    }

    [UsedImplicitly]
    private class Edge
    {
        public Node Node { get; set; }
    }

    [UsedImplicitly]
    private class Node
    {
        public string Name { get; set; }
        public string FileUid { get; set; }
        public string FileAuthor { get; set; }
        public string WorkshopId { get; set; }
    }
}