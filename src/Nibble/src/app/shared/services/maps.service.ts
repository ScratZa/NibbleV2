import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { GeocodeResponse } from '@mapbox/mapbox-sdk/services/geocoding';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class MapsService {

  geoCodeEndpoint = 'geocoding/v5/mapbox.places/';

  constructor(private httpClient: HttpClient) {

  }

  getMapResponse(address: string) : Observable<GeocodeResponse> {
    let url = this.BuildGeoCodeUrl(address);
    let defaultParams = this.BuildDefaultParams();
    return this.httpClient.get<GeocodeResponse>(url, {params : defaultParams})
  }

  private BuildGeoCodeUrl(value:string) : string {
      return environment.mapbox.origin + this.geoCodeEndpoint + value.toLowerCase() + '.json'
  }

  private BuildDefaultParams(): HttpParams{
    return new HttpParams()
    .append("access_token", environment.mapbox.accessToken)
    .append("country", "ZA")
    .append("types", "address")
  }

}
