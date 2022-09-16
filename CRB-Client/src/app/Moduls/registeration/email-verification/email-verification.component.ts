import { Component, OnInit , Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Customer } from 'src/app/Models/Customer';
import { EmailVerification } from 'src/app/Models/EmailVerification';
import { registerationService } from '../registration.service';

@Component({
  selector: 'email-verification',
  templateUrl: './email-verification.component.html',
  styleUrls: ['./email-verification.component.scss']
})
export class EmailVerificationComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<EmailVerificationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Customer,
    private _registerationService: registerationService
  ) {}

  isAccountCreated:boolean = false;
  errorMessage:string = "";
  resendMail:boolean = false;
  ngOnInit(): void {
  }
  // this called every time when user changed the code
  onCodeChanged(code: string) {  
    this.errorMessage="";
    this.resendMail=false;
  }


  // this called only if user entered full code
  onCodeCompleted(code: string) {
  
      let customer:Customer ={
          firstName: this.data.firstName,
          lastName: this.data.lastName,
          email: this.data. email,
          password: this. data. password,
          verificationCode: code,
      };

      this._registerationService.register(customer)
        .subscribe((res) => {
            this.isAccountCreated=true;
          }, (err) => { 
            this.errorMessage=err.error;
          });
  }


  resendVerificationCode(): void {
    this._registerationService.sendVerificationCode(this.data.email)
    .subscribe((res) => {
     this.resendMail=true;
    },
      (err) => {
        this.errorMessage=err.error;        
      });
  }

  closeDialog(): void {
    this.dialogRef.close();
  }
}
