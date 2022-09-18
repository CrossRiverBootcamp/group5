import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from 'src/app/page-not-found/page-not-found.component';
import { AccountDetailsComponent } from '../account-actions/account-details/account-details.component';
import { NewTransactionComponent } from './new-transaction/new-transaction.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';



const routes: Routes = [
  {path: 'account-details/:id', component: AccountDetailsComponent},
  {path: 'new-transaction/:id', component: NewTransactionComponent},
  {path: 'operations-history/:id', component: OperationsHistoryComponent},
  {path: '**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountActionsRoutingModule { }