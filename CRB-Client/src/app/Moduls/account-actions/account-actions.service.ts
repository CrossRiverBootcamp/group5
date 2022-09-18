import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { NewTransaction } from "src/app/Models/NewTransaction";
import { Observable } from "rxjs";
import { AccountInfo } from "src/app/Models/AccountInfo";
import { Operation } from "src/app/Models/Operation";


@Injectable()
export class AccountActionsService {
     
    constructor(private _http: HttpClient) { }

    getCustomerInfo(accountId: string): Observable<AccountInfo> {
        return this._http.get<AccountInfo>("https://localhost:7182/api/Account/GetAccountInfo/"+accountId);
    }
    getAccountInfo(accountId: string): Observable<AccountInfo> {
        return this._http.get<AccountInfo>("https://localhost:7182/api/OperationsHistory/GetAccountInfo/"+accountId);
    }
    createTransaction(transaction: NewTransaction) :Observable<any> {
        return this._http.post<any>("https://localhost:7147/api/Transaction/AddTransactionAsync", transaction);
    }

    getOperationsHistoryByAccountId(accountId: string, pageNumber: number, pageSize: number): Observable<Operation[]> {
        return this._http.get<Operation[]>
            ("https://localhost:7182/api/OperationsHistory/GetOperationsList/" + accountId + "/" + pageNumber + "/" + pageSize);
    }
}