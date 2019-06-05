import { Component, OnInit } from '@angular/core';
import { ServerConnectionService } from '../server-connection.service';

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
    this.sRoute = route;
    console.log(route);
 }

 getRoutes()
 {
     this.serverConnectionService.getRoutes().subscribe(
      (res) => {
          this.routes = res;
      });
 }
}
