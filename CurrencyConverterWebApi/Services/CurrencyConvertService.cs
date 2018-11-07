using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConvertService;

namespace CurrencyConverterWebApi.Services
{
    public static class CurrencyConvertService
    {
        public static ConvertService ConvertService;
        public static bool IsInitialized = false;

        public static async Task AttemptInitService()
        {
            ConvertService = new ConvertService();

            IsInitialized = await ConvertService.InitializeServiceAttempt();
        }


    }
}
