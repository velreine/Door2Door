import {RoomType} from "./RoomType";
import { GeoJsonObject } from 'geojson';
import { PointOfEntry } from "./PointOfEntry";
import { Geometry } from "./Geometry";

export class Room extends Geometry {

  constructor(
    // Scalar fields.
    public id: Number,
    public name: string,
    public type: RoomType,
    public geometry: GeoJsonObject,
    ) {
    super(geometry);
  }

}
