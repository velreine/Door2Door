import { Component } from '@angular/core';

//import { RoomService } from '../services/room-service.js'

import { RoomService } from '../services/room-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'Door2Door-Web';
  destinations = [];

  constructor() {
    console.log('constructed...');

    // Load rooms.
    RoomService.GetAllRoomsMock()
      .then((response) => {
        this.destinations = response;
        console.log(response);
      })
      .catch((error) => {
        console.error(error);
        alert('Something went wrong.');
      });
  }
}
//Test comment
