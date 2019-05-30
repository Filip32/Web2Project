import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
 
  constructor(private fb: FormBuilder) {

   }

    loginForm = this.fb.group({
      email: ['', Validators.required, Validators.email],
      password: ['', Validators.required],
    });
       ngOnInit() {
  }

  onSubmit() {
    console.warn(this.loginForm.value);
  }

  get f() { return this.loginForm.controls; }

}
