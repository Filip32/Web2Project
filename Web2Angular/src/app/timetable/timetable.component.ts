import { Component, OnInit } from '@angular/core';
import { ServerConnectionService } from '../server-connection.service';

@Component({
  selector: 'app-timetable',
  templateUrl: './timetable.component.html',
  styleUrls: ['./timetable.component.css']
})
export class TimetableComponent implements OnInit {

  selectedTypeOfTimetable : any;
  selectedTypeOfDay: any;
  selectedRoute: any;
  routes: any;
  departures: any;

  constructor(private serverConnectionService: ServerConnectionService) { }

  ngOnInit() {
  }

  ChangeRoutes()
  {
    this.selectedRoute = undefined;
    this.departures = [];
    if (this.selectedTypeOfTimetable == 'City') {
      if (this.selectedTypeOfDay == 'Workday') {
        this.getRoutesCityWorkday();
      }
      else if (this.selectedTypeOfDay == 'Saturday') {
        this.getRoutesCitySaturday();
      }
      else {
        this.getRoutesCitySunday();
      }
    }
    else {
      if (this.selectedTypeOfDay == 'Workday') {
        this.getRoutesSuburbanWorkday();
      }
      else if (this.selectedTypeOfDay == 'Saturday') {
        this.getRoutesSuburbanSaturday();
      }
      else {
        this.getRoutesSuburbanSunday();
      }
    }
  }

  ChangeByDay()
  {
    this.selectedRoute = undefined;
    this.departures = [];
    if (this.selectedTypeOfDay == 'Workday') {
      if (this.selectedTypeOfTimetable == 'City') {
        this.getRoutesCityWorkday();
      }
      else {
        this.getRoutesSuburbanWorkday();
      }
    }
    else if (this.selectedTypeOfDay == 'Saturday') {
      if (this.selectedTypeOfTimetable == 'City') {
        this.getRoutesCitySaturday();
      }
      else {
        this.getRoutesSuburbanSaturday();
      }
    }
    else {
      if (this.selectedTypeOfTimetable == 'City') {
        this.getRoutesCitySunday();
      }
      else {
        this.getRoutesSuburbanSunday();
      }
    }
  }

  getRoutesCityWorkday() {
    this.serverConnectionService.getRoutesCityWorkday().subscribe(
      (res) => {
        this.routes = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  getRoutesCitySaturday() {
    this.serverConnectionService.getRoutesCitySaturday().subscribe(
      (res) => {
        this.routes = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  getRoutesCitySunday() {
    this.serverConnectionService.getRoutesCitySunday().subscribe(
      (res) => {
        this.routes = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }



  getRoutesSuburbanWorkday() {
    this.serverConnectionService.getRoutesSuburbanWorkday().subscribe(
      (res) => {
        this.routes = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  getRoutesSuburbanSaturday() {
    this.serverConnectionService.getRoutesSuburbanSaturday().subscribe(
      (res) => {
        this.routes = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  getTimetable()
  {
    if(this.selectedTypeOfTimetable && this.selectedTypeOfDay && this.selectedRoute){
        let par = {
          timetable: this.selectedTypeOfTimetable,
          day: this.selectedTypeOfDay,
          route: this.selectedRoute
        };

        this.serverConnectionService.getTimetable(par).subscribe(
          (res) => {
            this.departures = res;
          },
          (err) => {
            console.error(err);
          }
        );
        }
  }

  getRoutesSuburbanSunday() {
    this.serverConnectionService.getRoutesSuburbanSunday().subscribe(
      (res) => {
        this.routes = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }


}
