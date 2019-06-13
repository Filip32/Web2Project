import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ServerConnectionService } from '../server-connection.service';

@Component({
  selector: 'app-routes-stations-admin',
  templateUrl: './routes-stations-admin.component.html',
  styleUrls: ['./routes-stations-admin.component.css']
})
export class RoutesStationsAdminComponent implements OnInit {
  newStation: boolean;
  newStations: boolean;
  changeStationbool: boolean;
  deleteStationAdminbool: boolean;
  newStationbool: boolean;
  newStationsbool: boolean;

  stations; any;
  selectedRouteDel: any;
  listRoutes :any;

  deleteStationForm = this.fb.group({
    IdStation: [''],
    DeleteStationRoute: ['', Validators.required],
  });

  changeStationForm = this.fb.group({
    IdStation: [''],
    Name: ['', Validators.required],
  });

  constructor(private fb: FormBuilder, private serverConnectionService: ServerConnectionService) { }

  get f() { return this.changeStationForm.controls; }
  get d() { return this.deleteStationForm.controls; }

  ngOnInit() {
    this.newStation = true;
    this.newStations = true;
    this.changeStationbool = false;
    this.deleteStationAdminbool = false;
    this.newStationbool = false;
    this.newStationsbool = false;
    this.getAllStations();
  }
  
  getAllStations()
  {
    this.serverConnectionService.getStationsAdmin().subscribe(
      (res) => {
        this.stations = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  changeStation(station: any)
  {
    this.changeStationForm.controls['IdStation'].setValue(station["IdStation"]);
    this.changeStationForm.controls['Name'].setValue(station["Name"]);
    this.newStation = true;
    this.newStations = true;
    this.changeStationbool = true;
    this.deleteStationAdminbool = false;
    this.newStationbool = false;
    this.newStationsbool = false;
  }

  deleteRouteAdmin(station: any)
  {
    this.listRoutes = station.RouteNumbers;
    this.deleteStationForm.controls['IdStation'].setValue(station["IdStation"]);
    this.selectedRouteDel =  this.listRoutes[0];
    this.newStation = true;
    this.newStations = true;
    this.changeStationbool = false;
    this.deleteStationAdminbool = true;
    this.newStationbool = false;
    this.newStationsbool = false;
  }

  addNewStation()
  {
    this.newStation = false;
    this.newStations = true;
    this.changeStationbool = false;
    this.deleteStationAdminbool = false;
    this.newStationbool = true;
    this.newStationsbool = false;
  }

  addNewStations()
  {
    this.newStation = true;
    this.newStations = false;
    this.changeStationbool = false;
    this.deleteStationAdminbool = false;
    this.newStationbool = false;
    this.newStationsbool = true;
  }

  onSubmitSaveChanges()
  {

  }

  onSubmitSaveDelete()
  {

  }
}
