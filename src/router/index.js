import { createRouter, createWebHistory } from "vue-router";
import Result from "../views/Result.vue";
import Survey from "../views/Survey.vue";
import ErrorPage from "../views/ErrorPage.vue";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/:guid",
      component: Survey,
      props: true
    },
    {
      path: "/result/:surveyID/:securityKey",
      component: Result,
      props: true
    },
    {
      path: "/:pathMatch(.*)*",
      component: ErrorPage,
      props: true
    }
  ]
});

export default router;
