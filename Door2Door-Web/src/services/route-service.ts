import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { Route } from 'src/app/model/Route';

@Injectable()
export class RouteService {
  private static mockData = [];

  constructor(private _http: HttpClient) {}

  public GetRouteTo(/*fromRoomId: Number,*/ toRoomId: Number): Promise<Route> {
    
    // TODO: Debugging, until the Database supports a from id.
    // 8 should point to B-Gang B.02 (our static starting point for now.)
    const fromRoomId = 8; 

    // For lack of knowledge of a better way to map the data coming straight from the api to our Route model.
    return new Promise<Route>((resolve, reject) => {

      let data = this
      ._http
      .get<Route>(environment.apiUrl + '/Route/GetRoute', {
        params: {
          destinationRoomId: toRoomId.toString(),
        }
      })
      .toPromise()
      .then((value) => {
        console.log('values from api...');
        console.log(value);
        resolve(new Route(fromRoomId, toRoomId, null /* todo: fix getter...*/));
      })
      .catch((error) => {
        reject(error)
      });
      

    });
    
  }
}
