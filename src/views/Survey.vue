<template>
  <div class="home">
    <h1>Survey</h1>
    <!-- loading and error section -->
    <h2 class="loading" v-if="loading">Loading...</h2>
    <h2 class="loading" v-if="error">There was some error...</h2>
    <!-- questions section -->
    <div
      class="questions"
      v-for="question in questions"
      v-bind:key="question.id"
    >
      <MultipleChoice
        @sendAnswer="saveAnswer"
        :body="question.body"
        :isCompulsory="question.isCompulsory"
        :items="question.items"
        :id="question.id"
      />
    </div>
    <!-- show submit button when not loading and no error -->
    <button v-if="!loading && !error" v-on:click="submitSurvey">submit</button>
  </div>
</template>

<script>
import MultipleChoice from "@/components/MultipleChoice.vue";
import axios from "axios";
export default {
  name: "Home",
  components: {
    MultipleChoice
  },
  data() {
    return {
      questions: [],
      loading: true,
      error: false
    };
  },
  mounted() {
    // fetching data from api
    axios
      .get("https://601b7c3559fa2c0017560ad0.mockapi.io/questions")
      .then(response => {
        this.questions = response.data;
      })
      .catch(() => {
        this.error = true;
      })
      .finally(() => {
        this.loading = false;
      });
  },
  methods: {
    // update answer at radio checked
    saveAnswer(questionID, itemID, value) {
      this.questions
        .find(e => e.id === questionID)
        .items.find(e => e.id === itemID).answer = value;
    },
    // post answers back
    submitSurvey() {
      // input validation to ensure every compulsory question is answered
      axios
        .post("url goes here", this.questions)
        .then(() => {
          alert("submitted");
        })
        .catch(() => {
          this.error = true;
        })
        .finally(() => {
          this.loading = false;
        });
    }
  }
};
</script>

<style scoped lang="scss"></style>
