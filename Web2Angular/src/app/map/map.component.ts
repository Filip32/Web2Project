import { Component, OnInit, Input, NgZone } from '@angular/core';
import { MarkerInfo } from './model/marker-info.model';
import { GeoLocation } from './model/geolocation';
import { Polyline } from './model/polyline';
import { Marker } from '@agm/core/services/google-maps-types';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  styles: ['agm-map {height: 555px; width: 682px;}'] //postavljamo sirinu i visinu mape
})
export class MapComponent implements OnInit {
  public polyline: Polyline;
  public stationsIcon: MarkerInfo[];
  public zoom: number;
  private _route: any;

  ngOnInit() {
    this.polyline= new Polyline([], '#9966ff', { url:"", scaledSize: {width: 50, height: 50}});
  }

  constructor(private ngZone: NgZone){
  }

  @Input()
  set route(route: any) {
    this._route = route;
    if(this._route){
        this.drowRoutes();
    }
  }

  drowRoutes()
  {
    this.polyline= new Polyline([], '#9966ff', { url:"", scaledSize: {width: 50, height: 50}});
    this.stationsIcon = [];
    this._route.Stations.forEach(element => {
      if(!element.IsStation)
      {
        this.polyline.addLocation(new GeoLocation(element.X, element.Y));
      }
      else
      {
        this.stationsIcon.push(new MarkerInfo(new GeoLocation(element.X, element.Y), "assets/busicon.png",
        element.Name , element.Address.City +  ", "+ element.Address.StreetName + ", " + element.Address.StreetNumber,""));
      }
    });
  }

  placeMarker($event){
    if(localStorage.role == "Admin"){
      this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng));
      console.log(this.polyline);
    }
  }
}
