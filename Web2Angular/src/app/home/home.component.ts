import { Component, OnInit, Output,NgZone } from '@angular/core';
import { ServerConnectionService } from '../server-connection.service';
import { EventEmitter } from 'protractor';
import { NotificationService } from '../notification.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  routes : any;
  sRoute : any;
  sBus: any;
  isSelected : any = null;
  isConnected: Boolean;
  notifications: string[];
  time: string;
  counter  = 0

  constructor(private ngZone: NgZone,private notifService: NotificationService,private serverConnectionService: ServerConnectionService) { 
    this.isConnected = false;
    this.notifications = [];
  }

 ngOnInit() {
    this.getRoutes();
    this.checkConnection();
    this.subscribeForTime();
 }

 selectedRoute(route: any)
 {
    this.notifService.SendBusRoute(route.RouteNumber);
    this.isSelected = route;
    this.serverConnectionService.getRoute(route.Id).subscribe(
      (res) => {
        this.sRoute = res;
      });
 }

 getRoutes()
 {
     this.serverConnectionService.getRoutes().subscribe(
      (res) => {
          this.routes = res;
      });
 }

 private checkConnection(){
  this.notifService.startConnection().subscribe(e => {this.isConnected = e; 
      if (e) {
        this.notifService.StartTimer()
      }
  });
}

subscribeForTime() {
  this.notifService.registerForTimerEvents().subscribe(
    e => {
      this.onTimeEvent(e);
      this.sBus = e;
      console.log(e);
    });
}

public onTimeEvent(time: string){
  this.ngZone.run(() => { 
      this.time = time; 
  });  
 
}

public startTimer() {
  this.notifService.StartTimer();
}

public stopTimer() {
  this.notifService.StopTimer();
  this.time = "";
}
}
