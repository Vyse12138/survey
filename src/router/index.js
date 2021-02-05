import { createRouter, createWebHashHistory } from "vue-router";
import Home from "../views/Home.vue";

const routes = [
  {
    path: "/",
    component: Home
  },
  {
    path: "/survey",
    // route level code-splitting, lazy-loaded when visited.
    component: () => import("../views/Survey.vue")
  },
  {
    path: "/result",
    // route level code-splitting, lazy-loaded when visited.
    component: () => import("../views/Result.vue")
  }
];

const router = createRouter({
  history: createWebHashHistory(),
  routes
});

export default router;
