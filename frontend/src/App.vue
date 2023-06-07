<template>
  <div>
    <input type="text" v-model="username" placeholder="Username" />
    <select v-model="role">
      <option v-for="role in userRoleOptions" :value="role.value">{{ role.name }}</option>
    </select>
    <input type="text" v-model="roomCode" placeholder="Room Code" />
    <input type="text" v-model="roomName" placeholder="Room Name" />
    <br />
    <button @click="createRoom">Create Room</button>
    <button @click="joinRoom">Join Room</button>
    <router-view></router-view>
  </div>
</template>
<script setup lang="ts">
import { useRoomStore } from "@/stores/roomStore";
import { HubConnection } from "@microsoft/signalr";
import { computed, inject, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import useStorage from "./composables/useStorage";
import { HubResponse } from "./types/common";
import { RoomResponse } from "./types/room";
import { UserJoinedResponse, UserRole } from "./types/user";

const connection = inject<HubConnection>("hubConnection")!;

const { storeUserId } = useStorage();
const router = useRouter();

const roomStore = useRoomStore();

const role = ref<UserRole | number>(-1);
const username = ref<string>();
const roomCode = ref<string>();
const roomName = ref<string>();

const userRoleOptions = computed(() => {
  const keys = Object.keys(UserRole).filter((v) => isNaN(Number(v)));
  const optionsMap = keys.map((key: string) => {
    return {
      name: key.replace(/([a-z])([A-Z])/g, "$1 $2"),
      value: Number(UserRole[key as any]),
    };
  });
  optionsMap.unshift({ name: "Select Role", value: -1 });
  return optionsMap;
});

async function createRoom() {
  const response = await connection.invoke<HubResponse<RoomResponse>>("CreateRoom", {
    role: role.value,
    username: username.value,
    roomName: roomName.value,
  });

  if (response.success && response.data) {
    const { user, roomCode, roomLink } = response.data;
    roomStore.currentRoom = {
      roomCode,
      roomLink,
      users: [],
    };
    roomStore.me = user;
    storeUserId(user.userId!);
    router.push({ name: "Lobby", params: { roomCode } });
  } else {
    console.log(response.validationErrors);
  }
}

async function joinRoom() {
  const response = await connection.invoke<HubResponse<RoomResponse>>("JoinRoom", {
    role: role.value,
    username: username.value,
    roomCode: roomCode.value,
  });

  if (response.success && response.data) {
    const { user, roomCode, roomLink } = response.data;
    roomStore.currentRoom = {
      roomCode,
      roomLink,
      users: [],
    };
    roomStore.me = user;
    storeUserId(user.userId!);

    router.push({ name: "Lobby", params: { roomCode } });
  } else {
    console.log(response.validationErrors);
  }
}

onMounted(async () => {
  connection.on("Connected", (response) => {
    console.log(response);
  });

  connection.on("UserJoined", (response: UserJoinedResponse) => {
    roomStore.roomUsers.set(response.roomCode, response.users);
  });

  connection.onclose(() => {
    // TODO:
  });
});
</script>
<style scoped></style>
