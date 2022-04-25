import App from "./App.vue";
import router from "./router";
import { createApp } from "vue";
import { plugin, defaultConfig } from "@formkit/vue";
import "@formkit/themes/genesis";

const app = createApp(App).use(plugin, defaultConfig);

app.use(router);

app.mount("#app");
