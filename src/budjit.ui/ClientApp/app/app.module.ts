import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { TransactionList } from "./transactions/transactionList.component";
import { DataService } from "./shared/dataService";
import { RouterModule } from "@angular/router";
import { Dashboard } from "./dashboard/dashboard.component";

let routes = [
  { path: "", component: Dashboard }
];

@NgModule({
  declarations: [
    AppComponent,
    TransactionList,
    Dashboard
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes, {
      useHash: true,
      enableTracing: false
    })
  ],
  providers: [
    DataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
