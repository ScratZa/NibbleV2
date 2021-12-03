import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateChefComponent } from './components/create-chef/create-chef.component';
import { DashboardComponent } from './dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    children: [
      {
        path:'ac', component: CreateChefComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
