import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { GeocodeResponse } from '@mapbox/mapbox-sdk/services/geocoding';
import { Observable } from 'rxjs';
import { CreateChefRequest } from '../models/create-chef-request';
import { CreateChefResponse } from '../models/create-chef-response';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ChefService {

  constructor(private httpClient: HttpClient) {

  }

  createChef(chef:CreateChefRequest): Observable<CreateChefResponse> {
    return this.httpClient.post<CreateChefResponse>('/api/chef/create', chef,
    {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    })
    .pipe(
      map(x => <CreateChefResponse>{
        id: x.id
      })
    )
  }
}
