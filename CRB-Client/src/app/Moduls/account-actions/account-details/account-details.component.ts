import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { AccountInfo } from 'src/app/Models/AccountInfo';
import { AccountActionsService } from '../account-actions.service';


@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.scss']
})
export class AccountDetailsComponent implements OnInit {

  constructor(private _accountActionsService: AccountActionsService, private _acr: ActivatedRoute) { }

  //subscription: Subscription | undefined;
  accountId?: any;
  accountInfo?: AccountInfo;

  ngOnInit(): void {
    this._acr.paramMap.subscribe(params => {
      if (params.get('id') !== undefined) {
        this.accountId = params.get('id');
        this._accountActionsService.getAccountInfo(this.accountId).subscribe(data => {
          this.accountInfo = data;
        })
      }
    })
  }

}


