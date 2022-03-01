import { Component } from '@angular/core';

//import { RoomService } from '../services/room-service.js'

import { RoomService } from '../services/room-service';
import * as L from 'leaflet';
import Geometry from './model/Geometry';
import GeometryType from './model/GeometryType';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'Door2Door-Web';
  destinations = [];

  ngAfterViewInit() {
    console.log('ngAfterViewInit();');

    var map = L.map('map', {
      dragging: true,
      zoomControl: true,
      maxZoom: 20,
      minZoom: 1,
    }).fitBounds([
      [55.4271372879558, 11.783122828667683],
      [55.42761923303025, 11.784800721781808],
    ]);

    // Create a new feature group to contain our layers.
    // Layers in the feature group as the name suggests shares features
    // For our project this means the layers shares Lattitude/Longittude bounds.
    var featureGroup = L.featureGroup([]);

    // Create a new pane and set its Z-Index
    let floormap = map.createPane('floormap');
    floormap.style.zIndex = '400';

    var floormapImage = 'assets/bgang-floorplan.png';
    var floormapBounds = new L.LatLngBounds(
      [55.42712721487636, 11.783205007787574],
      [55.427610656891154, 11.784297446598194]
    );

    var floormapLayer = L.imageOverlay(floormapImage, floormapBounds, {
      pane: 'floormap',
    });

    //let generatedRouteData = Anton.CallApiAndGetRoute('');
    //var generatedRouteLayer = L.geoJSON
    let m = new Geometry(GeometryType.Door, 'B26');

    L.geoJSON();

    featureGroup.addLayer(floormapLayer);

    map.addLayer(floormapLayer);

    /*var tileLayer = L.tileLayer(
      '/assets/bgang-floorplan.png?s={s}&x={x}&y={y}&z={z}',
      {
        attribution: '&copy; Door2Door project',
      }
    );

    map.addLayer(tileLayer);*/
  }

  constructor() {
    console.log('app.component: constructed');

    // Load rooms.
    RoomService.GetAllRoomsMock()
      .then((response) => {
        this.destinations = response;
        console.log(response);
      })
      .catch((error) => {
        console.error(error);
        alert('Something went wrong.');
      });
  }
}
//Test comment
