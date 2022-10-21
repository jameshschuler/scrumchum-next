import { createApp } from 'vue';
import { createRouter, createWebHistory } from 'vue-router';
import Landing from '@/components/Landing.vue';
import App from './App.vue';
import 'bulma';

const routes = [{ path: '/', component: Landing }];

const router = createRouter({
  history: createWebHistory(),
  routes,
});
const app = createApp(App);

app.use(router);

app.mount('#app');
