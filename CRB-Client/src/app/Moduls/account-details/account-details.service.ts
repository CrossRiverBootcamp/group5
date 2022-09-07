import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AccountInfo } from "src/app/Models/AccountInfo";



@Injectable()
export class AccountDetailsService {

    constructor(private _http: HttpClient) { }

    getAccountInfo(accountId: string): Observable<AccountInfo> {
        return this._http.get<AccountInfo>("https://localhost:7182/api/Login/GetCustomerInfoAsync/"+accountId);
    }
  
}