import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Customer } from "src/app/Models/Customer";
import { LoginDTO } from "../Models/LoginDTO";

@Injectable()
export class registerationService {

    constructor(private _http: HttpClient) { }

    register(customer: Customer) :Observable<boolean> {
        return this._http.post<boolean>("https://localhost:7182/api/Account/AddCustomer", customer);
    }
    login(loginDTO: LoginDTO) :Observable<string> {
        return this._http.post<string>("https://localhost:7182/api/Account/AddCustomer", loginDTO);
    }
}