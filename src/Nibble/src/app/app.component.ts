import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public customer?: CustomerResponse;

  constructor(http: HttpClient) {
    // http.get<CustomerResponse>('/api/customer/d960f617-b22b-4c8b-ad03-0aab6e135704').subscribe(result => {
    //   this.customer = result;
    // }, error => console.error(error));
  }

  title = 'Nibble';
}

interface CustomerResponse {
  customer: Customer;
  address: Address;
}
interface Customer {
  name: string ;
  lastName: string;
}
interface Address {
  name: string ;
  latitude: number;
  longitude: number;
}
