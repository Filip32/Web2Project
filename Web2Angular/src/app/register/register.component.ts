import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ConfirmPasswordValidator } from './confirm-password.validator';
import { ServerConnectionService } from '../server-connection.service';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  submitted: boolean = false;
  serverSuccessMessage = "";
  typeOfUser: any;
  fileToUpload: File = null;

  constructor(private fb: FormBuilder, private ServerConnectionService: ServerConnectionService, private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.registerForm = this.fb.group({
      Email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      Name: ['', Validators.required],
      Lastname: ['', Validators.required],
      Address_StreetName: ['', Validators.required],
      Address_StreetNumber: ['', Validators.required],
      Address_City: ['', Validators.required],
      Birthday: ['', Validators.required],
      UserType: [''],
      photo: ['']
    }, {
        validator: MustMatch('password', 'confirmPassword')
      })
      this.typeOfUser = "Regular";
  };

  selectedFile: File = null;
  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  uploadFileToActivity() {
    let e = this.registerForm.get("Email").value;
    this.ServerConnectionService.postFile(this.fileToUpload, e).subscribe(data => {
    }, error => {
      console.log(error);
    });
  }
  

  get f() { return this.registerForm.controls; }
  onSubmit() {
    //console.warn(this.registerForm.value);
    //this.registerForm.append('photo', this.selectedFile, this.selectedFile.name);
    if (this.registerForm.valid) {
      let poruka = this.ServerConnectionService.Register(this.registerForm.value).subscribe(
        (res) => {
  
          if(res === "Successfully registered."){
            if(this.fileToUpload!=null)
            {
              this.uploadFileToActivity();
            }this.serverSuccessMessage = res;
          }else
          {
            this.serverSuccessMessage = res;
            this.router.navigate(['/login']);
          }
        }
      );
      this.submitted = true;
    }else
    {
      Object.keys(this.registerForm.controls).forEach(field => {
        const control = this.registerForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
    
    //this.router.navigate(['/login']);
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