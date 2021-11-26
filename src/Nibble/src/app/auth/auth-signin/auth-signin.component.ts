import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormFieldTypes } from '@aws-amplify/ui-components';
import awsmobile from '../../../aws-exports';
import Amplify, { Auth } from 'aws-amplify';

Amplify.configure(awsmobile);
Auth.configure(awsmobile)
@Component({
  selector: 'app-auth-signin',
  templateUrl: './auth-signin.component.html',
  styleUrls: ['./auth-signin.component.scss']
})
export class AuthSigninComponent implements OnInit {
  formFields: FormFieldTypes;
  constructor(private ref:ChangeDetectorRef) {
    this.formFields = [
      {type: 'username'},
      {type: 'name'},
      {type: 'family_name'},
      {type: 'password'}
    ]
   }

  ngOnInit(): void {
  }

}
