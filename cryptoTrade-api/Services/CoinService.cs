using cryptoTrade_api.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cryptoTrade_api.Services
{
    public class CoinService
{
        public const int ERROR = -1;

        public string RetrieveCurrentCoinValueInUSD(int id)
        {
            if (id <= 0) {
                return ERROR.ToString();
            }

            string currentCoin = Crud.Get("https://api.coinlore.net/api/ticker/?id=" + id);
            List<Coin> coinDeserialized = JsonConvert.DeserializeObject<List<Coin>>(currentCoin);

            return coinDeserialized.First().PriceUsd;
        }

        public int RetrieveCurrentCoinValueInUSDMultipliedByQuantity(int id, int quantity)
        {
            //int currentCoinValueInUDS = RetrieveCurrentCoinValueInUSD(id);
            //int quantityTimesCurrentCoinValueInUDS = quantity * currentCoinValueInUDS;
            /*
            if (quantityTimesCurrentCoinValueInUDS <= 0)
            {
                return ERROR;
            }

            return currentCoinValueInUDS * quantity;
            */
            return 0;
        }

}


}
