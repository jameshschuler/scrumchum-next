import LandingPage from "@/views/LandingPage.vue";
import LobbyPage from "@/views/LobbyPage.vue";
import { createRouter, createWebHistory } from "vue-router";
import { useRoomStore } from "./stores/roomStore";

const routes = [
  { path: "/", name: "Landing", component: LandingPage },
  { path: "/lobby/:roomCode", name: "Lobby", component: LobbyPage },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, _from, next) => {
  const roomStore = useRoomStore();

  if (to.name === "Lobby" && !roomStore.currentRoom) {
    next({ name: "Landing" });
  } else {
    next();
  }
});

export default router;
