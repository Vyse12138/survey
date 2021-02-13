import { createRouter, createWebHistory } from "vue-router";
import Home from "../views/Home.vue";
import Result from "../views/Result.vue";
import Survey from "../views/Survey.vue";
const routes = [
  {
    path: "/",
    component: Home
  },
  {
    path: "/survey",
    component: Survey
  },
  {
    path: "/result",
    component: Result
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
