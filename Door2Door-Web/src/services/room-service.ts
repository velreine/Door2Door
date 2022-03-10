import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Room } from 'src/app/model/Room';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RoomService {
  constructor(private _http: HttpClient) { }

  public GetAllRooms(): Promise<Room[]> {
    return this._http
      .get<Room[]>(environment.apiUrl + '/Room/GetAllRooms', {
        headers: new HttpHeaders({
          'Access-Control-Allow-Origin': '*',
        }),
      })
      .toPromise();
  }

  public GetRoomById(id: number) : Promise<Room> {
    return this._http
      .get<Room>(environment.apiUrl + '/Room/GetRoomById', {
        params: {
          id: id,
        },
      })
      .toPromise();
  }
}
