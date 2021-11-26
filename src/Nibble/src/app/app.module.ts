import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AmplifyAuthenticatorModule } from '@aws-amplify/ui-angular';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { SplashComponent } from './splash.component';
import { AuthSigninComponent } from './auth/auth-signin/auth-signin.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthSigninComponent,
    SplashComponent
  ],
  imports: [
    AmplifyAuthenticatorModule,BrowserModule, HttpClientModule, AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
