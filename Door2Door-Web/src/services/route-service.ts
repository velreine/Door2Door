import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { Route } from 'src/app/model/Route';
import { GeoJsonObject } from 'geojson';

@Injectable({
  providedIn: 'root',
})
export class RouteService {
  private static mockData = [];

  constructor(private _http: HttpClient) {}

  public GetRouteTo(
    /*sourceRoomId: Number,*/ destinationRoomId: Number
  ): Promise<Route> {
    // TODO: Debugging, until the Database supports a from id.
    // 8 should point to B-Gang B.02 (our static starting point for now.)
    const sourceRoomId = 8;

    // For lack of knowledge of a better way to map the data coming straight from the api to our Route model.
    return new Promise<Route>((resolve, reject) => {
      let data = this._http
        .get<any>(environment.apiUrl + '/Route/GetRoute', {
          params: {
            destinationRoomId: destinationRoomId.toString(),
          },
        })
        .toPromise()
        .then((value) => {
          const parsedGeometries = value.geometries.map((v) => {
            return JSON.parse(v);
          });

          resolve(new Route(sourceRoomId, destinationRoomId, parsedGeometries));
        })
        .catch((error) => {
          reject(error);
        });
    });
  }

  public GetStatringPoint(id: number): Promise<GeoJsonObject> {
    return new Promise<GeoJsonObject>((resolve, reject) => {
      this._http
        .get<any>(environment.apiUrl + '/Route/GetStartingPoint', {
          params: {
            id: id.toString(),
          },
        })
        .toPromise()
        .then((value) => {
          const parsedAsJson = JSON.parse(value);
          resolve(parsedAsJson)
        })
        .catch((error) => {
          reject(error);
        })
    });
  }
}
