import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ConfirmPasswordValidator } from './confirm-password.validator';
import { ServerConnectionService } from '../server-connection.service';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm:FormGroup;
  submitted:boolean = false;
  serverSuccessMessage = "";

  constructor(private fb: FormBuilder,private ServerConnectionService: ServerConnectionService, private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.registerForm = this.fb.group({
      Email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      confirmPassword:['', Validators.required],
      Name: ['',Validators.required],
      Lastname: ['',Validators.required],
      Address_StreetName: ['',Validators.required],
      Address_StreetNumber: ['',Validators.required],
      Address_City: ['',Validators.required],
      Birthday:['',Validators.required],
      UserType:['']
    },{
      validator: MustMatch('password', 'confirmPassword')
    })};

    get f() { return this.registerForm.controls; }
  onSubmit() {
    //console.warn(this.registerForm.value);
    let poruka = this.ServerConnectionService.Register(this.registerForm.value).subscribe(
      (res) => {
        this.serverSuccessMessage = res;
      }
    );
    this.submitted = true;
    this.router.navigate(['/login']);
  }
}

export function MustMatch(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
          // return if another validator has already found an error on the matchingControl
          return;
      }

      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
          matchingControl.setErrors({ mustMatch: true });
      } else {
          matchingControl.setErrors(null);
      }
  }
}