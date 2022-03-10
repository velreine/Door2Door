import { Component } from '@angular/core';
import { RoomService } from '../services/room-service';
import { RouteService } from 'src/services/route-service';
import * as L from 'leaflet';
import { FormBuilder } from '@angular/forms';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'Door2Door-Web';

  private apiUrlIsDefined = false;

  private _map: L.Map;
  private _routePane: HTMLElement;
  private _markerPane: HTMLElement;

  // private _markerCoordinates: []

  isApiUrlDefined() {
    return this.apiUrlIsDefined;
  }

  public destinationForm = this._formBuilder.group({
    destinationId: '',
  });

  public destinations = [];

  ngAfterViewInit() {
    console.log('ngAfterViewInit()...');
    this.initializeLeafletMap();
    this.initializeAllRooms();
  }

  onSubmit(): void {
    let data = this.destinationForm.value;

    // Ensure a destination is selected.
    if (!data.destinationId) {
      alert('Please select a destination first.');
      return;
    }

    // If all is good, invoke the API.
    this._routeService
      .GetRouteTo(data.destinationId)
      .then((response) => {
        console.table(response);

        // Clear the "route" pane, so the old route is effectively removed.
        this._map.eachLayer((layer) => {
          if (layer.getPane() === this._routePane) {
            this._map.removeLayer(layer);
          }
        });

        response.Geometry.forEach((geoJsonObject) => {

          // Construct Geo JSON Layer.
          // Flip coordinates to order the used by Leaflet.
          const layer = L.geoJSON(geoJsonObject, {
            pane: 'routePane',
          });

          // Add this layer to the map.
          this._map.addLayer(layer);
        });
      })
      .catch((error) => {
        console.error(error);
        alert('Something went wrong, it was not possible to generate a route.');
      });
  }

  constructor(
    private _roomService: RoomService,
    private _routeService: RouteService,
    private _formBuilder: FormBuilder
  ) {
    console.log('app.component: constructed');

    console.log('API URL IS : ' + environment.apiUrl);

    if (environment.apiUrl) {
      this.apiUrlIsDefined = true;
    }

    //this.initializeAllRooms();
  }

  private initializeLeafletMap() {
    console.log('initializeLeafletMap()...');
    // This initializes the LeafLet map to bear near Ringsted, DK.
    this._map = L.map('map', {
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
    let floormapPane = this._map.createPane('floormapPane');
    floormapPane.style.zIndex = '400';

    this._routePane = this._map.createPane('routePane');
    this._routePane.style.zIndex = '500';

    this._markerPane = this._map.createPane('markerPane');
    this._markerPane.style.zIndex = '550';

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

    // Gets the 'you are here' marker.
    this.getStartingPoint()

    featureGroup.addLayer(geoJsonLayer);
    featureGroup.addLayer(floormapLayer);

    this._map.addLayer(floormapLayer);
    //this._map.addLayer(geoJsonLayer); TODO : Remove when testing is done.
  }

  private initializeAllRooms() {
    console.log('am i invoked?');
    this._roomService
      .GetAllRooms()
      .then((data) => {
        console.log('all rooms:');
        console.log(data);

        let options = data.map((room) => {
          return {
            value: room.id,
            label: room.name + ' - ' + room.type.name,
          };
        });

        console.log(options);

        this.destinations = options;
      })
      .catch((error) => {
        alert('Something went wrong while trying to load all rooms...');
      });
  }
  private getStartingPoint() {
    const id = 1; // Hard code for testing.
    this._routeService
    .GetStatringPoint(id)
    .then((response) => {

      L.geoJSON(response, {
        pointToLayer: function (feature, latlng) {
          return L.marker(latlng, {
            title: 'You are here.',
            pane: 'markerPane'
          })
        }
      }).addTo(this._map);
    });
  }
}
