import { HubConnectionBuilder } from "@microsoft/signalr";
import { createPinia } from "pinia";
import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";

const app = createApp(App);

const pinia = createPinia();

app.use(router);
app.use(pinia);

const baseUrl = "https://localhost:7032";

// TODO: handle connection error better than this
(async () => {
  try {
    const connection = new HubConnectionBuilder().withUrl(`${baseUrl}/room`).build();

    await connection.start();
    app.provide("hubConnection", connection);
  } catch (err) {
    console.error(err);
  } finally {
    app.mount("#app");
  }
})();
