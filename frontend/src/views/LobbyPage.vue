<template>
  <v-toolbar>
    <v-toolbar-title>{{ heading }}</v-toolbar-title>
    <v-spacer></v-spacer>
    <v-btn icon>
      <v-icon :icon="mdiLogout" />
    </v-btn>
  </v-toolbar>
  <v-container class="h-screen">
    <v-row class="h-50">
      <v-col cols="3" class="border">
        <p>User List</p>
        <v-list lines="one">
          <v-list-item
            v-for="user in roomStore.currentRoom?.users"
            :key="user.userId"
            :title="user.username"
          ></v-list-item>
        </v-list>
      </v-col>
      <v-col cols="6" class="border">
        <p>Center</p>
      </v-col>
      <v-col cols="3" class="border">
        <p>Card Queue</p>
      </v-col>
    </v-row>
    <v-row class="border h-25">
      <v-col>
        <p>Voting Results</p>
      </v-col>
    </v-row>
    <v-row class="border h-25">
      <v-col>
        <p>Card List</p>
        <div class="d-flex h-100 overflow-x-auto">
          <Card :value="'0'" />
          <Card :value="'1'" />
          <Card :value="'2'" />
          <Card :value="'3'" />
          <Card :value="'5'" />
          <Card :value="'8'" />
          <Card :value="'13'" />
          <Card :value="'21'" />
          <Card :value="'Pass'" />
        </div>
      </v-col>
    </v-row>
  </v-container>
</template>
<script setup lang="ts">
import Card from '@/components/Card.vue';
import useStorage from '@/composables/useStorage';
import { useRoomStore } from '@/stores/roomStore';
import { HubResponse } from '@/types/common';
import { RejoinRoomResponse } from '@/types/room';
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

const heading = computed(() => `Room ${roomStore.currentRoom?.roomCode}`);

onMounted(async () => {
  if (!roomStore.currentRoom) {
    const userId = getUser();
    const response = await connection.invoke<HubResponse<RejoinRoomResponse>>('RejoinRoom', {
      roomCode: roomCode.value,
      userId,
    });

    if (response.success && response.data) {
      const { hostUserId, user, roomCode, roomLink, users } = response.data;
      console.log(users);
      roomStore.currentRoom = {
        hostUserId,
        roomCode,
        roomLink,
        users,
      };
      roomStore.me = user;
      // TODO: get user list
    } else {
      clearUser();
      console.error(response.validationErrors);
      router.push({ name: 'Landing' });
    }
  }
});
</script>
<style lang="scss" scoped></style>
