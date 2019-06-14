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

  listRoutesNewRoute: any;
  selectedRouteNewRoute: any;
  stations: any;
  selectedRouteDel: any;
  listRoutes : any;
  selectedRouteAddStation : any;
  listRoutesAddStation: any;

  //
  infoMessage : any;

  sType: any;
  sRoute : any;
  sXY: any;
  sXYDot: any[];

  deleteStationForm = this.fb.group({
    IdStation: [''],
    RouteNumber: ['', Validators.required],
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
    NumberInRoute: ['']
  });

  constructor(private fb: FormBuilder, private serverConnectionService: ServerConnectionService) { }

  get f() { return this.changeStationForm.controls; }
  get d() { return this.addStationForm.controls; }

  ngOnInit() {
    this.sXYDot = [];
    this.newStation = true;
    this.newStations = true;
    this.changeStationbool = false;
    this.deleteStationAdminbool = false;
    this.newStationbool = false;
    this.newStationsbool = false;
    this.getAllStations();
    this.infoMessage="";
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


    this.serverConnectionService.getNewRoutes().subscribe(
      (res) => {
        this.listRoutesNewRoute = res;
        this.selectedRouteNewRoute = this.listRoutesNewRoute[0];
      });
  }

  XYData(data: any)
  {
    this.sXY = data;
  }

  XYDataDot(data: any)
  {
    console.log(data);
    this.sXYDot.push(data);
  }

  onSubmitSaveChanges()
  {
    //
    console.log(this.changeStationForm.value);
    this.serverConnectionService.onSubmitSaveChanges(this.changeStationForm.value).subscribe(
      (res) => {
        console.log(res);
      });
  }

  onSubmitSaveDelete()
  {//
    let p = this.deleteStationForm.value;

    let pom =  this.stations.filter(function(s) {
    return s.IdStation == p.IdStation;
    });

    let pom2 = 0;
    let bb = true;

    pom[0].RouteNumbers.forEach(element => {
        if(element == p.RouteNumber)
        {
          bb = false;
        }

        if(bb)
        {
          pom2 += 1;
        }
    });

    let fpom = pom[0].IdRoute[pom2];

   p.RouteNumber = fpom;

   this.serverConnectionService.deleteStationFromRoute(p).subscribe(
    (res) => {
      console.log(res);
    });
  }

  onSubmitAddStation()
  {////hlper

    if(this.sXY){
    let routeNumberPom:any;
    //let idRoutePom:any;
    let pom :any[];
    pom = [];
    pom.push(this.addStationForm.value["RouteNumbers"]);

    this.addStationForm.value["RouteNumbers"] = pom;
   // idRoutePom =  this.addStationForm.value["RouteNumber"]["Id"];
    routeNumberPom = this.addStationForm.value["RouteNumber"]["RouteNumber"];
    pom = [];
    pom.push(this.selectedRouteAddStation.Id);
    //this.addStationForm["RouteNumber"].setValue(routeNumberPom);
    this.addStationForm.controls['RouteNumber'].setValue(routeNumberPom);
    this.addStationForm.controls['IdRoute'].setValue(pom);
    //NumberInRoute
    this.addStationForm.controls['NumberInRoute'].setValue(this.addStationForm.value["RouteNumbers"][0]);
    this.addStationForm.controls['X'].setValue(this.sXY.X);
    this.addStationForm.controls['Y'].setValue(this.sXY.Y);
    console.log(this.addStationForm.value);

    this.serverConnectionService.addStation(this.addStationForm.value).subscribe(
      (res) => {
        console.log(res);
      });
    }else
    {
      this.infoMessage = "You must select station position on map.";
    }
  }

  onSubmitSaveRouteLines()
  {
    //sXYDot
    if(this.sXYDot.length > 1){
    console.log(this.sXYDot);
    console.log(this.selectedRouteNewRoute);
    //selectedRouteNewRoute

    //nova klasa in string lista 
    let pom = {"Id": this.selectedRouteNewRoute.Id, "RouteNumber":this.selectedRouteNewRoute.RouteNumber,
  "Dots":this.sXYDot  }
    console.log(pom);
    this.serverConnectionService.addLines(pom).subscribe(
      (res) => {
        console.log(res);
      });
    }else
    {
      this.infoMessage = "You must select at least 2 dots.";
    }
  }
}
