import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { DataRequestService } from '../data-request.service';
import { error } from 'protractor';

@Component({
   selector: 'app-currency-list',
   templateUrl: './currency-list.component.html',
   styleUrls: ['./currency-list.component.css']
})
export class CurrencyListComponent implements OnInit, OnDestroy {

   //#region PROPERTIES
   public isLoading: boolean = true;
   
   public isCurrencyRatesListFull: boolean = false;
   public baseCurrency: string = 'EUR';
   public showMoreEnabled: boolean = false;
   
   private _skipAmount: number;
   public get skipAmount(): number {
      return this._skipAmount;
   }
   public set skipAmount(value: number) {
      this._skipAmount = value;
      console.log('current take location', this._skipAmount);
   }

   private _takeAmount: number;
   public get takeAmount(): number {
      return this._takeAmount;
   }
   public set takeAmount(value: number) {
      this._takeAmount = value;
      console.log('take items amount', this._skipAmount);
   }

   /* 
      [className]="this.isLoading ? 'loading' : 'not-loading'"
      [className]="this.isCurrencyRatesListFull ? 'is-over' : ''"
   */
   

   @Input() public currenciesList: object = new Object();
   public currenciesRates: any = new Array();


   //#endregion
   ngOnInit() {
      console.log(document.getElementById('currency-list-table'));
      setTimeout(() => {
         this.initialCurrencyListLoading();
      }, 1000);

      window.addEventListener('scroll', this.scrollHandler.bind(this));
      window.addEventListener('wheel', this.scrollHandler.bind(this));

   }

   ngOnDestroy(): void {
      window.removeEventListener('scroll', this.scrollHandler.bind(this));
      window.removeEventListener('wheel', this.scrollHandler.bind(this));
   }
   constructor(private dataRequestService: DataRequestService) {
      this.skipAmount = 0;
      this.isLoading = true;
   }

   public scrollHandler(e: any): void {
      if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
         // this.loadMoreRates();
         this.loadMoreRates();
      }
   
      // console.log((window.scrollY + 20));
      // console.log(window.innerHeight);
      // const isEndOfScreen = (window.scrollY + 20) > window.innerHeight;
      // console.log(isEndOfScreen);
      // if (isEndOfScreen) {
      //    console.log('End of the page');
      // }
   }
   public getCurrenciesListKeys() {
      return Object.keys(this.currenciesList);
   }
   public getCurrenciesRatesKeys() {
      return Object.keys(this.currenciesRates);
   }

   //#region METHODS


   public changeBaseCurrency(eventObject: any): void {
      const newCurrencyValue = eventObject.target.value;
      this.baseCurrency = newCurrencyValue;
   }

   public initialCurrencyListLoading() {
      this.isLoading = true;
      this.skipAmount = 0;
      this.isCurrencyRatesListFull = false;
      this.currenciesRates = new Array();
      
      const currencyListTable = document.getElementById('currency-list-table');
      const currencyListTableHeader = currencyListTable.querySelector('th');
      const currencyListTableHeaderHeight = currencyListTableHeader.offsetHeight;

      const tableRowHeight = currencyListTableHeaderHeight;

      const topPosition = currencyListTable.offsetTop;
      const height = currencyListTable.offsetHeight;
      const bottomPosition = topPosition + currencyListTableHeaderHeight;

      const windowHeight = window.innerHeight;

      const remainingSpace = windowHeight - bottomPosition;

      const elementsToAddToFillSpace = Math.floor(remainingSpace / tableRowHeight);
      
      this.takeAmount = elementsToAddToFillSpace;
      this.initialCurrenyLoad();
   }

   public loadMoreRates() {
      
      this.loadMoreCurrencyRates();
   }


   public loadMoreCurrencyRates(): void {
      if (!this.isLoading) {
         this.takeAmount = 10;
         this.skipAmount += 10;
         this.isLoading = true;
         this.dataRequestService.getAdjustedCurrencyRates(this.baseCurrency, this.skipAmount, this.takeAmount)
            .subscribe(currenciesRates => {
               if (currenciesRates) {
                  this.addRates(currenciesRates);
                  this.isLoading = false;
               } else {
                  this.showMoreEnabled = false;
                  this.isCurrencyRatesListFull = true;
               }
            });
      }
   }
   public initialCurrenyLoad(): void {

         this.dataRequestService.getAdjustedCurrencyRates(this.baseCurrency, this.skipAmount, this.takeAmount)
            .subscribe(currenciesRates => {
               if (currenciesRates) {
                  this.skipAmount += this.takeAmount;
                  this.addRates(currenciesRates);
                  this.isLoading = false;
               } else {
                  this.showMoreEnabled = false;
                  this.isCurrencyRatesListFull = true;

               }
            });
      
   }






   public getRatesByBaseCurrency(): void {
      if (!this.isLoading) {
         this.isLoading = true;
         this.dataRequestService.getAdjustedCurrencyRates(this.baseCurrency, this.skipAmount, this.takeAmount)
            .subscribe(currenciesRates => {
               if (currenciesRates) {
                  this.skipAmount += 10;
                  this.addRates(currenciesRates);
               } else {
                  currenciesRates = new Object();
                  this.showMoreEnabled = false;
                  this.isCurrencyRatesListFull = true;
               }
            });
      }
   }

   private addRates(newRatesToAppend: object) {
      for (const currencyName in newRatesToAppend) {
         if (newRatesToAppend.hasOwnProperty(currencyName)) {
            const currencyValue = newRatesToAppend[currencyName];
            const truncatedValue: number = Number.parseFloat(currencyValue.toFixed(3));
            this.currenciesRates[currencyName] = truncatedValue;
         }
      }
   }
   //#endregion
}
