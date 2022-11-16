import { defineStore } from 'pinia';
import { useCommonStore } from './common';
import { Events } from '@shared/enums/Events';

export const useRoomStore = defineStore('room', () => {
  const commonStore = useCommonStore();

  function createRoom() {
    const socket = commonStore.socket;

    socket?.emit(Events.CreateRoom, { name: 'test' });
  }

  return {
    createRoom,
  };
});
