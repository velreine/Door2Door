import { GeoJsonObject } from 'geojson';
import { Geometry } from './Geometry';
import { Room } from './Room';

export class PointOfEntry extends Geometry {

  constructor(
    // Scalar Fields
    public Id: Number,
    public Geometry: GeoJsonObject,

    // Many-To-One
    public Room?: Room,
  )  {
    super(Geometry);
  }
}