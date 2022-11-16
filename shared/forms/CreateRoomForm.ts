import { UserType } from "../enums/UserType";
import { CardSet } from "../enums/CardSet";

// export const CreateRoomFormSchema = z.object({
//   cardset: z.nativeEnum(Cardset, {
//     required_error: "Must select a Cardset",
//   }),
//   name: z.string().trim().min(1).max(25),
//   roomCode: z.string().trim().length(6),
//   userType: z.nativeEnum(UserType, {
//     required_error: "Must select a User Type",
//   }),
// });

export interface CreateRoomForm {
  cardSet?: CardSet;
  name?: string;
  roomCode?: string;
  userType?: UserType;
}
