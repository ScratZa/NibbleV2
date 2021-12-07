import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChefDetails } from 'src/app/shared/models/chef-details';
import { DataRequestError } from 'src/app/shared/models/data-request-error';

@Component({
  selector: 'app-chef-detail',
  templateUrl: './chef-detail.component.html',
  styleUrls: ['./chef-detail.component.scss']
})
export class ChefDetailComponent implements OnInit {
  chefDetails:ChefDetails;
  displayedColumns: string[] = ["name", "description", "price"]
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    let resolvedData : ChefDetails | DataRequestError = this.route.snapshot.data['chefDetails'];

    if(resolvedData instanceof DataRequestError){
      console.log(resolvedData)
    }
    else{
      this.chefDetails = resolvedData
      console.log(resolvedData)
    }

  }

}
