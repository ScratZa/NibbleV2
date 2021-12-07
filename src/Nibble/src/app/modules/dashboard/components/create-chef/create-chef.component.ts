import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, NgForm, ValidatorFn, Validators } from '@angular/forms';
import { GeocodeFeature, GeocodeResponse } from '@mapbox/mapbox-sdk/services/geocoding';
import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map, startWith, switchMap } from 'rxjs/operators';
import { MapsService } from 'src/app/shared/services/maps.service';
import {Address} from 'src/app/shared/models/address';
import { CreateChefRequest } from 'src/app/shared/models/create-chef-request';
import { ChefService } from 'src/app/shared/services/chef.service';
import { CreateChefResponse } from 'src/app/shared/models/create-chef-response';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DataRequestError } from 'src/app/shared/models/data-request-error';

function _styleValidator(validValues: string[]): ValidatorFn {
  return (formControl: AbstractControl) : {[key:string]: boolean} | null =>{
    if(!validValues?.includes(formControl.value))
      return {"ValidStyleSelected" : true}

      return null;
  }
}

@Component({
  selector: 'app-create-chef',
  templateUrl: './create-chef.component.html',
  styleUrls: ['./create-chef.component.scss']
})

export class CreateChefComponent implements OnInit {
  @ViewChild('formDirective') private formDirective: NgForm;
  form : FormGroup;
  addresses: Observable<GeocodeFeature[]>;
  selectedAddress: GeocodeFeature;
  cookingStyles : string[] = ['Italian', 'Greek', 'Portugese', 'Indian'];
  filteredStyles: Observable<string[]>;

  constructor(private mapsService: MapsService,
    private chefService : ChefService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar) {

  }

   ngOnInit() {

    this.form = this.formBuilder.group({
      firstName: [null,[Validators.required, Validators.minLength(2)]],
      lastName: [null, [Validators.required, Validators.minLength(2)]],
      cookingStyle: [null, [Validators.required,_styleValidator(this.cookingStyles)]],
      addr: [null, [Validators.required]],
    });

    this.filteredStyles = this.form.get('cookingStyle')!.valueChanges.pipe(
      startWith(''),
      map(value => this._filterStyles(value)),
    );

    this.addresses = this.form.get('addr')!.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      filter((addr) => !this.selectedAddress),
      switchMap(value =>
        {
          return this._filterAddress(value)
        })
    );
  }

  formSubmit(){
    let chef :  CreateChefRequest = <CreateChefRequest> this.form.getRawValue();
    chef = this._updateAddress(chef);
    console.log(chef)

    this.chefService.createChef(chef)
    .subscribe(
      ((data: CreateChefResponse) => {
        this.snackBar.open(`Chef Created with Id ${data.id}`, 'Dismiss');
        this.form.reset();
        this.formDirective.resetForm();
      }),
      (err: DataRequestError) => {
        console.log(err.errorMessage)
      }
    )
  }


  updateSelectedAddress(event: any)
  {
      this.selectedAddress = event.option.value;
      this.form.get('addr').patchValue(this.selectedAddress.place_name)
  }

  private _updateAddress(chef: CreateChefRequest): CreateChefRequest
  {
    chef.address  = <Address> {
        addressName: this.selectedAddress.place_name,
        addressDisplayName: "Default",
        point: this.selectedAddress.geometry.coordinates
     };
     return chef;
 }

  private _filterStyles(style: string) : string[]
  {
     return this._filterStrings(style);
  }

  private _filterStrings(value: string) : string[]
  {
    var lower = value?.toLowerCase();
    return this.cookingStyles.filter(opt => opt.toLowerCase().includes(lower))
  }
  private _filterAddress(value: string)
  {
    const filterValue = value?.toLowerCase();
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
