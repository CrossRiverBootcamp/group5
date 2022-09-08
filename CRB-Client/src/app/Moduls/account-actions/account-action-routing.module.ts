import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountDetailsComponent } from '../account-actions/account-details/account-details.component';
import { NewTransactionComponent } from './new-transaction/new-transaction.component';



const routes: Routes = [
  {path: 'account-details/:id', component: AccountDetailsComponent},
  {path: 'new-transaction/:id', component: NewTransactionComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountActionsRoutingModule { }