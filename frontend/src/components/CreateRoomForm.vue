<template>
  <v-form v-model="isFormValid" @submit.prevent="handleSubmit">
    <v-text-field
      class="my-4"
      variant="outlined"
      label="Room Name"
      clearable
      v-model="roomName"
      :rules="roomNameRules"
      counter="50"
    ></v-text-field>
    <v-text-field
      class="my-4"
      variant="outlined"
      label="Username"
      clearable
      v-model="username"
      :rules="usernameRules"
      counter="20"
    ></v-text-field>
    <v-select
      class="my-4"
      variant="outlined"
      label="User Role"
      :items="userRoleOptions()"
      clearable
      item-title="name"
      item-value="value"
      v-model="userRole"
      :rules="userRoleRules"
    ></v-select>

    <div class="d-flex justify-end">
      <v-btn
        type="submit"
        color="info"
        :disabled="!isFormValid"
        :loading="isSubmitting"
        size="large"
        flat
        >Create Room</v-btn
      >
    </div>
  </v-form>
</template>
<script setup lang="ts">
import { userRoleOptions } from "@/utils";
import { ref } from "vue";

const username = ref<string>();
const usernameRules = [
  (value: string) => {
    if (value?.length > 0 && value.length <= 20) return true;

    return "Username must be 1-20 characters.";
  },
];

const roomName = ref<string>();
const roomNameRules = [
  (value: string) => {
    if (value?.length > 0 && value.length <= 50) return true;

    return "Room Name must be 1-50 characters.";
  },
];

const userRole = ref<number>();
const userRoleRules = [
  (value: number | undefined) => {
    if (value !== undefined) return true;

    return "Must select User Role.";
  },
];

const isFormValid = ref<boolean>();
const isSubmitting = ref<boolean>();

function handleSubmit(e: any) {
  console.log("hello", e);
}
</script>
<style lang="scss" scoped></style>
