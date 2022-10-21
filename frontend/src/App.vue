<template>
  <Header />
  <Main />
  <Footer />
</template>
<script setup lang="ts">
import Header from '@/components/Header.vue';
import Main from '@/components/Main.vue';
import Footer from '@/components/Footer.vue';
import { io } from 'socket.io-client';

const url = import.meta.env.VITE_BACKEND_URL;
const socket = io('https://localhost:9000/');

console.log(socket);

socket.on('connect', () => {
  console.log(socket.id);
});

socket.on('disconnect', () => {
  console.log(socket.id);
});
</script>
<style lang="scss">
html,
body {
  width: 100%;
  height: 100%;
  background-color: #f5f5f5;
}
#app {
  min-height: 100%;
  display: flex;
  flex-direction: column;
  align-items: stretch;
  main {
    flex-grow: 1;
  }

  header,
  main,
  footer {
    flex-shrink: 0;
  }
}
</style>
