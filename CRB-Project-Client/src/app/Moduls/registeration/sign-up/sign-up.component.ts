import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Customer } from 'src/app/Models/Customer';
import { registerationService } from '../registration.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {

  constructor(private _registrationService: registerationService) { }

  ngOnInit(): void {
  }

  newCustomer: Customer | undefined;

  newCustomerForm: FormGroup = new FormGroup({
    "firstName": new FormControl(["", Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
    "lastName": new FormControl(["", Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
    "email": new FormControl(["", Validators.required, Validators.email]),
    "password": new FormControl(["", Validators.required, Validators.minLength(8), Validators.maxLength(20)]),
  });

  signUp() {
    this.newCustomer = new Customer();
    this.newCustomer.firstName = this.newCustomerForm.controls['firstName'].value;
    this.newCustomer.lastName = this.newCustomerForm.controls['lastName'].value;
    this.newCustomer.email = this.newCustomerForm.controls['email'].value;
    this.newCustomer.password = this.newCustomerForm.controls['password'].value;

    this._registrationService.register(this.newCustomer).subscribe(success => {
      if (success) {
        alert("The account was created successfully")      }
      else {
        alert("Account creation failed... try again later")
      }
    })
  }

}
