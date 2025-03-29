using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace HelloWorld;

public class DemoClient(HttpClient client)
{
    public async Task<string> GetRandomCatFact()
    {
        var response = await client.GetFromJsonAsync<Response>("");
        if (response is null)
        {
            return "No cat facts today :(";
        }
        
        var fact = Random.Shared
            .GetItems(response.Facts, 1)
            .Select(info => info.Fact)
            .FirstOrDefault("Today's cat facts were empty :(");

        return fact;
    }
    
    private record Response(FactInfo[] Facts);

    private record FactInfo(
        [property: JsonPropertyName("fact_number")] int FactNumber,
        string Fact
    );
}