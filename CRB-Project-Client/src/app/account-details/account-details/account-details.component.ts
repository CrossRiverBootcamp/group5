import { Component, OnInit } from '@angular/core';
import { AccountInfo } from 'src/app/Models/AccountInfo';
import { AccountDetailsService } from '../account-details.service';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.scss']
})
export class AccountDetailsComponent implements OnInit {

  constructor(private _accountDetailsService: AccountDetailsService) { }

 accountInfo?: AccountInfo;

  ngOnInit(): void {
    this.accountInfo= this._accountDetailsService.getInfo();
    console.log(this.accountInfo);
  
    
  }

}
