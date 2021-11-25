import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthSigninComponent } from './auth-signin/auth-signin.component';
import { AuthSignupComponent } from './auth-signup/auth-signup.component';
import { AuthComponent } from './auth.component';

const routes: Routes = [
  {path: '', component:AuthComponent, children:
    [
      {path: '',redirectTo :'signin'},
      {path: 'signin', component: AuthSigninComponent},
      {path: 'signup', component: AuthSignupComponent}
    ]
  },
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
