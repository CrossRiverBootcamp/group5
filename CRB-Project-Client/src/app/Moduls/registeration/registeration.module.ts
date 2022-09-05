import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignUpComponent } from './sign-up/sign-up.component';
import { HttpClientModule } from '@angular/common/http';
import { registerationService } from './registration.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    SignUpComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    //MatInputModule,
    FormsModule,
    // MatFormFieldModule,
    // MatButtonModule,
    // MatCardModule
  ],
  exports: [
    HttpClientModule,
   // MatInputModule,
    FormsModule,
    // MatFormFieldModule,
    // MatButtonModule,
    // MatCardModule
    SignUpComponent
  ],
  providers: [
    registerationService
  ]
})
export class RegisterationModule { }
