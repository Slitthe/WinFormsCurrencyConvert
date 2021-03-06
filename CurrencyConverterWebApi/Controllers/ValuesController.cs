﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConvertService;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverterWebApi.Controllers
{

    [Route("api/currencyConvert")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {
            Services.CurrencyConvertService.AttemptInitService().Wait();
        }

        private async Task<ConvertService> GetConvertService()
        {
            if (Services.CurrencyConvertService.IsInitialized)
            {
                return Services.CurrencyConvertService.ConvertService;
            }

            return null;
        }


        [HttpGet("convertAmount")]
        public async Task<ActionResult<float>> GetConvertedAmount(
            [FromQuery(Name = "baseCurrency")] string baseCurrency,
            [FromQuery(Name = "targetCurrency")] string targetCurrency,
            [FromQuery(Name = "convertAmount")] float convertAmount)
        {
            if (targetCurrency == null || baseCurrency == null)
            {
                return BadRequest(
                    "You need to specify the following arguments to the request: the base/target currency and convert amount");
            }
            var convertService = await GetConvertService();

            return convertService.ConvertCalculator.ConvertCurrencyAmount(baseCurrency, targetCurrency, convertAmount);
        }

        [HttpGet("currencyCodesToLongName")]
        public async Task< ActionResult< Dictionary<string, string> > > GetCurrenciesCodesToLongNameDictionary()
        {
            var convertService = await GetConvertService();

            var currencyCodesToNamesList = convertService.ConvertData.CurrenciesCodeToLongName;
                
            return currencyCodesToNamesList;
        }


        // make this accept a location and number of items per "page" amount in order to be able to implement the pagination for the currencies list
        [HttpGet("rates")]
        public async Task<ActionResult<IDictionary<string, float> > > GetCurrenciesRates(
            [FromQuery(Name = "baseCurrency")] string baseCurrency,
            [FromQuery(Name = "takeAmount")] int takeAmount,
            [FromQuery(Name = "skipAmount")] int skipAmount
            )
        {

            var convertService = await GetConvertService();
            convertService.ConvertData.SetBaseCurrency(baseCurrency);

            Dictionary<string, float> currenciesRates = convertService.GetCurrenciesRatesByBaseCurrency();
            currenciesRates = currenciesRates.Skip(skipAmount).Take(takeAmount).ToDictionary(item => item.Key, item => item.Value);

            // TODO: pass the base currency to the method insteaof chaning the state (by passing the base currecny to the method)
            if (currenciesRates.Count == 0)
            {
                return NoContent();
            }
            return Ok(currenciesRates);
        }


    }
}
