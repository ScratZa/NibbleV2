import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ChefListItem } from 'src/app/shared/models/chef-list-item';
import { DataRequestError } from 'src/app/shared/models/data-request-error';
import { ChefService } from 'src/app/shared/services/chef.service';

@Component({
  selector: 'app-chef-list',
  templateUrl: './chef-list.component.html',
  styleUrls: ['./chef-list.component.scss']
})
export class ChefListComponent implements OnInit {
  displayedColumns: string[] = ['Name', 'Address', 'Style', 'Id'];
  dataSource : MatTableDataSource<ChefListItem>;

  constructor(private chefService: ChefService) {

   }

  ngOnInit(): void {
    this.chefService.getChefs()
    .subscribe(
      (data:ChefListItem[]) => {
        this.dataSource = new MatTableDataSource<ChefListItem>(data);
      },
      (err:DataRequestError) =>{
        console.log(err.errorDisplayMessage);
      },
      () =>{}
    )

  }

}
