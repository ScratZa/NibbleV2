import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { GeocodeFeature, GeocodeResponse } from '@mapbox/mapbox-sdk/services/geocoding';
import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map, startWith, switchMap } from 'rxjs/operators';
import { MapsService } from 'src/app/shared/services/maps.service';
@Component({
  selector: 'app-auto-complete',
  templateUrl: './auto-complete.component.html',
  styleUrls: ['./auto-complete.component.scss']
})
export class AutoCompleteComponent implements OnInit {

  form = this.formBuilder.group({
    addr: [null],
  });
  constructor(private mapsService: MapsService, private formBuilder: FormBuilder) {
    this.addresses = this.form.get('addr')!.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      filter((addr) => !!addr),
      switchMap(value =>
        {
          return this._filter(value)
        })
    );
  }

   addresses: Observable<GeocodeFeature[]>;
   selectedAddress: null;

   ngOnInit() {

  }
  private _filter(value: string){
    const filterValue = value.toLowerCase();
    return this.mapsService.getMapResponse(value)
    .pipe(
      map(x =>
        {
          console.log(x);
          return x.features
        })
    )
  }
}
