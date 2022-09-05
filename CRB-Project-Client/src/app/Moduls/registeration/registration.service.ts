import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Customer } from "src/app/Models/Customer";

@Injectable()
export class registerationService {

    constructor(private _http: HttpClient) { }

    register(customer: Customer) :Observable<boolean> {
        return this._http.post<boolean>("/api/Account/AddCustomer", customer);
    }
}