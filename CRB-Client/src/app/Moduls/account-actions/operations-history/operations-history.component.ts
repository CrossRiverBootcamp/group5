import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { AccountInfo } from 'src/app/Models/AccountInfo';
import { Operation } from 'src/app/Models/Operation';
import { AccountActionsService } from '../account-actions.service';

@Component({
  selector: 'app-operations-history',
  templateUrl: './operations-history.component.html',
  styleUrls: ['./operations-history.component.scss']
})
export class OperationsHistoryComponent implements OnInit {

  constructor(private _accountActionsService: AccountActionsService, private _acr: ActivatedRoute, private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this._acr.paramMap.subscribe(params => {
      if (params.get('id') !== undefined) {
        this.accountId = params.get('id');
      }
    })
    this.getOperationsList();
  }

  title: string = 'Operations History';
  accountId?: any;

  operationsList: Operation[] = [
    // {accountId:'C9A87A06-09D2-46A7-FBE7-08DA8F323F11',debitOrCredit:true,amount:1,balance:905,date:new Date()},
    // {accountId:'C9A87A06-09D2-46A7-FBE7-08DA8F323F11',debitOrCredit:true,amount:2,balance:905,date:new Date()},
    // {accountId:'C9A87A06-09D2-46A7-FBE7-08DA8F323F11',debitOrCredit:true,amount:3,balance:905,date:new Date()},
    // {accountId:'C9A87A06-09D2-46A7-FBE7-08DA8F323F11',debitOrCredit:true,amount:4,balance:905,date:new Date()},
    // {accountId:'C9A87A06-09D2-46A7-FBE7-08DA8F323F11',debitOrCredit:true,amount:5,balance:905,date:new Date()}
  ];

  displayedColumns: string[] = ['accountId', 'debitOrCredit', 'amount', 'balance', 'date', 'moreDetails'];

  pageEvent?: PageEvent;
  pageNumber: number = 1;
  numberOfRecords: number = 4;

  onPaginateChange(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.numberOfRecords = event.pageSize;
    this.getOperationsList();
  }

  getOperationsList() {
    this._accountActionsService.getOperationsHistoryByAccountId(this.accountId, this.pageNumber, this.numberOfRecords)
      .subscribe(data => {
        this.operationsList = data;

      }, err => console.log(err.error));
  }

  partnerAccountInfo?: AccountInfo;
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';

  openSnackBar(element: Operation) {
    this._accountActionsService.getAccountInfo(element.accountID).subscribe(data => {
      this.partnerAccountInfo = data;
      let s: string;
      if (element.debit_Credit)
        s = "From: ";
      else
        s = "To: ";

      this._snackBar.open(

        s + JSON.stringify(this.partnerAccountInfo.firstName + ' ' + this.partnerAccountInfo.lastName)
        + ' - Email Addres: ' + JSON.stringify(this.partnerAccountInfo.email), 'close');
    },
      err => console.log(err.error))
  }

}

