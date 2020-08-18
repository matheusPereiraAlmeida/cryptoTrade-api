using Newtonsoft.Json;

namespace cryptoTrade_api.Entities
{

    public partial class Rate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("quote")]
        public string Quote { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("price_usd")]
        public double PriceUsd { get; set; }

        [JsonProperty("volume")]
        public double Volume { get; set; }

        [JsonProperty("volume_usd")]
        public double VolumeUsd { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }

}
