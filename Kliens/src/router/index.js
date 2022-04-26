import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: HomeView,
    },
    {
      path: "/eredmenyek/:formId",
      name: "eredmenyekDisplay",
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import("../components/display.vue"),
      props: true,
    },
    {
      path: "/eredmenyek",
      name: "eredmenyek",
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import("../views/Eredmenyek.vue"),
    },
    {
      path: "/szavazz/:formId",
      name: "szavazzForm",
      component: () => import("../views/SzavazzForm.vue"),
      props: true,
    },
    {
      path: "/szavazz",
      name: "szavazz",
      component: () => import("../views/Szavazz.vue"),
    },
    {
      path: "/kerdes",
      name: "kerdes",
      component: () => import("../views/Kerdes.vue"),
    },
  ],
});

export default router;
