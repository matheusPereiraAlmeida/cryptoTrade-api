using cryptoTrade_api.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cryptoTrade_api.Services
{

    public class RateService
    {
        public const int USDVALUE = 1;
        public const int EXCHANCEVALUE = 2;

        Dictionary<string, int> openWith = new Dictionary<string, int>();

        public RateService()
        {
            InicializaDictionary();
        }

        public String RetrieveCurrentCoinRate(int id, string cointype1, string cointype2, int orderBy)
        {
            var (retorno1, retorno2) = arrumacointypes(cointype1, cointype2);

            cointype1 = retorno1.Trim();
            cointype2 = retorno2.Trim();

            if (cointype1.Length <= 0 || cointype2.Length <= 0 || id <= 0)
            {
                return "";
            }
            else
            {
                string currentCoinRate = Crud.Get("https://api.coinlore.net/api/coin/markets?id=" + id);
                List<Rate> coinRateDeserialized = JsonConvert.DeserializeObject<List<Rate>>(currentCoinRate);
                //se for um desses casos, temos que fazer a regra de 3
                List<Rate> coinRateFiltered = coinRateDeserialized.Where(coin =>
                coin.Base != null && 
                coin.Quote != null && 
                (coin.Base.Equals(cointype1, StringComparison.InvariantCultureIgnoreCase) || coin.Quote.Equals(cointype1, StringComparison.InvariantCultureIgnoreCase))&&
                (coin.Base.Equals(cointype2, StringComparison.InvariantCultureIgnoreCase) || coin.Quote.Equals(cointype2, StringComparison.InvariantCultureIgnoreCase))).ToList();

                //metodo que faz a regra de 3
                List<Rate> terte = CalculatesWhenCoinType1AndCoinType2AreChanged(cointype1, cointype2, coinRateFiltered);
                List<Rate> coinRateOrdered = OrderBy(terte, orderBy);
                string coinRateFilteredAndOrderedJson = JsonConvert.SerializeObject(coinRateOrdered);

                return coinRateFilteredAndOrderedJson;

            }    
        }

        public List<Rate> CalculatesWhenCoinType1AndCoinType2AreChanged(string CoinType1, string CoinType2, List<Rate> coinRateFiltered)
        {
            foreach (var coin in coinRateFiltered)
            {
                if(coin.Base.Equals(CoinType2, StringComparison.InvariantCultureIgnoreCase) || coin.Quote.Equals(CoinType1, StringComparison.InvariantCultureIgnoreCase))
                {
                    coin.Price = 1 / coin.Price;
                    coin.PriceUsd = coin.Price * coin.PriceUsd;

                    var temp = coin.Base;
                    coin.Base = coin.Quote;
                    coin.Quote = temp;
                }                
            }
            return coinRateFiltered;
        }
        public List<Rate> OrderBy(List<Rate> coinRateOrdered, int orderBy)
        {
            switch (orderBy)
            {
                case USDVALUE:
                    coinRateOrdered.OrderBy(coin => coin.PriceUsd);
                    break;
                case EXCHANCEVALUE:
                    coinRateOrdered.OrderBy(coin => coin.Price);
                    break;
                default:
                    break;
            }

            return coinRateOrdered;

        }
        public Tuple<string, string> arrumacointypes(string coinType1, string coinType2)
        {
            string[] split = coinType1.Split(" ");
            string[] split2 = coinType2.Split(" ");

            var novastring1 = split[1].Replace('(', ' ').Replace(')', ' ');
            var novastring2 = split2[1].Replace('(', ' ').Replace(')', ' ');

            return new Tuple<string, string>(novastring1, novastring2);
        }
        public void InicializaDictionary()
        {
            openWith.Add("Vindax", 493);
            openWith.Add("Binance", 541);
            openWith.Add("Huobi", 258);
            openWith.Add("OKEX", 543);
            openWith.Add("HBTC", 593);
            openWith.Add("BitAsset", 508);
            openWith.Add("HitBTC", 49);
            openWith.Add("Bitcoin.com", 563);
            openWith.Add("Coineal", 223);
            openWith.Add("ZT", 547);
            openWith.Add("Bibox", 107);
            openWith.Add("Coinbene", 106);
            openWith.Add("Rightbtc", 117);
            openWith.Add("Coinbase Pro", 187);
            openWith.Add("OMGFIN", 242);
            openWith.Add("Bit - z", 102);
            openWith.Add("KKCoin", 490);
            openWith.Add("DigiFinex", 190);
            openWith.Add("Bkex", 244);
            openWith.Add("TOKOK", 225);
            openWith.Add("ZBG", 249);
            openWith.Add("Upbit", 577);
            openWith.Add("TOPBTC", 206);
            openWith.Add("Coinsbit", 251);
            openWith.Add("bitFlyer", 10);
            openWith.Add("Dragonex", 115);
            openWith.Add("Bitfinex", 9);
            openWith.Add("ChainEx", 241);
            openWith.Add("Kraken", 56);
            openWith.Add("Bitstamp", 16);
            openWith.Add(" Simex Global", 104);
            openWith.Add("CITEX", 469);
            openWith.Add("Cointiger", 147);
            openWith.Add("VB", 567);
            openWith.Add("BitMax", 226);
            openWith.Add("ZB.com", 85);
            openWith.Add("Sistemkoin", 108);
            openWith.Add("Kucoin", 96);
        }
    }
}
