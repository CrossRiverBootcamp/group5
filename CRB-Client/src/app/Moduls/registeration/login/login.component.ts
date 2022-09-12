import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { registerationService } from '../registration.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private _registerationService: registerationService, private _router: Router) { }

  ngOnInit(): void {
  }

  title: string = "Login";
  hide: boolean = true;
  error: boolean = false;
  errorMessage: string = "";
  
  login: FormGroup = new FormGroup({
    "email": new FormControl('', [Validators.required, Validators.email]),
    "password": new FormControl('', [Validators.required, Validators.minLength(8), Validators.maxLength(16)]),
  });


  onFormSubmit(): void {
    if (this.login.valid) {
      let loginDTO = this.login.value;
      this._registerationService.login(loginDTO)
        .subscribe( (res) => {
          this._router.navigate(['/account-menu', res]);
        },
        (err) => {
          alert(err.status);
        //  if(err.status == 401) {
          alert(err.httpStatus);

          this.error=true;
          this.errorMessage="The email or password are worng, please try again";
          //console.log("The email or password are worng, please try again"); 
             
        
        alert(err.status);});

          // .subscribe((accountId: any) => {
          //   if (accountId) {
          //     this._router.navigate(['/account-menu', accountId]);
          //   }
          // },
          //   err => {
  
          //     console.log(err);
  
          //   });
    }
  }
}




