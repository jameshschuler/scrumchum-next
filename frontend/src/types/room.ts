import { User } from "./user";

export interface Room {
  roomCode: string;
  roomLink: string;
  users: Array<User>;
}

export interface RoomResponse {
  roomCode: string;
  roomLink: string;
  roomName?: string;
  user: User;
}
