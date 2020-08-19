using Newtonsoft.Json;

namespace cryptoTrade_api.Entities
{

    public partial class Coin
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameid")]
        public string Nameid { get; set; }

        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("price_usd")]
        public string PriceUsd { get; set; }

        [JsonProperty("percent_change_24h")]
        public string PercentChange24H { get; set; }

        [JsonProperty("percent_change_1h")]
        public string PercentChange1H { get; set; }

        [JsonProperty("percent_change_7d")]
        public string PercentChange7D { get; set; }

        [JsonProperty("price_btc")]
        public string PriceBtc { get; set; }

        [JsonProperty("market_cap_usd")]
        public string MarketCapUsd { get; set; }

        [JsonProperty("volume24")]
        public double Volume24 { get; set; }

        [JsonProperty("volume24a")]
        public double Volume24A { get; set; }

        [JsonProperty("csupply")]
        public string Csupply { get; set; }

        [JsonProperty("tsupply")]
        public long Tsupply { get; set; }

        [JsonProperty("msupply")]
        public long? Msupply { get; set; }
    }
}
