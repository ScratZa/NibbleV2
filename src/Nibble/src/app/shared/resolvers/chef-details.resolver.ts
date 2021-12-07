import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ChefDetails } from '../models/chef-details';
import { DataRequestError } from '../models/data-request-error';
import { ChefService } from '../services/chef.service';

@Injectable({
  providedIn: 'root'
})
export class ChefDetailsResolver implements Resolve<ChefDetails | DataRequestError> {

  constructor(private chefService: ChefService) {

  }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ChefDetails | DataRequestError> {
    return this.chefService.getChef(route.paramMap.get('id'))
    .pipe(
      map(x => <ChefDetails>x)
    );
  }
}
