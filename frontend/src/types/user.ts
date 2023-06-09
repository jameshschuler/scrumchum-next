export interface User {
  username: string;
  role: UserRole;
  userId?: string;
  isHost: boolean;
}

export enum UserRole {
  Dev = 0,
  QA = 1,
  ProductOwner = 2,
  Spectator = 3,
}

export interface UserJoinedResponse {
  roomCode: string;
  users: Array<User>;
}
