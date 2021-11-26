import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthSigninComponent } from './auth/auth-signin/auth-signin.component';
import { SplashComponent } from './splash.component';

// Lazy load Auth Modules -> keep bundle small , not necessarily needed always
const routes: Routes = [
  {path:'splash', component: SplashComponent},
  {
    path:'auth', component: AuthSigninComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
