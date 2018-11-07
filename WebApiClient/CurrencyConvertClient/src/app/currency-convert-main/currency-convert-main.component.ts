import { Component, OnInit, Input } from '@angular/core';
import { variable } from '@angular/compiler/src/output/output_ast';
import { DataRequestService } from '../data-request.service';

@Component({
   selector: 'app-currency-convert-main',
   templateUrl: './currency-convert-main.component.html',
   styleUrls: ['./currency-convert-main.component.css']
})
export class CurrencyConvertMainComponent implements OnInit {
   @Input() public currenciesList: any = new Object();

   public convertFromCurrency: string = 'EUR';
   public convertToCurrency: string = 'EUR'; public convertResult: any = 0;
   public convertFromAmount: number = 1;

   public displayResultText: string = '';

   public getCurrenciesListKeys() {
      return Object.keys(this.currenciesList);
   }

   constructor(private dataRequestService: DataRequestService) {
   }

   private getCurrenciesDictionary() {
      this.dataRequestService.getCurrencyCodesToLongNames().subscribe(currenciesList => {
         this.currenciesList = currenciesList;
         const currencyKeys: string[] = Object.keys(currenciesList);
         this.convertFromCurrency = this.currenciesList[currencyKeys[0]];
         this.convertToCurrency = this.currenciesList[currencyKeys[0]];
      });
   }

   ngOnInit() {
      // this.getCurrenciesDictionary();
   }

   getConvertData(): void {
      this.dataRequestService.getConvertedCurrency(this.convertFromCurrency, this.convertToCurrency, this.convertFromAmount)
         .subscribe(convertResult => {
            this.convertResult = convertResult;
            const displayText = `${this.convertFromAmount} ${
               this.currenciesList[this.convertFromCurrency]
               } is ${this.convertResult} ${
               this.currenciesList[this.convertToCurrency]
               }`;
            this.displayResultText = displayText;
         });
   }

   inputValueChangeHandler(e: any): void {
      // if (e.value < 0) {
      //    e.preventDefault();
      // }
      const newValue = e.target.value;
      this.convertFromAmount = newValue;
   }
   resetConvertValues(): void {
      this.convertFromAmount = 0;
      this.convertResult = 0;
   }

   fromCurrencyChange(eventObject: any): void {
      const val = eventObject.target.value;
      this.convertFromCurrency = val;
   }
   toCurrencyChange(eventObject: any): void {
      const val = eventObject.target.value;
      this.convertToCurrency = val;
   }

   switchCurrencies(): void {
      const convertToCurrency = this.convertToCurrency;
      this.convertToCurrency = this.convertFromCurrency;
      this.convertFromCurrency = convertToCurrency;
   }
}
