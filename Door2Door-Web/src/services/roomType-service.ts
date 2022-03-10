import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RoomType } from 'src/app/model/RoomType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RoomTypeService {
  constructor(private _http: HttpClient) { }

  public GetAllRoomTypes(): Promise<RoomType[]> {
    return this._http
      .get<RoomType[]>(environment.apiUrl + '/Room/GetAllRoomTypes', {
        headers: new HttpHeaders({
          'Access-Control-Allow-Origin': '*',
        }),
      })
      .toPromise();
  }
}
