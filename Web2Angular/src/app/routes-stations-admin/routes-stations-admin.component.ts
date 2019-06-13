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

  stations: any;
  selectedRouteDel: any;
  listRoutes : any;
  selectedRouteAddStation : any;
  listRoutesAddStation: any;

  sType: any;
  sRoute : any;
  sXY: any;

  deleteStationForm = this.fb.group({
    IdStation: [''],
    DeleteStationRoute: ['', Validators.required],
  });

  changeStationForm = this.fb.group({
    IdStation: [''],
    Name: ['', Validators.required],
  });

  addStationForm = this.fb.group({
    Name: ['', Validators.required],
    Address: ['', Validators.required],
    RouteNumber: ['', Validators.required],
    IdRoute: ['', Validators.required],
    X: ['', Validators.required],
    Y: ['', Validators.required],
    RouteNumbers : ['', Validators.required],
  });

  constructor(private fb: FormBuilder, private serverConnectionService: ServerConnectionService) { }

  get f() { return this.changeStationForm.controls; }
  get d() { return this.addStationForm.controls; }

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

    this.serverConnectionService.getRoutesAddStation().subscribe(
      (res) => {
            this.listRoutesAddStation = res;
            this.selectedRouteAddStation = this.listRoutesAddStation[0];
            this.mapDrow(this.listRoutesAddStation[0].Id);
            this.sType = true;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  mySelectHandler()
  {
    this.mapDrow(this.selectedRouteAddStation.Id);
    this.sType = true;
  }

  mapDrow(id: any)
  {
    this.serverConnectionService.getRoute(id).subscribe(
      (res) => {
        this.sRoute = res;
      });
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

  XYData(data: any)
  {
    this.sXY = data;
  }

  onSubmitSaveChanges()
  {

  }

  onSubmitSaveDelete()
  {

  }

  onSubmitAddStation()
  {

  }
}
