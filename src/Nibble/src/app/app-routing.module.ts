import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthSigninComponent } from './auth/auth-signin/auth-signin.component';
import { StandardAuthGuard } from './auth/standard-auth.guard';
import { SplashComponent } from './splash.component';

// Lazy load Auth Modules -> keep bundle small , not necessarily needed always
const routes: Routes = [
  {
    path:'',
    loadChildren: () => import('./dashboard/dashboard.module').then(m=> m.DashboardModule),
    canActivate: [StandardAuthGuard]
  },
  {
    path:'login',
    component: AuthSigninComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
