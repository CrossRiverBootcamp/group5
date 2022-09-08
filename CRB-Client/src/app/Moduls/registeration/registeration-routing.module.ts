import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountDetailsModule } from '../account-details/account-details.module';
import { AccountDetailsComponent } from '../account-details/account-details/account-details.component';



const routes: Routes = [
  {path: 'account-details/:id', component: AccountDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes), AccountDetailsModule],
  exports: [RouterModule]
})
export class AccountRoutingModule { }