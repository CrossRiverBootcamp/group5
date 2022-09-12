import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewTransactionComponent } from './new-transaction/new-transaction.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { MatCardModule } from '@angular/material/card';
import { AccountActionsService } from './account-actions.service';
import { ActionsMenuComponent } from './actions-menu/actions-menu.component';
import { MatMenuModule } from '@angular/material/menu';
import { AccountActionsRoutingModule } from './account-action-routing.module';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';



@NgModule({
  declarations: [
    NewTransactionComponent,
    AccountDetailsComponent,
    ActionsMenuComponent,
    OperationsHistoryComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    AccountActionsRoutingModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule, 
    MatCardModule,
    MatMenuModule,
    MatTableModule,
    MatPaginatorModule
  ],
  exports: [NewTransactionComponent, AccountDetailsComponent, ActionsMenuComponent, OperationsHistoryComponent],
  providers: [AccountActionsService]
})
export class AccountActionsModule { }
