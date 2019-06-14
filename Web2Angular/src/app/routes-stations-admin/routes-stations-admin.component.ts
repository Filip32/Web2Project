import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ServerConnectionService } from '../server-connection.service';
import { Router } from '@angular/router';

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

  message: any;
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

  constructor(private fb: FormBuilder, private serverConnectionService: ServerConnectionService, private router: Router) { }

  get f() { return this.changeStationForm.controls; }
  get d() { return this.addStationForm.controls; }

  ngOnInit() {
    this.message = "";
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
        this.message = "";
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
    this.message = "";
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
    this.message = "";
  }

  addNewStation()
  {
    this.newStation = false;
    this.newStations = true;
    this.changeStationbool = false;
    this.deleteStationAdminbool = false;
    this.newStationbool = true;
    this.newStationsbool = false;
    this.message = "";

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
    this.message = "";

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
    this.serverConnectionService.onSubmitSaveChanges(this.changeStationForm.value).subscribe(
      (res) => {
        this.message = "Changes are made.";
        setTimeout(() => {
          this.router.navigate(['/routes-stations-a']);
        },
          2000);
      });
  }

  onSubmitSaveDelete()
  {
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
      this.message = "Station is deleted.";
      setTimeout(() => {
        this.router.navigate(['/routes-stations-a']);
      },
        2000);
    });
  }

  onSubmitAddStation()
  {////hlper
    let ii = this.addStationForm.value["Address"];
    let i: any[] = ii.split(',');
    console.log(+i[2]);
    if(i.length == 3 && !isNaN(+i[2]) ){
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
        this.message = "Station is added.";

        setTimeout(() => {
          this.router.navigate(['/routes-stations-a']);
        },
          2000);
      });
    }else
    {
      this.infoMessage = "You must select station position on map.";
    }
  }else
  {
    this.infoMessage = "Address format is not valid.";
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
        this.message = "Line is added.";

        setTimeout(() => {
          this.router.navigate(['/routes-stations-a']);
        },
          2000);
      });
    }else
    {
      this.infoMessage = "You must select at least 2 dots.";
    }
  }
}
