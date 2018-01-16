import { Component, OnInit } from "@angular/core";
import { DataService } from "../shared/dataService";
import { Transaction } from "../shared/transaction";

@Component({
    selector: "transaction-list",
    templateUrl: "transactionList.component.html",
    styleUrls: []
})

export class TransactionList implements OnInit {
    
    constructor(private data: DataService) { }

    public transactions: Transaction[];

    ngOnInit(): void {
        this.data.loadTransactions()
            .subscribe(success => {
                if (success) {
                    this.transactions = this.data.transactions;
                }
            });
    }
}