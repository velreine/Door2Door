import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Room } from 'src/app/model/Room';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RoomService {
  constructor(private _http: HttpClient) {}

  public GetAllRooms(): Promise<Room[]> {
    return new Promise<Room[]>((resolve, reject) => {
      return this._http
        .get<Room[]>(environment.apiUrl + '/Room/GetAllRooms', {
          headers: new HttpHeaders({
            'Access-Control-Allow-Origin': '*',
          }),
        })
        .toPromise()
        .then((response) => {
          // Sort the rooms by their name, then by their type.
          let sorted = response
            .sort((a, b) => a.name.localeCompare(b.name))
            .sort((a, b) => a.type.name.localeCompare(b.type.name));

          resolve(sorted);
        })
        .catch((error) => {
          reject(error);
        });
    });
  }

  public GetRoomById(id: number): Promise<Room> {
    return this._http
      .get<Room>(environment.apiUrl + '/Room/GetRoomById', {
        params: {
          id: id,
        },
      })
      .toPromise();
  }
}
