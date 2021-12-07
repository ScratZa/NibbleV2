import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { GeocodeResponse } from '@mapbox/mapbox-sdk/services/geocoding';
import { Observable, throwError } from 'rxjs';
import { CreateChefRequest } from '../models/create-chef-request';
import { CreateChefResponse } from '../models/create-chef-response';
import { catchError, map } from 'rxjs/operators';
import { ChefListItem } from '../models/chef-list-item';
import { DataRequestError } from '../models/data-request-error';
import { ChefDetails } from '../models/chef-details';

@Injectable({
  providedIn: 'root'
})
export class ChefService {

  constructor(private httpClient: HttpClient) {

  }

  createChef(chef:CreateChefRequest): Observable<CreateChefResponse | DataRequestError> {
    return this.httpClient.post<CreateChefResponse>('/api/chef/create', chef,
    {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    })
    .pipe(
      catchError(x => this.handleRequestError(x)),
      map(x => <CreateChefResponse>{
        id: x.id
      })
    )
  }

  getChefs():Observable<ChefListItem[] | DataRequestError> {
    return this.httpClient.get<ChefListItem[]>('/api/chef/list')
    .pipe(
      catchError(x => this.handleRequestError(x))
    )
  }

  getChef(id: string):Observable<ChefDetails| DataRequestError> {
    return this.httpClient.get<ChefDetails>(`/api/chef/${id}`)
    .pipe(
      catchError(x => this.handleRequestError(x))
    )
  }
  private handleRequestError(httpError: HttpErrorResponse)
  {
    let statusCode = httpError.status
    let dataError = new DataRequestError()
    switch(true){
      case (statusCode < 500 && statusCode>=400):
        dataError.errorCode = 1;
        dataError.errorMessage = httpError.message;
        dataError.errorDisplayMessage = httpError.statusText
        break;
      case (statusCode > 500):
        dataError.errorCode = 2;
        dataError.errorMessage = httpError.message;
        dataError.errorDisplayMessage = "There seems to be a problem with the server - please try again later"
        break;
      default:
        dataError.errorCode = 0;
        dataError.errorMessage = "Error occured while sending request"
        dataError.errorDisplayMessage = "Unable to retrieve data"
        break;
    }
    return throwError(dataError)
  }
}
