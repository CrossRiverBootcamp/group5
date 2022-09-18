import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FloatLabelType } from '@angular/material/form-field';
import { registerationService } from '../registration.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmailVerificationComponent } from '../email-verification/email-verification.component';

@Component({
  selector: 'sign-up',
  styleUrls: ['sign-up.component.scss'],
  templateUrl: './sign-up.component.html',

})
export class SignUpComponent {

  constructor(public dialog: MatDialog, private _registerationService: registerationService) {

  }

  title: string = 'Open Account';
  hide: boolean = true;
  errorMessage: string = "";

  newCustomer: FormGroup = new FormGroup({
    "firstName": new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
    "lastName": new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
    "email": new FormControl('', [Validators.required, Validators.email]),
    "password": new FormControl('', [Validators.required, Validators.minLength(8), Validators.maxLength(16)]),
  });


  onFormSubmit(): void {
    if (this.newCustomer.valid) {
      let email = this.newCustomer.controls['email'].value

      this._registerationService.sendVerificationCode(email)
        .subscribe((res) => {
          this.openDialog();
        },
          (err) => {
            this.errorMessage = err.error;
          });
    }
  }
  // (err) => {
  //   if (err.status == 401) {
  //     this.error = true;
  //     this.errorMessage = err.error;
  //   }
  // });


  //open dialog
  openDialog(): void {
    const dialogRef = this.dialog.open(EmailVerificationComponent, {
      disableClose: true ,
      width: '500px',
      data: {
        firstName: this.newCustomer.controls['firstName'].value,
        lastName: this.newCustomer.controls['lastName'].value,
        email: this.newCustomer.controls['email'].value,
        password: this.newCustomer.controls['password'].value,
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');

    });
  }
}

