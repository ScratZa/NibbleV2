import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { CognitoUserPool } from 'amazon-cognito-identity-js';
import { environment } from '../../../environments/environment';
@Component({
  selector: 'app-auth-signup',
  templateUrl: './auth-signup.component.html',
  styleUrls: ['./auth-signup.component.scss']
})
export class AuthSignupComponent implements OnInit {
  isLoading:boolean = false;
  fname:string = '';
  lname:string = '';
  email:string = '';
  password:string = '';

  constructor(private router:Router) { }

  ngOnInit(): void {
  }


}
