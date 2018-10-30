using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CurrencyConvertService.Calculators;
using CurrencyConvertService.Data;
using CurrencyConvertService.Helpers;
using CurrencyConvertService.Models;
using CurrencyConvertService.Services;

namespace CurrencyConvertService
{
    public class ConvertService
    {
        private CurrencyConvertData _currencyConvertData;
        private CurrencyConvertCalculator _currencyConvertCalculator;

        private readonly DataRequestService _dataRequestService;


        public ConvertService()
        {
            _dataRequestService = new DataRequestService();
        }

        public CurrencyConvertData ConvertData
        {
            get
            {
                if (_currencyConvertData == null)
                {
                    throw new NullReferenceException("The currency convert data is uninitialized. Make sure you initialize it first before accesing it.");
                }
                return _currencyConvertData;
            }
            private set
            {
                _currencyConvertData = value;
            }
        }

        public CurrencyConvertCalculator ConvertCalculator
        {
            get
            {
                if (_currencyConvertCalculator == null)
                {
                    throw new NullReferenceException("The currency convert calculator is uninitialized. Make sure you initialize it first before accesing it.");

                }
                return _currencyConvertCalculator;
            }
            private set
            {
                _currencyConvertCalculator = value;
            }
        }


        public async Task<bool> InitializeServiceAttempt()
        {
            var dataRequestTaskList = new List<Task<ResponseMessageDto>>();

            Task<ResponseMessageDto> namesToCodeData = _dataRequestService.GetSymbolsAsync();
            dataRequestTaskList.Add(namesToCodeData);

            Task<ResponseMessageDto> currenciesRatesData = _dataRequestService.GetRatesAsync();
            dataRequestTaskList.Add(currenciesRatesData);

            await Task.WhenAll(dataRequestTaskList);

            if (namesToCodeData.Result?.Symbols != null && currenciesRatesData.Result?.Rates != null)
            {
                IntializeData(namesToCodeData.Result.Symbols, currenciesRatesData.Result.Rates);

                return true;
            }

            return false;
        }

        private void IntializeData(Dictionary<string, string> namesToCodeData, Dictionary<string, float> currenciesRatesData)
        {
            ConvertData = new CurrencyConvertData(namesToCodeData);
            ConvertCalculator = new CurrencyConvertCalculator(currenciesRatesData);
        }


    }
}
