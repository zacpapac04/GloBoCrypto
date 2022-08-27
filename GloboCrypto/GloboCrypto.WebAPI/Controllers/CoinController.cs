﻿using GloboCrypto.Models.Data;
using GloboCrypto.WebAPI.Services.Coins;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GloboCrypto.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly ICoinService CoinService;

        public CoinController(ICoinService coinService)
        {
            CoinService = coinService;
        }

        [HttpGet("{coinId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CoinInfo>> Get(string coinId)
        {
            var result = await CoinService.GetCoinInfo(coinId);
            
            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet("prices/{coinIds}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CoinPriceInfo>>> GetPrices(string coinIds, 
                                                                                string currency, 
                                                                                string intervals)
        {
            var result = await CoinService.GetCoinPriceInfo(coinIds, currency, intervals);
            
            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }
    }
}
