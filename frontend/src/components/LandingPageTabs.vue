<template>
  <div class="mt-3">
    <div class="pb-5">
      <h1 class="is-size-3 pb-2">Welcome to Scrumchum!</h1>
      <p>
        Lorem Ipsum has been the industry's standard dummy text ever since the
        1500s, when an unknown printer took a galley of type and scrambled it to
        make a type specimen book.
      </p>
    </div>

    <div class="tabs is-medium is-flex is-flex-direction-column">
      <ul>
        <li
          :class="{ 'is-active': currentTab === 'create' }"
          @click="handleTabChange('create')"
        >
          <a>Create</a>
        </li>
        <li
          :class="{ 'is-active': currentTab === 'join' }"
          @click="handleTabChange('join')"
        >
          <a>Join</a>
        </li>
      </ul>
      <div id="tab-content" class="px-3 py-5">
        <div
          v-if="currentTab === 'create'"
          class="is-flex is-flex-direction-column"
        >
          <CreateRoomForm />
        </div>
        <div
          v-if="currentTab === 'join'"
          class="is-flex is-flex-direction-column"
        >
          <p>Join an existing room</p>
          <form class="pt-3">
            <div class="field">
              <label class="label">Name</label>
              <div class="control">
                <input class="input" type="text" placeholder="Text input" />
              </div>
            </div>
            <div class="field">
              <label class="label">Name</label>
              <div class="control">
                <input class="input" type="text" placeholder="Text input" />
              </div>
            </div>
            <div class="field pt-3">
              <div class="control">
                <button class="button">Join</button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref } from 'vue';
import CreateRoomForm from '@/components/CreateRoomForm.vue';

const currentTab = ref<string>('create');

function handleTabChange(tab: string) {
  currentTab.value = tab;
}
</script>
<style lang="scss" scoped>
button {
  background-color: $primary;
  color: #fff;
  border-color: transparent;

  &:hover {
    color: #fff;
    background-color: #6259ff;
  }
}
</style>
