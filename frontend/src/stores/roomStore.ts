import { Room } from "@/types/room";
import { User } from "@/types/user";
import { defineStore } from "pinia";
import { ref } from "vue";

export const useRoomStore = defineStore("room", () => {
  const currentRoom = ref<Room>();

  // TODO: will we ever have users who aren't in the same room or multiple rooms in state?
  const roomUsers = ref<Map<string, User[]>>(new Map<string, User[]>());
  const me = ref<User>();

  return {
    currentRoom,
    me,
    roomUsers,
  };
});
