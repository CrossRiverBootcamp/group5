import { Component, OnInit , Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { EmailVerification } from 'src/app/Models/EmailVerification';
import { registerationService } from '../registration.service';
import { DialogData } from '../sign-up/sign-up.component';

@Component({
  selector: 'email-verification',
  templateUrl: './email-verification.component.html',
  styleUrls: ['./email-verification.component.scss']
})
export class EmailVerificationComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<EmailVerificationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private _registerationService: registerationService
  ) {}


  ngOnInit(): void {
  }
  // this called every time when user changed the code
  onCodeChanged(code: string) {
  }

  // this called only if user entered full code
  onCodeCompleted(code: string) {

    let emailVerification:EmailVerification={
      email:this.data.email,
      code:code
    }

    this._registerationService.checkVerificationCode(emailVerification)
    .subscribe((res) => {
     
    },
      (err) => {

      });
  }


  resendVerificationCode(): void {
    this._registerationService.sendVerificationCode(this.data.email)
    .subscribe((res) => {
     
    },
      (err) => {

      });
  }

    onNoClick(): void {
    this.dialogRef.close();
  }
}
