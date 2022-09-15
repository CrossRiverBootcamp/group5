import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Moduls/registeration/login/login.component';
import { SignUpComponent } from './Moduls/registeration/sign-up/sign-up.component';

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "sign-up" },
  {path: 'sign-up', component: SignUpComponent},
  {path: 'login', component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
