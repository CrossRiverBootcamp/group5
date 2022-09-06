import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { AccountDetailsService } from './account-details.service';



@NgModule({
  declarations: [
    AccountDetailsComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [AccountDetailsComponent],
  providers:[AccountDetailsService,]
})
export class AccountDetailsModule { }
