import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChefDetailsResolver } from 'src/app/shared/resolvers/chef-details.resolver';
import { ChefAdminComponent } from './components/chef-admin/chef-admin.component';
import { ChefDetailComponent } from './components/chef-detail/chef-detail.component';
import { ChefListComponent } from './components/chef-list/chef-list.component';
import { CreateChefComponent } from './components/create-chef/create-chef.component';
import { DashboardComponent } from './dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    children: [
      {
        path:'', component: ChefAdminComponent,
        children:[
          {
            path:'', component:ChefListComponent
          },
          {
            path:'create', component:CreateChefComponent
          }
        ]
      },
      {
        path:'chef/:id', component:ChefDetailComponent, resolve: {chefDetails: ChefDetailsResolver}
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
