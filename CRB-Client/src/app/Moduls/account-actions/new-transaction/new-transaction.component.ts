import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NewTransaction } from 'src/app/Models/NewTransaction';
import { AccountActionsService } from '../account-actions.service';

@Component({
  selector: 'app-new-transaction',
  templateUrl: './new-transaction.component.html',
  styleUrls: ['./new-transaction.component.scss']
})
export class NewTransactionComponent implements OnInit {

  constructor(private _accountActionsService: AccountActionsService, private _acr: ActivatedRoute) { }

  ngOnInit(): void {
    this._acr.paramMap.subscribe(params => {
      if (params.get('id') !== undefined) {
        this.accountId = params.get('id');
      }
    })
  }

  title: string = 'New Transaction';
  accountId?: any;
  errorMessage: string = '';
  isTransactionAdded: boolean = false;
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
      this.transaction.fromAccountId = this.accountId;
      this._accountActionsService.createTransaction(this.transaction).subscribe((res) => {
        this.isTransactionAdded = true;
      },
        (err) => {
          this.errorMessage = err.error;
        })
    }
  }
}
