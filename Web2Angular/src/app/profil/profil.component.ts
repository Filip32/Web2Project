import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ConfirmPasswordValidator } from './confirm-password.validator';
import { ServerConnectionService } from '../server-connection.service';

@Component({
  selector: 'app-profil',
  templateUrl: './profil.component.html',
  styleUrls: ['./profil.component.css']
})
export class ProfilComponent implements OnInit {

  profileForm: FormGroup;
  submitted: boolean = false;
  serverSuccessMessage = "";

  constructor(private fb: FormBuilder, private ServerConnectionService: ServerConnectionService) { }

  ngOnInit() {
    this.profileForm = this.fb.group({
      Email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      CurrentPassword: ['', Validators.required],
      Name: ['', Validators.required],
      Lastname: ['', Validators.required],
      Address_StreetName: ['', Validators.required],
      Address_StreetNumber: ['', Validators.required],
      Address_City: ['', Validators.required],
      Birthday: ['', Validators.required],
      UserType: ['']
    }, {
        validator: MustMatch('password', 'confirmPassword')
      })

    this.getProfileData();

  };

  get f() { return this.profileForm.controls; }


  onSubmit() {
    let poruka = this.ServerConnectionService.UpdateProfile(this.profileForm.value).subscribe(
      (res) => {
        this.serverSuccessMessage = res;
      }
    );
    this.submitted = true;
  }

  getProfileData() {
    this.ServerConnectionService.getProfileData().subscribe(
      (res) => {
        console.log(res);
       this.profileForm.controls['Email'].setValue(res["Username"]);
       this.profileForm.controls['Name'].setValue(res["Name"]);
       this.profileForm.controls['Lastname'].setValue(res["Lastname"]);
       this.profileForm.controls['Birthday'].setValue(res["SendBackBirthday"]);
       this.profileForm.controls['Address_City'].setValue(res["City"]);
       this.profileForm.controls['Address_StreetName'].setValue(res["StreetName"]);
       this.profileForm.controls['Address_StreetNumber'].setValue(res["StreetNumber"]);
       this.profileForm.controls['UserType'].setValue(res["UserType"]);

      }
    );
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
