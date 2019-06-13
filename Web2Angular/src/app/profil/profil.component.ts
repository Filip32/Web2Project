import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ConfirmPasswordValidator } from './confirm-password.validator';
import { ServerConnectionService } from '../server-connection.service';
import { NgAnalyzeModulesHost } from '@angular/compiler';

@Component({
  selector: 'app-profil',
  templateUrl: './profil.component.html',
  styleUrls: ['./profil.component.css']
})
export class ProfilComponent implements OnInit {

  profileForm: FormGroup;
  submitted: boolean = false;
  serverSuccessMessage = "";
  UserValid: any;
  typeOfUser: any;
  fileToUpload: File = null;
  photoo : any;
  idd : any;

  constructor(private fb: FormBuilder, private ServerConnectionService: ServerConnectionService) { }

  ngOnInit() {
    this.profileForm = this.fb.group({
      Email: ['', [Validators.required, Validators.email]],
      password: [''],
      confirmPassword: ['' ],
      CurrentPassword: ['', Validators.required],
      Name: ['', Validators.required],
      Lastname: ['', Validators.required],
      Address_StreetName: ['', Validators.required],
      Address_StreetNumber: ['', Validators.required],
      Address_City: ['', Validators.required],
      Birthday: ['', Validators.required],
      UserType: [''],
      Photo: ['']
    }, {
        validator: MustMatch('password', 'confirmPassword')
      })

    this.getProfileData();

  };

  get f() { return this.profileForm.controls; }


 // onSubmit() {
   /* let poruka = this.ServerConnectionService.UpdateProfile(this.profileForm.value).subscribe(
      (res) => {
        this.serverSuccessMessage = res;
      }
    );
    this.submitted = true;*/
    onSubmit() {
    if (this.profileForm.valid) {
      let poruka = this.ServerConnectionService.UpdateProfile(this.profileForm.value).subscribe(
        (res) => {
          this.serverSuccessMessage = res;
          if(res ==="Profile successfully updated.")
          {
            if(this.fileToUpload!=null)
            {
              this.uploadFileToActivity();
            }
          }
        }
      );
      this.submitted = true;
    }
    else {
      Object.keys(this.profileForm.controls).forEach(field => {
        const control = this.profileForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
  } 
 // }
 handleFileInput(files: FileList) {
  this.fileToUpload = files.item(0);
}

uploadFileToActivity() {
  let e = this.profileForm.get("Email").value;
  this.ServerConnectionService.postFile(this.fileToUpload, e).subscribe(data => {
  }, error => {
    console.log(error);
  });
}


  getProfileData() {
    this.ServerConnectionService.getProfileData().subscribe(
      (res) => {
        console.log(res);
       this.profileForm.controls['Email'].setValue(res["Username"]);
       this.idd = res["Username"];
       this.profileForm.controls['Name'].setValue(res["Name"]);
       this.profileForm.controls['Lastname'].setValue(res["Lastname"]);
       this.profileForm.controls['Birthday'].setValue(res["SendBackBirthday"]);
       this.profileForm.controls['Address_City'].setValue(res["City"]);
       this.profileForm.controls['Address_StreetName'].setValue(res["StreetName"]);
       this.profileForm.controls['Address_StreetNumber'].setValue(res["StreetNumber"]);
       this.profileForm.controls['UserType'].setValue(res["UserType"]);
       this.profileForm.controls['Photo'].setValue(res["Photo"]);
       this.typeOfUser = res["UserType"];
       //this.photoo = res["Photo"];
       //getPhoto
       this.getPhoto();
      }
    );

    this.ServerConnectionService.getTypeOfLoginUser().subscribe(
      (res) => {
        let i: string = res;
        let j: any = JSON.parse(i);
        this.UserValid = j.IsValid;
      }
    );

  }

  getPhoto() {
    this.ServerConnectionService.getPhoto(this.idd).subscribe(
      (res) => {
        this.photoo = 'data:image/png;base64,' + res;
      },
      (err) => {
        console.error(err);
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
