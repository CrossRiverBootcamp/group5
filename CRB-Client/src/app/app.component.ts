import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  headTitle = 'CRB-Project-Client';
  login:string='Login';
  signUp :string='Sign-Up';
}
