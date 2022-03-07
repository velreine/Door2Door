import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Room } from 'src/app/model/Room';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RoomService {
  constructor(private _http: HttpClient) {}

  private static mockRoomData = [
    { Id: '0', Name: 'Det', Geometry: null },
    { Id: '1', Name: 'Her', Geometry: null },
    { Id: '2', Name: 'Kommer', Geometry: null },
    { Id: '3', Name: 'Fra', Geometry: null },
    { Id: '4', Name: 'room-service.ts', Geometry: null },
  ];

  public GetAllRooms(): Promise<ApiRoomType[]> {
    return this._http
      .get<ApiRoomType[]>(environment.apiUrl + '/Door2Door/GetAllRooms', {
        headers: new HttpHeaders({
          'Access-Control-Allow-Origin': '*',
        }),
      })
      .toPromise();
  }

  public GetRoomById(id /*:number*/) {
    return this._http
      .get<ApiRoomType>(
        environment.apiUrl + '/Door2Door/GetRoomByIdCoolVersion',
        {
          headers: new HttpHeaders({
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Method': 'any',
            'Access-Control-Allow-Header': '*',
          }),
          params: {
            id: id,
          },
        }
      )
      .toPromise();
  }
}

type ApiRoomType = Pick<Room, 'Id' | 'Geometry' | 'Name' | 'Type'>;
