import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { NewTransaction } from "src/app/Models/NewTransaction";
import { Observable } from "rxjs";
import { AccountInfo } from "src/app/Models/AccountInfo";


@Injectable()
export class AccountActionsService {
     
    constructor(private _http: HttpClient) { }

    getAccountInfo(accountId: string): Observable<AccountInfo> {
        return this._http.get<AccountInfo>("https://localhost:7182/api/Login/GetCustomerInfoAsync/"+accountId);
    }

    createTransaction(transaction: NewTransaction) :Observable<boolean> {
        return this._http.post<boolean>("https://localhost:7147/api/Transaction/AddTransactionAsync", transaction);
    }
}