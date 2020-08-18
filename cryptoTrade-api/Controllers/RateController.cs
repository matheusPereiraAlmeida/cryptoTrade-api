using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptoTrade_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace cryptoTrade_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        RateService rateService = new RateService();

        // GET api/rate/5
        [HttpGet("{id}/{cointype1}/{cointype2}/{orderby:int?}")]
        public ActionResult<string> Get(int id, string cointype1, string cointype2, int orderby)
        {           
            return rateService.RetrieveCurrentCoinRate(id, cointype1, cointype2, orderby);
        }
    }
}