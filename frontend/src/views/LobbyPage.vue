<template>
  <h1>Lobby - {{ heading }}</h1>
  <div>
    <p v-for="user in users">
      {{ user.username }}
    </p>
  </div>
</template>
<script setup lang="ts">
import useStorage from "@/composables/useStorage";
import { useRoomStore } from "@/stores/roomStore";
import { HubConnection } from "@microsoft/signalr";
import { computed, inject, onMounted, ref } from "vue";
import { useRoute, useRouter } from "vue-router";

const connection = inject<HubConnection>("hubConnection")!;

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
    const response = await connection.invoke("RejoinRoom", {
      roomCode: roomCode.value,
      userId,
    });

    if (response.success && response.data) {
      const { user, roomCode, roomLink } = response.data;
      roomStore.currentRoom = {
        roomCode,
        roomLink,
        users: [],
      };
      roomStore.me = user;
    } else {
      clearUser();
      console.error(response.validationErrors);
      router.push({ name: "Landing" });
    }
  }
});
</script>
<style lang="scss" scoped></style>
