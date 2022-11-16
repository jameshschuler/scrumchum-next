<template>
  <Header />
  <Main />
  <Footer />
</template>
<script setup lang="ts">
import Header from '@/components/Header.vue';
import Main from '@/components/Main.vue';
import Footer from '@/components/Footer.vue';
import { useCommonStore } from '@/stores/common';

useCommonStore().setupSocketConnection();
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
