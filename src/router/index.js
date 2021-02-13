import { createRouter, createWebHistory } from "vue-router";
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
  },
  {
    path: "/gratitude",
    // route level code-splitting, lazy-loaded when visited.
    component: () => import("../views/Gratitude.vue")
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
