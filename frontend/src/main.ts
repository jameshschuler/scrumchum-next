import { HubConnectionBuilder } from "@microsoft/signalr";
import { createPinia } from "pinia";
import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";

import "@/assets/scss/app.scss";

// Vuetify
import { createVuetify } from "vuetify";
import * as components from "vuetify/components";
import * as directives from "vuetify/directives";
import "vuetify/styles";

const vuetify = createVuetify({
  components,
  directives,
});

const app = createApp(App);

const pinia = createPinia();

app.use(vuetify);
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
