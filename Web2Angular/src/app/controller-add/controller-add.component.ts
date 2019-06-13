import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService } from '../auth.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ServerConnectionService } from '../server-connection.service';

@Component({
  selector: 'app-controller-add',
  templateUrl: './controller-add.component.html',
  styleUrls: ['./controller-add.component.css']
})
export class ControllerAddComponent implements OnInit {

  constructor(private fb: FormBuilder, private authService: AuthService,private ServerConnectionService: ServerConnectionService, private http: HttpClient, private router: Router) { }

  ngOnInit() {
  }

  controllerForm = this.fb.group({
    email: ['', [Validators.email, Validators.required]],
    password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
  }, {
    validator: MustMatch('password', 'confirmPassword')
  });

  get f() { return this.controllerForm.controls; }
  onSubmit() {
    //console.warn(this.registerForm.value);
    //this.registerForm.append('photo', this.selectedFile, this.selectedFile.name);
    if (this.controllerForm.valid) {
      let poruka = this.ServerConnectionService.Register(this.controllerForm.value).subscribe(
        (res) => {

        }
      );
    }else
    {
      Object.keys(this.controllerForm.controls).forEach(field => {
        const control = this.controllerForm.get(field);
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