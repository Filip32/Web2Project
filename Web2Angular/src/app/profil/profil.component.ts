import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ConfirmPasswordValidator } from './confirm-password.validator';

@Component({
  selector: 'app-profil',
  templateUrl: './profil.component.html',
  styleUrls: ['./profil.component.css']
})
export class ProfilComponent implements OnInit {

  profileForm:FormGroup;
  submitted:boolean = false;
 
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.profileForm = this.fb.group({
      Email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      confirmPassword:['', Validators.required],
      Name: ['',Validators.required],
      Lastname: ['',Validators.required],
      Address_StreetName: ['',Validators.required],
      Address_StreetNumber: ['',Validators.required],
      Address_Zipcode: ['',Validators.required],
      Birthday:['',Validators.required],
      UserType:['']
    },{
      validator: MustMatch('password', 'confirmPassword')
    })};

    get f() { return this.profileForm.controls; }
  onSubmit() {
    console.warn(this.profileForm.value);
    this.submitted = true;
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