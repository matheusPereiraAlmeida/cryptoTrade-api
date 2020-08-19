using cryptoTrade_api.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cryptoTrade_api.Services
{

    public class RateService
    {
        public const string EMPTY = "";
        public const int USDVALUE = 1;
        public const int EXCHANCEVALUE = 2;

        Dictionary<string, int> openWith = new Dictionary<string, int>();

        public String RetrieveCurrentCoinRate(int id, string cointype1, string cointype2, int orderBy)
        {
            var (cointypeChanged1, cointypeChanged2) = Arrumacointypes(cointype1, cointype2);

            cointype1 = cointypeChanged1.Trim();
            cointype2 = cointypeChanged2.Trim();

            if (cointype1.Length <= 0 || cointype2.Length <= 0 || id <= 0)
            {
                return EMPTY;
            }
            else
            {
                string currentCoinRate = Crud.Get("https://api.coinlore.net/api/coin/markets?id=" + id);
                List<Rate> coinRateDeserialized = JsonConvert.DeserializeObject<List<Rate>>(currentCoinRate);
                
                List<Rate> coinRateFiltered = coinRateDeserialized.Where(coin =>
                coin.Base != null && 
                coin.Quote != null && 
                (coin.Base.Equals(cointype1, StringComparison.InvariantCultureIgnoreCase) || coin.Quote.Equals(cointype1, StringComparison.InvariantCultureIgnoreCase))&&
                (coin.Base.Equals(cointype2, StringComparison.InvariantCultureIgnoreCase) || coin.Quote.Equals(cointype2, StringComparison.InvariantCultureIgnoreCase))).ToList();

                List<Rate> newCoinRateFiltered = CalculatesWhenCoinType1AndCoinType2AreChanged(cointype1, cointype2, coinRateFiltered);
                List<Rate> coinRateOrdered = OrderBy(newCoinRateFiltered, orderBy);
                string coinRateFilteredAndOrderedJson = JsonConvert.SerializeObject(coinRateOrdered);

                return coinRateFilteredAndOrderedJson;

            }    
        }
        private List<Rate> CalculatesWhenCoinType1AndCoinType2AreChanged(string CoinType1, string CoinType2, List<Rate> coinRateFiltered)
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
        private List<Rate> OrderBy(List<Rate> coinRateOrdered, int orderBy)
        {
            IOrderedEnumerable<Rate> returnOfList = null;
            orderBy = EXCHANCEVALUE;

            switch (orderBy)
            {
                case USDVALUE:
                    returnOfList = coinRateOrdered.OrderBy(coin => coin.PriceUsd);
                    break;
                case EXCHANCEVALUE:
                    returnOfList = coinRateOrdered.OrderBy(coin => coin.Price);
                    break;
                default:
                    break;
            }

            return returnOfList.ToList();

        }
        private Tuple<string, string> Arrumacointypes(string coinType1, string coinType2)
        {
            string[] split = coinType1.Split(" ");
            string[] split2 = coinType2.Split(" ");

            var newcoinType1 = split[1].Replace('(', ' ').Replace(')', ' ');
            var newcoinType2 = split2[1].Replace('(', ' ').Replace(')', ' ');

            return new Tuple<string, string>(newcoinType1, newcoinType2);
        }
    }
}
