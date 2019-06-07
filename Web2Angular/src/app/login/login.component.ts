import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  serverSuccessMessage = "";

  constructor(private fb: FormBuilder, private authService: AuthService, private http: HttpClient, private router: Router) {

   }

    loginForm = this.fb.group({
      email: ['', [Validators.email, Validators.required]],
      password: ['', Validators.required],
    });
       ngOnInit() {
  }

  onSubmit() {
    let poruka = this.authService.login(this.loginForm.value).subscribe(
      (res) => {
        if(localStorage.role == "AppUser")
        {
          this.router.navigate(['/']);
        } 
        else if(localStorage.role == "Admin")
        {
          this.router.navigate(['/routes-stations-a']);
        }
        else if(localStorage.role == "Controller")
        {
          this.router.navigate(['/verification-c']);
        }else
        {
          this.serverSuccessMessage="You have entered wrong username/password";
        }
      }
    );
  }

  get f() { return this.loginForm.controls; }

}
