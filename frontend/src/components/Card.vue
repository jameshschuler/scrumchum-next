<template>
  <v-card class="card border w-25 h-100 d-flex justify-center align-center mx-2 clickable">
    <h1>{{ value }}</h1>
  </v-card>
</template>
<script setup lang="ts">
defineProps({
  value: {
    type: String,
    required: true,
  },
});
</script>
<style lang="scss" scoped>
.card {
  min-width: 150px;
}
</style>
