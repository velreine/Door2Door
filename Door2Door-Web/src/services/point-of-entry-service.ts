import { PointOfEntry } from "src/app/model/PointOfEntry";
import { Room } from "src/app/model/Room";

// No, it's not that kind of room service.
export class PointOfEntryService {
  private static mockData = [
    { Id: '0', Room: null, Geometry: null },
    { Id: '1', Room: null, Geometry: null },
    { Id: '2', Room: null, Geometry: null },
    { Id: '3', Room: null, Geometry: null },
  ];

  public static GetAllPointsOfEntry(): Promise<
    Pick<PointOfEntry, 'Id' | 'Geometry'>[]
  > {
    return new Promise<any>((resolve, reject) => {

      // TODO: Invoke API here...

      resolve(PointOfEntryService.mockData);
    });
  }
}
