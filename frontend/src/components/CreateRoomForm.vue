<template>
  <p>Create a new room</p>
  <form class="pt-3" @submit.prevent="handleSubmit">
    <div class="field">
      <label class="label">Name</label>
      <div class="control">
        <input
          class="input"
          type="text"
          placeholder="Your Name"
          required
          v-model="formData.name"
        />
      </div>
    </div>

    <div class="field">
      <label class="label">Card Set</label>
      <div class="control">
        <div class="select is-fullwidth">
          <select v-model="formData.cardSet">
            <option selected value="">Select Card Set</option>
            <option :value="key" v-for="[key, value] in cardSets">
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
          placeholder="(e.g. 123543)"
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
            <option selected value="">Select User Type</option>
            <option
              :value="key.toLowerCase()"
              v-for="key in Object.keys(UserType)"
            >
              {{ key }}
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
import { useRoomStore } from '@/stores/room';
import { UserType } from '@shared/enums/UserType';
import { CreateRoomForm } from '@shared/forms/CreateRoomForm';
import { ref } from 'vue';

const roomStore = useRoomStore();

const formData = ref<CreateRoomForm>({
  cardSet: '',
  userType: '',
});

const cardSets = ref<Map<string, string>>(
  new Map<string, string>([
    ['default', '1, 2, 3, 5, 8,  13, 20, 40, 100, ?, coffee, infinity, pass'],
  ])
);

function generateRoomCode() {
  console.log('generateRoomCode');
}

function handleSubmit() {
  try {
    // const result = CreateRoomFormSchema.parse(formData.value);
    // console.log(result);

    roomStore.createRoom();
  } catch (err) {
    // TODO: handle errors
    console.error(err);
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
