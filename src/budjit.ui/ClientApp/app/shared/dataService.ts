import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import 'rxjs/add/operator/map';
import { Observable } from "rxjs/Observable";
import { Transaction } from "./transaction";

@Injectable()
export class DataService {
    constructor(private http: HttpClient) { }

    public transactions: Transaction[] = [];

    loadTransactions(): Observable<boolean> {
        return this.http.get("/api/transaction")
            .map((data: any) => {
                this.transactions = data;
                return true;
            });
    }

    uploadTransactionFile(file: FormData){
        return this.http.post('/api/transaction/file', file);
    }
}