import { Component, OnInit } from '@angular/core';
import { DataRequestService } from './data-request.service';

@Component({
   selector: 'app-root',
   templateUrl: './app.component.html',
   styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
   title = 'CurrencyConvertClient';
   public currenciesList: object = new Object();
   
   constructor(private dataRequestService: DataRequestService) {
   }

   ngOnInit(): void {
      this.getCurrenciesDictionary();
   }
   
   private getCurrenciesDictionary() {
      this.dataRequestService.getCurrencyCodesToLongNames().subscribe(currenciesList => {
         this.currenciesList = currenciesList;
      });
   }
}
