export interface User {
  username: string;
  role: UserRole;
  userId?: number;
}

export enum UserRole {
  Dev = 0,
  QA = 1,
  ProjectManager = 2,
  Spectator = 3,
}

export interface UserJoinedResponse {
  roomCode: string;
  users: Array<User>;
}
