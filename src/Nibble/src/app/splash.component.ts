import { Component, OnInit } from '@angular/core';
import * as mapboxgl from 'mapbox-gl';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-splash',
  templateUrl: './splash.component.html',
  styleUrls: ['./splash.component.scss']
})
export class SplashComponent implements OnInit {
  map: mapboxgl.Map;
  style = 'mapbox://styles/scratza/ckwggo6v80axp14mmn8ql12l4';
  lat = -26.137147620488772;
  lng = 28.18318863702289;
  constructor() { }

  ngOnInit(): void {
    this.map = new mapboxgl.Map({
      container: 'map',
      accessToken: environment.mapbox.accessToken,
      style: this.style,
      zoom: 13,
      center: [this.lng, this.lat]
  });
  }

}
