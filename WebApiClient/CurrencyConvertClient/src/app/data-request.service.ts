import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class DataRequestService {
   private currencyCodesToLongNamesUrl: string = 'http://localhost:56421/api/currencyConvert/currencyCodesToLongName';
   private getConvertCurrencyUrl(fromCurrency: string, toCurrency: string, convertAmount: number): string {
      return `http://localhost:56421/api/currencyConvert/convertAmount?baseCurrency=${fromCurrency}&targetCurrency=${toCurrency}&convertAmount=${convertAmount}`;
   }
   private getAdjustedCurrencyRatesUrl(baseCurrency: string): string {
      return `http://localhost:56421/api/currencyConvert/rates?&baseCurrency=${baseCurrency}`;
   }

   constructor(private http: HttpClient) { }

   public getCurrencyCodesToLongNames() {
      return this.http.get(
         this.currencyCodesToLongNamesUrl
      );
   }

   public getConvertedCurrency(fromCurrency: string, toCurrency: string, convertAmount: number) {
      return this.http.get(
         this.getConvertCurrencyUrl(fromCurrency, toCurrency, convertAmount)
      );
   }

   public getAdjustedCurrencyRates(baseCurrency: string) {
      return this.http.get(
         this.getAdjustedCurrencyRatesUrl(baseCurrency)
      );
   }

}
