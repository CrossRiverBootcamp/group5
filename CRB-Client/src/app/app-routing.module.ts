import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Moduls/registeration/login/login.component';
import { SignUpComponent } from './Moduls/registeration/sign-up/sign-up.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "login" },
  {path: 'sign-up', component: SignUpComponent},
  {path: 'login', component: LoginComponent},
  //{path: '**', component: PageNotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
