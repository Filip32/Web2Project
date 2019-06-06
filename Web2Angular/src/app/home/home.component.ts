import { Component, OnInit, Output } from '@angular/core';
import { ServerConnectionService } from '../server-connection.service';
import { EventEmitter } from 'protractor';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  routes : any;
  sRoute : any;
  isSelected : any = null;

  constructor(private serverConnectionService: ServerConnectionService) { }

 ngOnInit() {
   this. getRoutes();
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
}
