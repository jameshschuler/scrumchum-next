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

    <div class="field">
      <label class="label">Cardset</label>
      <div class="control">
        <div class="select">
          <select v-model="formData.cardset">
            <option selected value="">Select Cardset</option>
            <option :value="key" v-for="[key, value] in cardsets">
              {{ value }}
            </option>
          </select>
        </div>
      </div>
    </div>

    <label class="label">Room Code</label>
    <div class="field has-addons">
      <div class="control">
        <input
          class="input"
          type="text"
          placeholder="(Optional)"
          v-model="formData.roomCode"
        />
      </div>
      <div class="control">
        <a class="button" @click="generateRoomCode()">
          <i class="fa-solid fa-arrows-rotate"></i>
        </a>
      </div>
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
        <button class="button" :disabled="!isFormValid">Create</button>
      </div>
    </div>
  </form>
</template>
<script setup lang="ts">
import { UserType } from '@shared/enums/UserType';
import {
  CreateRoomForm,
  CreateRoomFormSchema,
} from '@shared/forms/CreateRoomForm';
import { ref } from 'vue';

const formData = ref<CreateRoomForm>({
  cardset: '',
  userType: -1,
});
const isFormValid = ref<bool>(true);

const userTypes = ref(
  new Map<number, string>([
    [UserType.Developer, 'Developer'],
    [UserType.Host, 'Host'],
    [UserType.QA, 'QA'],
    [UserType.Spectator, 'Spectator'],
  ])
);

const cardsets = ref<Map<string, string>>(
  new Map<string, string>([
    ['default', '1, 2, 3, 5, 8,  13, 20, 40, 100, ?, coffee, infinity, pass'],
  ])
);

function generateRoomCode() {
  console.log('generateRoomCode');
}

function handleSubmit() {
  try {
    const result = CreateRoomFormSchema.parse(formData.value);
    console.log(result);
  } catch (err) {
    // TODO: handle errors
    console.log(err);
  }
}
</script>
<style lang="scss" scoped>
.field {
  margin-bottom: 2rem;
}

.button {
  background-color: $primary;
  color: #fff;
  border-color: transparent;

  &:hover {
    color: #fff;
    background-color: #6259ff;
  }
}

.button[disabled] {
  background-color: $primary;
}
</style>
