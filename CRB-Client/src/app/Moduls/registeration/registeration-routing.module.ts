import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountDetailsComponent } from '../account-actions/account-details/account-details.component';
import { AccountActionsModule } from '../account-actions/account-actions.module';
import { ActionsMenuComponent } from '../account-actions/actions-menu/actions-menu.component';



const routes: Routes = [
  {path: 'account-menu/:id', component: ActionsMenuComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes), AccountActionsModule],
  exports: [RouterModule]
})
export class AccountRoutingModule { }