import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';

@Injectable()
export class RouteService {
  private static mockData = [];

  constructor(private _http: HttpClient) {}

  public TestApiCall(): any {
    console.log('Invoked TestApiCall():');

    console.log('Result is: ');

    this._http
      .get(environment.apiUrl + '/nicky', {
        observe: 'body',
      })
      .subscribe((data: any) => {
        console.log(data);
      });
  }
}
