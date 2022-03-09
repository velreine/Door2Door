import { RoomType } from './RoomType';
import { GeoJsonObject } from 'geojson';
import { PointOfEntry } from './PointOfEntry';
import { Geometry } from './Geometry';

export class Route {
  constructor(
    // Scalar fields.
    public fromRoomId: Number,
    public toRoomId: Number,
    public Geometry: GeoJsonObject[]
  ) {
    let m = Geometry[0] as any;

    Geometry.forEach((obj: any) => {
      let reversedCoordinates = obj.coordinates.map((coord) => {
        console.log('coord', coord);
        return [coord[1], coord[0]];
      });

      obj.coordinates = reversedCoordinates;

      console.log('reversedCoordinates', reversedCoordinates);
    });

    console.log('coords:', m.coordinates);
  }
}
