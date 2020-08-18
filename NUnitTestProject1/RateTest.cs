using cryptoTrade_api.Entities;
using cryptoTrade_api.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTest_cryptoTrade_api
{

    [TestFixture]
    public class RateTest
    {
        public const int USDVALUE = 1;
        public const int EXCHANCEVALUE = 2;

        [TestCase(90, 20, "", "BTC", 1, -1)]//n passou mercado1
        [TestCase(90, 20, "BTC", "", 1, -1)]//n passou mercado2 
        [TestCase(-1, 20, "BTC", "BTC", 1, -1)]//id invalido  
        [TestCase(90, 20, "BTC", "USDT", 1, 0)]//deu certo 
        public void RetrieveCurrentCoinRate(int id, string cointype1, string cointype2, int orderBy, int expectedValue)
        {
            int ItisLessThanZero = 0;

            if (cointype1.Length <= 0 || cointype2.Length <= 0 || id <= 0)
            {
                ItisLessThanZero = -1;
            }
            else
            {
                string currentCoinRate = Crud.Get("https://api.coinlore.net/api/coin/markets?id=" + id);
                List<Rate> coinRateDeserialized = JsonConvert.DeserializeObject<List<Rate>>(currentCoinRate);

                List<Rate> coinRateFiltered = coinRateDeserialized.Where(coin => coin.Base.Equals(cointype1) && coin.Quote.Equals(cointype2)).ToList();

                switch (orderBy)
                {
                    case USDVALUE:
                        coinRateFiltered.OrderBy(coin => coin.PriceUsd);
                        break;
                    case EXCHANCEVALUE:
                        coinRateFiltered.OrderBy(coin => coin.Price);
                        break;
                    default:
                        break;
                }

                string coinRateFilteredAndOrderedJson = JsonConvert.SerializeObject(coinRateFiltered);
            }

            Assert.AreEqual(expectedValue, ItisLessThanZero);
        }
    }
}
