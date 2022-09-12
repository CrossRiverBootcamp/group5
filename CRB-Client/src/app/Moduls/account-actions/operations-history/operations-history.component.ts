import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { Operation } from 'src/app/Models/Operation';
import { AccountActionsService } from '../account-actions.service';

@Component({
  selector: 'app-operations-history',
  templateUrl: './operations-history.component.html',
  styleUrls: ['./operations-history.component.scss']
})
export class OperationsHistoryComponent implements OnInit {

  constructor(private _accountActionsService: AccountActionsService, private _acr: ActivatedRoute) { }

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
  operationsList: Operation[] = [];
  displayedColumns: string[] = ['accountId', 'debitOrCredit', 'amount', 'balance', 'date'];

  pageEvent?: PageEvent;
  pageNumber: number = 1;
  numberOfRecords: number = 10;

  onPaginateChange(event: PageEvent) {
    this.pageNumber = event.pageIndex;
    this.numberOfRecords = event.pageSize;
    this.getOperationsList();
    // alert(JSON.stringify("Current page size: " + event.pageSize + "Current page index: " + event.pageIndex));
  }

  getOperationsList() {
    this._accountActionsService.getOperationsHistoryByAccountId(this.accountId, this.pageNumber, this.numberOfRecords)
      .subscribe(data => {
        console.log(data);
        
        this.operationsList = data;
      })
  }

}

