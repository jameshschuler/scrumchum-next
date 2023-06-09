import { User } from "./user";

export interface Room {
  roomCode: string;
  roomLink: string;
  users: Array<User>;
  hostUserId: string;
}

export interface RoomResponse {
  hostUserId: string;
  roomCode: string;
  roomLink: string;
  roomName?: string;
  user: User;
}
