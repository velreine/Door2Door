import { Room } from 'src/app/model/Room';

// No, it's not that kind of room service.
export class RoomService {
  private static mockRoomData = [
    { Id: '0', Name: 'Det', Geometry: null },
    { Id: '1', Name: 'Her', Geometry: null },
    { Id: '2', Name: 'Kommer', Geometry: null },
    { Id: '3', Name: 'Fra', Geometry: null },
    { Id: '4', Name: 'room-service.ts', Geometry: null },
  ];

  //export type scalarFields = 'Id' | 'Geometry' | 'Name' | 'Type';

  public static GetAllRooms(): Promise<
    Pick<Room, 'Id' | 'Geometry' | 'Name' | 'Type'>[]
  > {
    return new Promise<any>((resolve, reject) => {

      // Invoke API here...

      resolve(RoomService.mockRoomData);
    });
  }

  

}
