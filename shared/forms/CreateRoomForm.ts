import { UserType } from "../enums/UserType";

export interface CreateRoomForm {
  name?: string;
  roomCode?: string;
  userType?: UserType;
}
