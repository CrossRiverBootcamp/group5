import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Customer } from "src/app/Models/Customer";
import { AccountInfo } from "../Models/AccountInfo";
import { LoginDTO } from "../Models/LoginDTO";

@Injectable()
export class AccountDetailsService {

    constructor(private _http: HttpClient) { }
// Observable<AccountInfo> 
    getInfo() {
        // return this._http.get<AccountInfo>("https://localhost:7182/api/Account"+accountId);
        let accountInfo:AccountInfo={
            firstName:"h",
            lastName:"h" ,
            openDate : new Date,
            balance: 12000 
        };

        return accountInfo;
    }
  
}