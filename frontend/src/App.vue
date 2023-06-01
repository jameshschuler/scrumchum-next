<template>
  <div>
    <button @click="createRoom">Create Room</button>
    <!-- <button @click="joinRoom">Join Room</button> -->
  </div>
</template>
<script setup lang="ts">
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { onMounted, ref } from "vue";
const hubConnection = ref<HubConnection | null>(null);

const baseUrl = "https://localhost:7032";

async function createRoom() {
  const response = await fetch(`${baseUrl}/api/room`, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ name: "test" }),
  });

  const responseData = await response.json();
  if (responseData.success) {
    console.log(responseData.data);
    const { roomCode, name } = responseData.data;
    await hubConnection.value?.invoke("JoinRoom", {
      roomCode,
      name,
    });
  }
}

// TODO: make connection to hub
onMounted(() => {
  console.log("connecting to hub...");
  let connection = new HubConnectionBuilder().withUrl(`${baseUrl}/room`).build();

  hubConnection.value = connection;

  // connection.on("send", (data) => {
  //   console.log(data);
  // });

  if (hubConnection.value) {
    hubConnection.value.start().then(() => {
      const hub = hubConnection.value!;
      hub.on("Connected", (response) => {
        console.log(response);
      });

      hub.on("JoinedRoom", () => {
        console.log("do something!");
      });
    });
  }
});
</script>
<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
