using cryptoTrade_api.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cryptoTrade_api.Services
{
    public class CoinService
{
        public int RetrieveCurrentCoinValueInUSD(int id)
        {
            if (id <= 0) {
                return -1;
            }

            string currentCoin = Crud.Get("https://api.coinlore.net/api/ticker/?id=" + id);
            List<Coin> coinDeserialized = JsonConvert.DeserializeObject<List<Coin>>(currentCoin);
   
            Int32.TryParse(coinDeserialized.First().PriceUsd, out int PriceUsd);
            return PriceUsd;
        }

        public int RetrieveCurrentCoinValueInUSDMultipliedByQuantity(int id, int quantity)
        {
            int currentCoinValueInUDS = RetrieveCurrentCoinValueInUSD(id);
            int quantityTimesCurrentCoinValueInUDS = quantity * currentCoinValueInUDS;

            if (quantityTimesCurrentCoinValueInUDS <= 0)
            {
                return -1;
            }

            return currentCoinValueInUDS * quantity;
        }

}


}
