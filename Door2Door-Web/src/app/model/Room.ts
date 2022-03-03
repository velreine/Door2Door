import {RoomType} from "./RoomType";
import { GeoJsonObject } from 'geojson';
import { PointOfEntry } from "./PointOfEntry";
import { Geometry } from "./Geometry";

export class Room extends Geometry {
  

  constructor(
    // Scalar fields.
    public Id: Number,
    public Name: string,
    public Type: RoomType,
    public Geometry: GeoJsonObject,

    // One-To-Many(s)
    public PointOfEntries?: PointOfEntry[]
    ) {
    super(Geometry);
  }

}