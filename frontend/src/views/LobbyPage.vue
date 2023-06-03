<template>
  <h1>Lobby - {{ heading }}</h1>
  <div>
    <p v-for="user in users">
      {{ user.username }}
    </p>
  </div>
</template>
<script setup lang="ts">
import { useRoomStore } from "@/stores/roomStore";
import { computed, onMounted, ref } from "vue";
import { useRoute } from "vue-router";

const roomStore = useRoomStore();

const route = useRoute();
const roomCode = ref<string>(route.params.roomCode as string);

const users = computed(() => {
  return roomStore.roomUsers.get(roomCode.value) ?? [];
});
const heading = computed(() => `Room ${roomStore.currentRoom?.roomCode}`);

onMounted(() => {
  // TODO: check localstorage for user id
  // If found send user id and room code, send rejoin room event
  console.log("mounted");
});
</script>
<style lang="scss" scoped></style>
