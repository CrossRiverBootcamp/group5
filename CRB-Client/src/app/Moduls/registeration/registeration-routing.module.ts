import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountDetailsComponent } from '../account-actions/account-details/account-details.component';
import { AccountActionsModule } from '../account-actions/account-actions.module';



const routes: Routes = [
  //{path: 'account-menu', component: AccountMenuComponent}
  {path: 'account-details/:id', component: AccountDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes), AccountActionsModule],
  exports: [RouterModule]
})
export class AccountRoutingModule { }