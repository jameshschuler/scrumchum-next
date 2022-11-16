import { defineStore } from 'pinia';
import { io, Socket } from 'socket.io-client';
import { ref } from 'vue';

export const useCommonStore = defineStore('common', () => {
  const socket = ref<Socket | null>(null);

  function setupSocketConnection() {
    const url = import.meta.env.VITE_BACKEND_URL;
    socket.value = io(url);

    console.log(socket);

    socket.value.on('connect', () => {
      console.log(socket.value!.id);
    });

    socket.value.on('disconnect', () => {
      console.log(socket.value!.id);
    });
  }

  return {
    socket,
    setupSocketConnection,
  };
});
