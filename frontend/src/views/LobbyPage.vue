<template>
  <v-toolbar>
    <v-toolbar-title>{{ heading }}</v-toolbar-title>
    <v-spacer></v-spacer>
    <v-btn icon>
      <v-icon :icon="mdiLogout" />
    </v-btn>
  </v-toolbar>
  <v-container>
    <v-row>
      <v-col cols="3">
        <p>User List</p>
        <v-list lines="one">
          <v-list-item
            v-for="user in users"
            :key="user.userId"
            :title="user.username"
          ></v-list-item>
        </v-list>
      </v-col>
      <v-col cols="6">
        <p>Center</p>
      </v-col>
      <v-col cols="3">
        <p>Card Queue</p>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <p>Voting Results</p>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <p>Card List</p>
      </v-col>
    </v-row>
  </v-container>
</template>
<script setup lang="ts">
import useStorage from '@/composables/useStorage';
import { useRoomStore } from '@/stores/roomStore';
import { HubResponse } from '@/types/common';
import { RoomResponse } from '@/types/room';
import { mdiLogout } from '@mdi/js';
import { HubConnection } from '@microsoft/signalr';
import { computed, inject, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const connection = inject<HubConnection>('hubConnection')!;

const roomStore = useRoomStore();

const { getUser, clearUser } = useStorage();
const route = useRoute();
const router = useRouter();

const roomCode = ref<string>(route.params.roomCode as string);

const users = computed(() => {
  return roomStore.roomUsers.get(roomCode.value) ?? [];
});
const heading = computed(() => `Room ${roomStore.currentRoom?.roomCode}`);

onMounted(async () => {
  if (!roomStore.currentRoom) {
    const userId = getUser();
    const response = await connection.invoke<HubResponse<RoomResponse>>('RejoinRoom', {
      roomCode: roomCode.value,
      userId,
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
    } else {
      clearUser();
      console.error(response.validationErrors);
      router.push({ name: 'Landing' });
    }
  }
});
</script>
<style lang="scss" scoped></style>
