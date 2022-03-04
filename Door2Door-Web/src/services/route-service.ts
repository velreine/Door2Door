import { Point } from 'leaflet';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';

@Injectable()
export class RouteService {
  private static mockData = [];

  //public static GetRouteBetween(from: Point, to: Point) : Promise<GeoJsonObject[]> {
  //}

  constructor(private _http: HttpClient) {}

  public TestApiCall() : any {

    console.log('Invoked TestApiCall():');

    console.log('Result is: ');

    this._http.get(environment.apiUrl + '/nicky', {
      observe: 'body'
    }).subscribe((data: any) => {
      console.log(data);
    });

  }

}
