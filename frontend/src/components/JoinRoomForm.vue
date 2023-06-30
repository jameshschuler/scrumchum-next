<template>
  <v-form v-model="isFormValid" @submit.prevent="handleSubmit">
    <v-text-field
      class="my-4"
      variant="outlined"
      label="Room Code"
      clearable
      v-model="roomCode"
      :rules="roomCodeRules"
      counter="4"
    ></v-text-field>
    <v-text-field
      class="my-4"
      variant="outlined"
      label="Username"
      clearable
      v-model="username"
      :rules="usernameRules"
      counter="20"
    ></v-text-field>
    <v-select
      class="my-4"
      variant="outlined"
      label="User Role"
      :items="userRoleOptions()"
      clearable
      item-title="name"
      item-value="value"
      v-model="userRole"
      :rules="userRoleRules"
    ></v-select>

    <div class="d-flex justify-end">
      <v-btn
        type="submit"
        color="info"
        :disabled="!isFormValid"
        :loading="isSubmitting"
        size="large"
        flat
        >Join Room</v-btn
      >
    </div>
  </v-form>
</template>
<script setup lang="ts">
import useStorage from '@/composables/useStorage';
import { useRoomStore } from '@/stores/roomStore';
import { HubResponse } from '@/types/common';
import { JoinRoomRequest, RoomResponse } from '@/types/room';
import { userRoleOptions } from '@/utils';
import { HubConnection } from '@microsoft/signalr';
import { inject, ref } from 'vue';
import { useRouter } from 'vue-router';

const username = ref<string>();
const usernameRules = [
  (value: string) => {
    if (value?.length > 0 && value.length <= 20) {
      return true;
    }

    return 'Username must be 1-20 characters.';
  },
];

const roomCode = ref<string>();
const roomCodeRules = [
  (value: string) => {
    if (value?.length === 4) {
      return true;
    }

    return 'Room Code must be 4 characters.';
  },
];

const userRole = ref<number>();
const userRoleRules = [
  (value: number | undefined) => {
    if (value !== undefined) {
      return true;
    }

    return 'Must select User Role.';
  },
];

const isFormValid = ref<boolean>();
const isSubmitting = ref<boolean>();

const connection = inject<HubConnection>('hubConnection')!;
const router = useRouter();
const { storeUserId } = useStorage();

const roomStore = useRoomStore();

async function handleSubmit(_: any) {
  isSubmitting.value = true;
  const response = await connection.invoke<HubResponse<RoomResponse>>('JoinRoom', {
    roomCode: roomCode.value,
    username: username.value,
    userRole: userRole.value,
  } as JoinRoomRequest);

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
    console.error(response.validationErrors);
  }

  isSubmitting.value = false;
}
</script>
<style lang="scss" scoped></style>
