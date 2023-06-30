import { User, UserRole } from "./user";

export interface Room {
  roomCode: string;
  roomLink: string;
  users: Array<User>;
  hostUserId: string;
}

export interface JoinRoomRequest {
  roomCode: string;
  username: string;
  userRole: UserRole;
}

export interface CreateRoomRequest {
  roomName: string;
  username: string;
  userRole: UserRole;
}

export interface RoomResponse {
  hostUserId: string;
  roomCode: string;
  roomLink: string;
  roomName?: string;
  user: User;
}

export interface RejoinRoomResponse extends RoomResponse {
  users: Array<User>;
}
