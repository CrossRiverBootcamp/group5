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

@NgModule({
  declarations: [
    SignUpComponent,
    LoginComponent,
  ],
  imports: [
    CommonModule, HttpClientModule,
    ReactiveFormsModule,MatFormFieldModule, MatInputModule,MatIconModule,MatButtonModule,
  ],
  exports: [SignUpComponent, LoginComponent],
  providers: [registerationService]
})
export class RegisterationModule { }
