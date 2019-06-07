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
    //this.serverConnectionService.notify();
    this.checkConnection();
    this.subscribeForTime();
 }

 selectedRoute(route: any)
 {
    this.isSelected = route;
    this.serverConnectionService.getRoute(route.Id).subscribe(
      (res) => {
        console.log(res);
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
  this.notifService.registerForTimerEvents().subscribe(e => this.onTimeEvent(e));
}

public onTimeEvent(time: string){
  this.ngZone.run(() => { 
      this.time = time; 
    //  console.log(this.time);
    //  console.log(++this.counter)
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
