import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-actions-menu',
  templateUrl: './actions-menu.component.html',
  styleUrls: ['./actions-menu.component.scss']
})
export class ActionsMenuComponent implements OnInit {

  constructor(private _acr: ActivatedRoute, private _router: Router) { }

  accountId?: any;

  ngOnInit(): void {
    this._acr.paramMap.subscribe(params => {
      if (params.get('id') !== undefined) {
        this.accountId = params.get('id');
      }
    })
  }

  navigateToAccountDetails() {
    this._router.navigate(['/account-details', this.accountId]);
  }
  navigateToNewTransaction() {
    this._router.navigate(['/new-transaction', this.accountId]);
  }


}
