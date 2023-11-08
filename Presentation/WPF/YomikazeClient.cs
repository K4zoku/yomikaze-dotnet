using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Yomikaze.Application.Data.Models.Common;
using Yomikaze.Application.Data.Models.Response;

namespace Yomikaze.WPF;

public class YomikazeClient
{
    private HttpClient Client { get; }

    public YomikazeClient(HttpClient httpClient)
    {
        Client = httpClient;
    }

    public async Task<T> SendAsync<T>(HttpRequestMessage request)
    {
        var response = await Client.SendAsync(request);
        var raw = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseModel<T>>(raw);
        if (!result.Success) throw new(raw);
        return result.Data;
    }

    public async Task<T> GetAsync<T>(string url)
    {
        var response = await Client.GetAsync(url);
        var raw = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseModel<T>>(raw);
        if (!result.Success) throw new(raw);
        return result.Data;
    }

    public async Task<List<ComicModel>> GetComics()
    {
        var result = await GetAsync<List<ComicModel>>("/API/V1/Comics");
        return result;
    }
}
