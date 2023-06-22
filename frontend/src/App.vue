<template>
  <div>
    <router-view></router-view>
  </div>
</template>
<script setup lang="ts">
import { useRoomStore } from '@/stores/roomStore';
import { HubConnection } from '@microsoft/signalr';
import { inject, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import useStorage from './composables/useStorage';
import { HubResponse } from './types/common';
import { RoomResponse } from './types/room';
import { UserJoinedResponse, UserRole } from './types/user';

const connection = inject<HubConnection>('hubConnection')!;

const { storeUserId } = useStorage();
const router = useRouter();

const roomStore = useRoomStore();

const role = ref<UserRole | number>(-1);
const username = ref<string>();
const roomCode = ref<string>();
const roomName = ref<string>();

async function createRoom() {
  const response = await connection.invoke<HubResponse<RoomResponse>>('CreateRoom', {
    role: role.value,
    username: username.value,
    roomName: roomName.value,
  });

  // TODO: this is same as joinRoom...move to shared function
  if (response.success && response.data) {
    const { hostUserId, user, roomCode, roomLink } = response.data;
    roomStore.currentRoom = {
      hostUserId,
      roomCode,
      roomLink,
      users: [],
    };
    roomStore.me = user;
    storeUserId(user.userId!);
    router.push({ name: 'Lobby', params: { roomCode } });
  } else {
    console.log(response.validationErrors);
  }
}

async function joinRoom() {
  const response = await connection.invoke<HubResponse<RoomResponse>>('JoinRoom', {
    role: role.value,
    username: username.value,
    roomCode: roomCode.value,
  });

  if (response.success && response.data) {
    const { hostUserId, user, roomCode, roomLink } = response.data;
    roomStore.currentRoom = {
      hostUserId,
      roomCode,
      roomLink,
      users: [],
    };
    roomStore.me = user;
    storeUserId(user.userId!);

    router.push({ name: 'Lobby', params: { roomCode } });
  } else {
    console.log(response.validationErrors);
  }
}

onMounted(async () => {
  connection.on('Connected', (response) => {
    console.log(response);
  });

  connection.on('UserJoined', (response: UserJoinedResponse) => {
    roomStore.roomUsers.set(response.roomCode, response.users);
  });

  connection.onclose(() => {
    // TODO:
  });
});
</script>
<style scoped></style>
