import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { SignUpComponent } from './sign-up/sign-up.component';
import { ReactiveFormsModule } from '@angular/forms';
import { registerationService } from './registration.service';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { LoginComponent } from './login/login.component';
import { Routes } from '@angular/router';
import { AccountRoutingModule } from './registeration-routing.module';
import { AccountActionsModule } from '../account-actions/account-actions.module';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { EmailVerificationComponent } from './email-verification/email-verification.component';
import { CodeInputModule } from 'angular-code-input';
import {MatCardModule} from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [
    SignUpComponent,
    LoginComponent,
    EmailVerificationComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatFormFieldModule, 
    MatInputModule,
    MatIconModule,
    MatButtonModule, 
    AccountActionsModule, 
    AccountRoutingModule, 
    MatToolbarModule, 
    MatMenuModule,
    MatCardModule,
    MatDialogModule,
    CodeInputModule.forRoot({
      codeLength: 4,
      isCharsCode: true
    })
  ],
  exports: [SignUpComponent, LoginComponent],
  providers: [registerationService]
})
export class RegisterationModule { }
