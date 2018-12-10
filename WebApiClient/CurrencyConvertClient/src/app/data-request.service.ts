import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class DataRequestService {
   private baseApi: string = 'http://localhost:56421';
   private currencyCodesToLongNamesUrl: string = `${this.baseApi}/api/currencyConvert/currencyCodesToLongName`;
   private getConvertCurrencyUrl(fromCurrency: string, toCurrency: string, convertAmount: number): string {
      return `${this.baseApi}/api/currencyConvert/convertAmount?baseCurrency=${fromCurrency}&targetCurrency=${toCurrency}&convertAmount=${convertAmount}`;
   }
   private getAdjustedCurrencyRatesUrl(baseCurrency: string, skipAmount: number, takeAmount: number): string {
      return `${this.baseApi}/api/currencyConvert/rates?&baseCurrency=${baseCurrency}&takeAmount=${takeAmount}&skipAmount=${skipAmount}`;
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

   public getAdjustedCurrencyRates(baseCurrency: string, skipAmount: number, takeAmount: number) {
      return this.http.get(
         this.getAdjustedCurrencyRatesUrl(baseCurrency, skipAmount, takeAmount)
      );
   }

}
