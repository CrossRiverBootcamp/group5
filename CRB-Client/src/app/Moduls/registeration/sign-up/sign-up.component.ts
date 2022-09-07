import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { registerationService } from '../registration.service';

// export interface Customer {
//     firstName?: string;
//     lastName?: string ;
//     email?: string;
//     password?: string ;

// }

@Component({
  selector: 'sign-up',
  styleUrls: ['sign-up.component.scss'],
  templateUrl: './sign-up.component.html',

})
export class SignUpComponent {

  constructor(private _registerationService: registerationService) {

  }

title: string = 'Open Account';
  hide:boolean = true;

 
  newCustomer: FormGroup = new FormGroup({
    "firstName": new FormControl('', [Validators.required, Validators.minLength(2),Validators.maxLength(50)]),
    "lastName": new FormControl('', [Validators.required, Validators.minLength(2),Validators.maxLength(50)]),
    "email": new FormControl('', [Validators.required, Validators.email]),
    "password": new FormControl('', [Validators.required, Validators.minLength(8),Validators.maxLength(16)]),
  });

  onFormSubmit(): void {
    if (this.newCustomer.valid) {
      let customer = this.newCustomer.value;
      this._registerationService.register(customer)
        .subscribe((success: any) => {
            if (success) {
              alert("The account was created successfully")      }
            else {
              alert("Account creation failed... try again later")
            }
          });
    }

  }
}
