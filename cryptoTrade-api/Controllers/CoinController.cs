using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptoTrade_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cryptoTrade_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {

        CoinService coinService = new CoinService();

        // GET api/coin/5
        [HttpGet("{id}")]
        public ActionResult<int> Get(int id)
        {
            return coinService.RetrieveCurrentCoinValueInUSD(id);
        }

        // GET api/coin/5/23
        [HttpGet("{id}/{{quantity}}")]
        public ActionResult<int> GetMultipliedByQuantity(int id, int quantity)
        {          
            return coinService.RetrieveCurrentCoinValueInUSDMultipliedByQuantity(id, quantity);
        }

        //get valor multiplicado do get
    }
}