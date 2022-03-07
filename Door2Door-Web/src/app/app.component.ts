import { Component } from '@angular/core';

//import { RoomService } from '../services/room-service.js'

import { RoomService } from '../services/room-service';
import * as L from 'leaflet';
import { Geometry } from './model/Geometry';
import { GeometryType } from './model/GeometryType';
import { Room } from './model/Room';
import { RoomType } from './model/RoomType';
import { PointOfEntry } from './model/PointOfEntry';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'Door2Door-Web';

  private apiUrlIsDefined = false;

  isApiUrlDefined() {
    return this.apiUrlIsDefined;
  }

  destinations = [];

  ngAfterViewInit() {
    console.log('ngAfterViewInit();');

    // This initializes the LeafLet map to bear near Ringsted, DK.
    var map = L.map('map', {
      dragging: true, // This enables the user to drag the map.
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
    let floormapPane = map.createPane('floormapPane');
    floormapPane.style.zIndex = '400';

    let routePane = map.createPane('routePane');
    routePane.style.zIndex = '500';

    var floormapImage = 'assets/bgang_georeferenced.png';

    var floormapBounds = new L.LatLngBounds(
      [55.42712721487636, 11.783205007787574],
      [55.427610656891154, 11.784297446598194]
    );

    // Create the floor map as an image overlay.
    var floormapLayer = L.imageOverlay(floormapImage, floormapBounds, {
      pane: 'floormapPane',
    });

    var floorGeoJsonTest =
      '{"type":"MultiPolygon","coordinates":[[[[11.783210735,55.427279538],[11.783264409,55.42739771],[11.783433952,55.427370828],[11.78348503,55.427481634],[11.783419047,55.427492277],[11.783464599,55.427589399],[11.784288218,55.427467759],[11.784130461,55.427129638],[11.783210735,55.427279538]]]]}';

    var floorGeoJson = JSON.parse(floorGeoJsonTest);

    var geoJsonLayer = L.geoJSON(floorGeoJson, {
      pane: 'routePane',
      //style:
    });

    //markerLocation = new LatLngExp
    //11.78422327,55.42739002
    var icon = new L.Icon.Default();
    icon.options.shadowSize = [0, 0];
    icon.options.imagePath = 'marker-icon.png';

    var marker = L.marker([55.42739002, 11.78422327], {
      title: 'You are here.',
      pane: 'routePane',
    });

    marker.addTo(map);

    featureGroup.addLayer(geoJsonLayer);
    featureGroup.addLayer(floormapLayer);

    map.addLayer(floormapLayer);
    map.addLayer(geoJsonLayer);
  }

  constructor(private _roomService: RoomService) {
    console.log('app.component: constructed');

    console.log('API URL IS : ' + environment.apiUrl);

    if (environment.apiUrl) {
      this.apiUrlIsDefined = true;
    }

    this._roomService
      .GetRoomById(5)
      .then((data) => {
        console.log('the fetched room data is:');
        console.log(data);
      })
      .catch((error) => {
        alert('Something went wrong while trying to load a Room.');
      });

    this._roomService
      .GetAllRooms()
      .then((data) => {
        console.log('all rooms:');
        console.log(data);

        let options = data.map((room) => {
          return {
            value: room.id,
            label: room.name,
          };
        });

        this.destinations = options;
      })
      .catch((error) => {
        alert('Something went wrong while trying to load all rooms...');
      });

    // Load rooms.
    /*this._roomService.GetAllRooms()
      .then((response) => {
        // Map rooms to select options.
        let options = response.map((room) => {
          return {
            value: room.Id,
            label: room.Name,
          };
        });

        this.destinations = options;
        console.log(response);
      })
      .catch((error) => {
        console.error(error);
        alert('Something went wrong.');
      });*/
  }
}
//Test comment
