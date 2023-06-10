<template>
  <v-container class="mt-16">
    <v-row>
      <v-col class="rounded-lg border">
        <h1 class="text-center mt-6 clickable">
          <span :class="{ selected: selectedTab === 'create' }" @click="toggleTab('create')"
            >Create Room</span
          >
          /
          <span :class="{ selected: selectedTab === 'join' }" @click="toggleTab('join')"
            >Join Room</span
          >
        </h1>
        <div class="pa-10">
          <CreateRoomForm v-if="selectedTab === 'create'" />
          <JoinRoomForm v-if="selectedTab === 'join'" />
        </div>
      </v-col>
    </v-row>
  </v-container>
</template>
<script setup lang="ts">
import CreateRoomForm from '@/components/CreateRoomForm.vue';
import JoinRoomForm from '@/components/JoinRoomForm.vue';
import { ref } from 'vue';

const selectedTab = ref<string>('create');

function toggleTab(tab: string) {
  console.log('toggle', tab);
  selectedTab.value = tab;
}
</script>
<style lang="scss" scoped>
.selected {
  color: #ff80ab;
}
</style>
