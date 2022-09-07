import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { AccountDetailsService } from './account-details.service';
import {MatCardModule} from '@angular/material/card';



@NgModule({
  declarations: [
    AccountDetailsComponent
  ],
  imports: [
    CommonModule,
    MatCardModule
  ],
  exports: [AccountDetailsComponent],
  providers:[AccountDetailsService,]
})
export class AccountDetailsModule { }
