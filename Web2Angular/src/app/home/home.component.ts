import { Component, OnInit } from '@angular/core';

import Map from 'ol/Map';
import XYZ from 'ol/source/XYZ';
import TileLayer from 'ol/layer/Tile';
import View from 'ol/View';
import { fromLonLat } from 'ol/proj'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

 map: Map;
 source: XYZ;
 layer: TileLayer;
 view: View;

 ngOnInit() {
   this.source = new XYZ({
     url: 'http://tile.osm.org/{z}/{x}/{y}.png'
   });

   this.layer = new TileLayer({
     source: this.source
   });

   this.view = new View({
     center: fromLonLat([19.8266243, 45.245861]),
     zoom: 13
   });

   this.map = new Map({
     target: 'map',
     layers: [this.layer],
     view: this.view
   });

  
}
}
