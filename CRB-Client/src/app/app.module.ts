import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar'; 
import {MatButtonModule} from '@angular/material/button';

import { MatMenuModule} from '@angular/material/menu';
import { RegisterationModule } from './Moduls/registeration/registeration.module';
import { AccountActionsModule } from './Moduls/account-actions/account-actions.module';
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RegisterationModule,
    AccountActionsModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
