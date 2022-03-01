import GeometryType from "./GeometryType";

class Geometry {

  public Name: string;

  public Type: GeometryType;

  public GeoJsonData: string;

  constructor(type: GeometryType, name: string) {
    this.Name = name;
    this.Type = type;
  }

}

export default Geometry;