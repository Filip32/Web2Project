import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainService } from '../main.service';
import {HttpClient, HttpHeaders} from '@angular/common/http'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
 
  constructor(private fb: FormBuilder, private mainService: MainService, private http: HttpClient) {

   }

    loginForm = this.fb.group({
      email: ['', [Validators.email, Validators.required]],
      password: ['', Validators.required],
    });
       ngOnInit() {
  }

  onSubmit() {
    let poruka = this.mainService.login(this.loginForm.value).subscribe(
      (res) => {
        console.log(res);
      }
    );
  }

  get f() { return this.loginForm.controls; }

}
