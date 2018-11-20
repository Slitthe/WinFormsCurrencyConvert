import { Component, OnInit, Input } from '@angular/core';
import { DataRequestService } from '../data-request.service';
import { error } from 'protractor';

@Component({
   selector: 'app-currency-list',
   templateUrl: './currency-list.component.html',
   styleUrls: ['./currency-list.component.css']
})
export class CurrencyListComponent implements OnInit {
   public baseCurrency: string = 'EUR';
   private currentTakeLocation: number;
   public showMoreEnabled: boolean = false;
   private readonly takeItemsAmount: number;
   

   @Input() public currenciesList: object = new Object();
   public currenciesRates: object = new Object();

   constructor(private dataRequestService: DataRequestService) {
      this.currentTakeLocation = 0;
      this.takeItemsAmount = 10;
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

   public showMoreRates(): void {
      this.dataRequestService.getAdjustedCurrencyRates(this.baseCurrency, this.currentTakeLocation, this.takeItemsAmount)
         .subscribe(currenciesRates => {
            if (currenciesRates) {
               if (this.currentTakeLocation === 0) {
                  this.showMoreEnabled = true;
               }
               this.currentTakeLocation += 10;
               this.addRates(currenciesRates);
            } else {
               this.showMoreEnabled = false;
            }
         });
   }

   // link this method to the actual "show more" button in the template
   public getRatesByBaseCurrency(): void {
      this.currentTakeLocation = 0;
      
      this.dataRequestService.getAdjustedCurrencyRates(this.baseCurrency, this.currentTakeLocation, this.takeItemsAmount)
         .subscribe(currenciesRates => {
            if (currenciesRates) {
               if (this.currentTakeLocation === 0) {
                  this.showMoreEnabled = true;
               }
               this.currentTakeLocation += 10;
               this.currenciesRates = currenciesRates;
            } else {
               currenciesRates = new Object();
               this.showMoreEnabled = false;
            }
         });
   }

   private addRates(newRatesToAppend: object) {
      for (const currencyName in newRatesToAppend) {
         if (newRatesToAppend.hasOwnProperty(currencyName)) {
            const currencyValue = newRatesToAppend[currencyName];

            this.currenciesRates[currencyName] = currencyValue;
         }
      }
   }

   private getCurrenciesDictionary() {
      this.dataRequestService.getCurrencyCodesToLongNames().subscribe(currenciesList => {
         this.currenciesList = currenciesList;
      });
   }
}
