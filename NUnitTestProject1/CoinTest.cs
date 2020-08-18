using cryptoTrade_api.Entities;
using cryptoTrade_api.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace NUnitTest_cryptoTrade_api
{
    [TestFixture]
    public class CoinTest
    {
        [TestCase(0, 0)]
        [TestCase(-1, -1)]
        [TestCase(90,null)]
        public void TestGetCurrentValueForACoin(int id, int expectedValue = 0)
        {
            if (id <= 0)
            {
                Assert.AreEqual(expectedValue, id);
            }

            string currentCoin = Crud.Get("https://api.coinlore.net/api/ticker/?id=" + id);
            List<Coin> coinDeserialized = JsonConvert.DeserializeObject<List<Coin>>(currentCoin);

            Assert.AreNotEqual(expectedValue, coinDeserialized);
        }

        [TestCase(0, 3, 0)]
        [TestCase(-1, 2, -2)]
        [TestCase(1, 0, 0)]
        [TestCase(2, 3, 6)]
        public void TestRetrieveCurrentCoinValueInUSDMultipliedByQuantity(int quantity, int currentCoinValueInUDS, int expectedValue)
        {
            int quantityTimesCurrentCoinValueInUDS = quantity * currentCoinValueInUDS;

            if (quantityTimesCurrentCoinValueInUDS <= 0)
            {
                Assert.AreEqual(expectedValue, quantityTimesCurrentCoinValueInUDS);
            }

            Assert.AreEqual(expectedValue, quantityTimesCurrentCoinValueInUDS);
        }
        
    }
}