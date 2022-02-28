import { Component } from '@angular/core';

//import { RoomService } from '../services/room-service.js'

import { RoomService } from '../services/room-service';
import * as L from 'leaflet';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'Door2Door-Web';
  destinations = [];

  
  ngAfterViewInit() {
    console.log('ngAfterViewInit();')
    //var map = L.map('map').setView([51.505, -0.09], 1);
    var map = L.map('map').setView([51.505,-0.09], 2);

    

    var tileLayer = L.tileLayer('/assets/bgang-floorplan.png?s={s}&x={x}&y={y}&z={z}', {
      attribution: '&copy; Door2Door project'
    });

    map.addLayer(tileLayer);
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
