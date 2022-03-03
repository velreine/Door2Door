import { GeoJsonObject } from 'geojson';

export abstract class Geometry {

  constructor(public Geometry: GeoJsonObject) { }

}