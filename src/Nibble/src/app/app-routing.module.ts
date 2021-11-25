import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SplashComponent } from './splash.component';

// Lazy load Auth Modules -> keep bundle small , not necessarily needed always
const routes: Routes = [
  {path:'splash', component: SplashComponent},
  {
    path:'auth',
    loadChildren: () => import("./auth/auth.module").then(m => m.AuthModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
