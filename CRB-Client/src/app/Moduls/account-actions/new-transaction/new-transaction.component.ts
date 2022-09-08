import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NewTransaction } from 'src/app/Models/NewTransaction';
import { AccountActionsService } from '../account-actions.service';

@Component({
  selector: 'app-new-transaction',
  templateUrl: './new-transaction.component.html',
  styleUrls: ['./new-transaction.component.scss']
})
export class NewTransactionComponent implements OnInit {

  constructor(private _accountActionsService: AccountActionsService) { }

  ngOnInit(): void {
  }

  title: string = 'New Transaction';
  transaction: NewTransaction = new NewTransaction();

  newTransactionForm: FormGroup = new FormGroup({
    "toAccountId": new FormControl("", [Validators.required]),
    "amount": new FormControl("", [Validators.required, Validators.min(1), Validators.max(1000000)]),
    "moreDetails": new FormControl()
  });

  addTransaction(): void {
    if (this.newTransactionForm.valid) {
      this.transaction = new NewTransaction();
      this.transaction = this.newTransactionForm.value;
      this._accountActionsService.createTransaction(this.transaction).subscribe(success => {
        if (success) {
          alert("The transaction was successfully completed");
        }
        else {
          alert("The transaction failed");
        }
      })
    }

  }

}
