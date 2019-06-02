import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-routes-stations-admin',
  templateUrl: './routes-stations-admin.component.html',
  styleUrls: ['./routes-stations-admin.component.css']
})
export class RoutesStationsAdminComponent implements OnInit {
  routesStationsForm:FormGroup;
  submitted:boolean = false;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.routesStationsForm = this.fb.group({
      StationXcoordinate: ['', Validators.required],
      StationYcoordinate: ['', Validators.required],
      StationName:['', Validators.required],
      AddressStreetName: ['',Validators.required],
      AddressStreetNumber: ['',Validators.required],
      AddressZipcode: ['',Validators.required],
    })};
  
  onSubmit() {
    console.warn(this.routesStationsForm.value);
    this.submitted = true;
  }

}
