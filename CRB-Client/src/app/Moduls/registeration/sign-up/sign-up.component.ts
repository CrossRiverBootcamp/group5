import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FloatLabelType } from '@angular/material/form-field';
import { registerationService } from '../registration.service';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { EmailVerificationComponent } from '../email-verification/email-verification.component';
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

  constructor(public dialog: MatDialog, private _registerationService: registerationService) {

  }

  title: string = 'Open Account';
  hide:boolean = true;
  emailVerification:boolean = false;
  // hideRequiredControl = new FormControl(false);


  newCustomer: FormGroup = new FormGroup({
    "firstName": new FormControl('', [Validators.required, Validators.minLength(2),Validators.maxLength(50)]),
    "lastName": new FormControl('', [Validators.required, Validators.minLength(2),Validators.maxLength(50)]),
    "email": new FormControl('', [Validators.required, Validators.email]),
    "password": new FormControl('', [Validators.required, Validators.minLength(8),Validators.maxLength(16)]),
  });


  onFormSubmit(): void {
   //this.openDialog();
    let email= this.newCustomer.controls['email'].value
 
    this._registerationService.sendVerificationCode(email)
    .subscribe((res) => {
         this.openDialog();
    },
      (err) => {

      });

    // if (this.newCustomer.valid) {
    //   let customer = this.newCustomer.value;
    //   this._registerationService.register(customer)
    //     .subscribe((success: any) => {
    //         if (success) {
    //           alert("The account was created successfully")      }
    //         else {
    //           alert("Account creation failed... try again later")
    //         }
    //       });
    // }

  }
//open dialog
  openDialog(): void {
    const dialogRef = this.dialog.open(EmailVerificationComponent, {
      width: '500px',
      data: {email: this.newCustomer.controls['email'].value},
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    
    });
}}

export interface DialogData {
  email: string;
}