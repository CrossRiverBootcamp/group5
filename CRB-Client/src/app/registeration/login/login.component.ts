import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { registerationService } from '../registration.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private _registerationService: registerationService) { }

  ngOnInit(): void {
  }

  hide:boolean = true;
  login: FormGroup = new FormGroup({
    "email": new FormControl('', [Validators.required, Validators.email]),
    "password": new FormControl('', [Validators.required, Validators.minLength(8),Validators.maxLength(16)]),
  });

  onFormSubmit(): void {
    if (this.login.valid) {
      let loginDTO = this.login.value;
      this._registerationService.login(loginDTO)
        .subscribe((success: any) => {
            if (success) {
              alert("The account id is: " + success)      }
            else {
              alert("Account creation failed... try again later")
            }
          });
    }

}}
