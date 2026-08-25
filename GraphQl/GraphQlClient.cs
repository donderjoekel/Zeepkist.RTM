using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using ZeepSDK.External.Cysharp.Threading.Tasks;
using ZeepSDK.External.FluentResults;

namespace TNRD.Zeepkist.RTM.GraphQl;

public class GraphQlClient
{
    private readonly HttpClient _httpClient;
    private readonly AsyncPolicy<HttpResponseMessage> _policy;
    private readonly JsonSerializerSettings _settings;

    public GraphQlClient(
        HttpClient httpClient,
        ILogger<GraphQlClient> logger)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://graphql.zeepki.st");
        _settings = new JsonSerializerSettings();

        _policy = Policy
            .Handle<Exception>()
            .OrResult<HttpResponseMessage>(x => !x.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                3,
                retryCount =>
                {
                    logger.LogWarning("Request failed, retrying ({RetryCount})", retryCount);
                    return TimeSpan.FromSeconds(Math.Pow(2, retryCount));
                });
    }

    public async UniTask<HttpResponseMessage> PostAsync(
        string query,
        object variables = null,
        CancellationToken ct = default)
    {
        return await _policy.ExecuteAsync(
            () => _httpClient.PostAsync(
                string.Empty,
                new StringContent(
                    JsonConvert.SerializeObject(new { query, variables }),
                    Encoding.UTF8,
                    "application/json"),
                ct));
    }

    public async UniTask<Result<T>> PostAsync<T>(string query, object variables = null, CancellationToken ct = default)
    {
        HttpResponseMessage response = await PostAsync(query, variables, ct);

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e));
        }

        string content;

        try
        {
            content = await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e));
        }

        try
        {
            return JsonConvert.DeserializeObject<T>(content, _settings);
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e));
        }
    }
}
