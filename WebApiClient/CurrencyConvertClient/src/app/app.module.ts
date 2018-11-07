import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { CurrencyConvertMainComponent } from './currency-convert-main/currency-convert-main.component';
import { CurrencyListComponent } from './currency-list/currency-list.component';
import { DataRequestService } from './data-request.service';


@NgModule({
  declarations: [
    AppComponent,
    CurrencyConvertMainComponent,
    CurrencyListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
   providers: [DataRequestService],
  bootstrap: [AppComponent]
})
export class AppModule { }
