<template>
  <div>
    <button @click="createRoom">Create Room</button>
    <button @click="joinRoom">Join Room</button>
  </div>
</template>
<script setup lang="ts">
import { ref, onMounted } from "vue";
import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr";
const hubConnection = ref<HubConnection | null>(null);

async function createRoom() {
  try {
    const response = await hubConnection.value?.invoke("CreateRoom", {
      username: "Bobby",
    });
    console.log(response);
  } catch (err) {
    console.error(err);
  }
}

async function joinRoom() {
  const response = await hubConnection.value?.invoke("JoinRoom", {
    roomCode: "",
    name: "",
  });
  console.log(response);
}

// TODO: make connection to hub
onMounted(() => {
  console.log("connecting to hub...");
  let connection = new HubConnectionBuilder()
    .withUrl("https://localhost:7032/planning")
    .build();

  hubConnection.value = connection;

  // connection.on("send", (data) => {
  //   console.log(data);
  // });

  if (hubConnection.value) {
    hubConnection.value.start().then(() => {
      hubConnection.value!.on("Connected", (response) => {
        console.log(response);
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
