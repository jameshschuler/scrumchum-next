<template>
  <p>Create a new room</p>
  <form class="pt-3" @submit.prevent="handleSubmit">
    <div class="field">
      <label class="label">Name</label>
      <div class="control">
        <input
          class="input"
          type="text"
          placeholder="Name"
          required
          v-model="formData.name"
        />
      </div>
    </div>

    <label class="label">Room Code</label>
    <div class="field has-addons">
      <p class="control">
        <input
          class="input"
          type="text"
          placeholder="(Optional)"
          v-model="formData.roomCode"
        />
      </p>
      <p class="control">
        <button class="button" @click="generateRoomCode()">
          <i class="fa-solid fa-arrows-rotate"></i>
        </button>
      </p>
    </div>
    <div class="field">
      <label class="label">User Type</label>
      <div class="control">
        <div class="select">
          <select v-model="formData.userType">
            <option selected value="-1">Select User Type</option>
            <option :value="key" v-for="[key, value] in userTypes">
              {{ value }}
            </option>
          </select>
        </div>
      </div>
    </div>
    <div class="field pt-3">
      <div class="control">
        <button class="button">Create</button>
      </div>
    </div>
  </form>
</template>
<script setup lang="ts">
import { UserType } from '@shared/enums/UserType';
import { CreateRoomForm } from '@shared/forms/CreateRoomForm';
import { ref } from 'vue';

const formData = ref<CreateRoomForm>({
  userType: -1,
});
const userTypes = ref(
  new Map<number, string>([
    [UserType.Developer, 'Developer'],
    [UserType.Host, 'Host'],
    [UserType.QA, 'QA'],
    [UserType.Spectator, 'Spectator'],
  ])
);

function generateRoomCode() {
  console.log('generateRoomCode');
}

function handleSubmit() {
  // TODO: validate formData
  console.log(formData.value);
  console.log(UserType);
  console.log('handleSubmit');
}
</script>
<style lang="scss" scoped>
.field {
  margin-bottom: 1rem;
}

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
