import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { AuthSignupComponent } from './auth-signup/auth-signup.component';
import { AuthSigninComponent } from './auth-signin/auth-signin.component';
import { AuthComponent } from './auth.component';


@NgModule({
  declarations: [
    AuthSignupComponent,
    AuthSigninComponent,
    AuthComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule
  ]
})
export class AuthModule { }
