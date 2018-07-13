using Newtonsoft.Json;
using System.Collections.Generic;

public partial class CoinPrice
{
    [JsonProperty("USD")]
    public float USD { get; set; }

    [JsonProperty("BTC")]
    public float BTC { get; set; }

    [JsonProperty("ETH")]
    public float ETH { get; set; }
}

public partial class Datum
{
    [JsonProperty("Algorithm")]
    public string Algorithm { get; set; }

    [JsonProperty("CoinName")]
    public string CoinName { get; set; }

    [JsonProperty("FullName")]
    public string FullName { get; set; }

    [JsonProperty("FullyPremined")]
    public string FullyPremined { get; set; }

    [JsonProperty("Id")]
    public string Id { get; set; }

    [JsonProperty("ImageUrl")]
    public string ImageUrl { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("PreMinedValue")]
    public string PreMinedValue { get; set; }

    [JsonProperty("ProofType")]
    public string ProofType { get; set; }

    [JsonProperty("SortOrder")]
    public string SortOrder { get; set; }

    [JsonProperty("Sponsored")]
    public bool Sponsored { get; set; }

    [JsonProperty("Symbol")]
    public string Symbol { get; set; }

    [JsonProperty("TotalCoinSupply")]
    public string TotalCoinSupply { get; set; }

    [JsonProperty("TotalCoinsFreeFloat")]
    public string TotalCoinsFreeFloat { get; set; }

    [JsonProperty("Url")]
    public string Url { get; set; }
}

/// <summary>
/// The Root object response from 
/// https://min-api.cryptocompare.com/data/all/coinlist
/// </summary>
[System.Serializable]
public partial class CoinListResponse
{
    [JsonProperty("Response")]
    public string Response { get; set; }

    [JsonProperty("Message")]
    public string Message { get; set; }

    [JsonProperty("BaseImageUrl")]
    public string BaseImageUrl { get; set; }

    [JsonProperty("BaseLinkUrl")]
    public string BaseLinkUrl { get; set; }

    [JsonProperty("DefaultWatchlist")]
    public DefaultWatchlist DefaultWatchlist { get; set; }

    /// <summary>
    /// All tracked currencies from cryptocompare
    /// </summary>
    [JsonProperty("Data")]
    public Dictionary<string, Datum> Data { get; set; }
    
    [JsonProperty("Type")]
    public long Type { get; set; }
}

public partial class CoinListResponse
{
    public static CoinListResponse FromJson(string json) => JsonConvert.DeserializeObject<CoinListResponse>(json, Converter.Settings);
}

public partial class PriceResponse
{
    [JsonProperty("Prices")]
    public Dictionary<string, CoinPrice> prices { get; set; }

    public static PriceResponse FromJson(string json) => JsonConvert.DeserializeObject<PriceResponse>(json, Converter.Settings);
}


public partial class DefaultWatchlist
{
    [JsonProperty("CoinIs")]
    public string CoinIs { get; set; }

    [JsonProperty("Sponsored")]
    public string Sponsored { get; set; }
}
public class Converter
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        //MissingMemberHandling = MissingMemberHandling.Ignore,
    };
}
