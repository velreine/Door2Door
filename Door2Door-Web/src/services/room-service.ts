// No, it's not that kind of room service.
export class RoomService {

  private static mockRoomData = [
    {value: "0", label: "Det"},
    {value: "1", label: "Her"},
    {value: "2", label: "Kommer"},
    {value: "3", label: "Fra"},
    {value: "4", label: "room-service.ts"},
  ];

  GetAllRooms() {
    // TODO: Implement when API supports it.
  }

  public static GetAllRoomsMock() : Promise<any> {

    return new Promise<any>((resolve, reject) => {
      resolve(RoomService.mockRoomData);
    });

    
  }

}
