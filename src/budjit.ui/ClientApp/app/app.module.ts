import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";
import { RouterModule, Routes } from "@angular/router";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";

import { AppComponent } from './app.component';
import { TransactionList } from "./transactions/transactionList.component";
import { SideNav } from "./shared/navigation/sidenav.component";
import { Dashboard } from "./dashboard/dashboard.component";
import { ImportTransaction } from "./transactions/importTransaction.component";

import { DataService } from "./shared/dataService";


let routes: Routes = [
  { path: "", redirectTo:"/dashboard", pathMatch:"full" },
  { path: "dashboard", component: Dashboard },
  { path: "upload", component: ImportTransaction }
];

@NgModule({
  declarations: [
    AppComponent,
    TransactionList,
    SideNav,
    Dashboard,
    ImportTransaction
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes, {
      useHash: true,
      enableTracing: false
    }),
    FormsModule, 
    ReactiveFormsModule
  ],
  providers: [
    DataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
