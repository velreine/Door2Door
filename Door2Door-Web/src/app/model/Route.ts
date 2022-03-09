import {RoomType} from "./RoomType";
import { GeoJsonObject } from 'geojson';
import { PointOfEntry } from "./PointOfEntry";
import { Geometry } from "./Geometry";

export class Route extends Geometry {
  
  constructor(
    // Scalar fields.
    public fromRoomId: Number,
    public toRoomId: Number,
    public geometry: GeoJsonObject,
    ) {
    super(geometry);
  }

}