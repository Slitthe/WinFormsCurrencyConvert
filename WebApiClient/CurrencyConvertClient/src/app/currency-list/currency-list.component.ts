import { Component, OnInit, Input } from '@angular/core';
import { DataRequestService } from '../data-request.service';

@Component({
   selector: 'app-currency-list',
   templateUrl: './currency-list.component.html',
   styleUrls: ['./currency-list.component.css']
})
export class CurrencyListComponent implements OnInit {
   public baseCurrency: string = 'EUR';

   @Input() public currenciesList: object = new Object();
   public currenciesRates: object = new Object();

   constructor(private dataRequestService: DataRequestService) {
   }

   public getCurrenciesListKeys() {
      return Object.keys(this.currenciesList);
   }
   public getCurrenciesRatesKeys() {
      return Object.keys(this.currenciesRates);
   }

   ngOnInit() {
      // this.getCurrenciesDictionary();
   }

   public changeBaseCurrency(eventObject: any): void {
      const newCurrencyValue = eventObject.target.value;

      this.baseCurrency = newCurrencyValue;
   }

   public getRatesByBaseCurrency(): void {
      this.dataRequestService.getAdjustedCurrencyRates(this.baseCurrency).subscribe(currenciesRates => {
         this.currenciesRates = currenciesRates;
      });
      // const requestUrl = `http://localhost:56421/api/currencyConvert/rates?&baseCurrency=${this.baseCurrency}`;

      // fetch('http://localhost:56421/api/currencyConvert/currencyCodesToLongName')
      // .then(response => {
      //     response.json()
      //     .then(currenciesList => {
      //       this.currenciesList = currenciesList;

      //     });
      //   }
      // );
   }

   private getCurrenciesDictionary() {
      this.dataRequestService.getCurrencyCodesToLongNames().subscribe(currenciesList => {
         this.currenciesList = currenciesList;
      });
   }
}
