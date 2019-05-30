import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private fb: FormBuilder) { }

  registerForm = this.fb.group({
    Email: ['', Validators.required],
    Password: [''],
    PasswordR:[''],
    Name: [''],
    Lastname: [''],
    Address: [''],
    Birthday:[''],
    UserType:['']
  });

  ngOnInit() {
  }

  onSubmit() {
    console.warn(this.registerForm.value);
  }
}
