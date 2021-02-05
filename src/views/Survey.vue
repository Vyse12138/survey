<template>
  <div class="home">
    <!-- front image -->
    <div class="frontImg" />

    <!-- loading and error indicator section -->
    <h2 class="loading" v-if="loading">Loading...</h2>
    <h2 class="loading" v-if="error">There was some error...</h2>

    <!-- survey heading section -->
    <div class="heading" v-if="!loading && !error">
      <h1>{{ title }}</h1>
      <h3>{{ description }}</h3>
    </div>

    <!-- questions section -->
    <MultipleChoice
      v-for="question in questions"
      v-bind:key="question.id"
      @sendAnswer="saveAnswer"
      :body="question.body"
      :isCompulsory="question.isCompulsory"
      :items="question.items"
      :id="question.id"
      :unfinished="unfinished[question.id]"
    />
    <OpenEnded
      body="Any feedback?"
      isCompulsory="0"
      :unfinished="false"
      v-if="!loading && !error"
    />
    <!-- show submit button when not loading and no error -->
    <button class="submit" v-if="!loading && !error" v-on:click="submitSurvey">
      submit
    </button>
  </div>
</template>

<script>
import MultipleChoice from "@/components/Survey/MultipleChoice.vue";
import OpenEnded from "@/components/Survey/OpenEnded.vue";
import axios from "axios";

export default {
  name: "Home",
  components: {
    MultipleChoice,
    OpenEnded
  },
  data() {
    return {
      //response data
      title: "",
      description: "",
      questions: [],
      //indicator for loading and fetching error
      loading: true,
      error: false,
      //indicator for unfinished compulsory questions
      unfinished: []
    };
  },
  mounted() {
    // fetching data from api
    axios
      .get("https://run.mocky.io/v3/3450b206-246f-4c05-bfb9-41e1a165700e")
      .then(response => {
        this.title = response.data.title;
        this.description = response.data.description;
        this.questions = response.data.questions;
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
        .items.find(e => e.name === itemID).answer = value;
    },
    // function to post data
    submitSurvey(e) {
      e.preventDefault();
      // input validation to ensure every compulsory question is answered
      this.questions.forEach(question => {
        if (question.isCompulsory === 1) {
          question.items.forEach(item => {
            this.unfinished[question.id] = false;
            if (item.answer === null) {
              this.unfinished[question.id] = true;
              setTimeout(() => {
                document.getElementById(question.id).scrollIntoView({
                  behavior: "smooth",
                  block: "center"
                });
              }, 0);
              throw new Error("A compulsory question is not being answered.");
            }
          });
        }
      });
      // post answers to server
      axios
        .post("url goes here", this.questions)
        .then(() => {
          // testing
          console.log(this.questions);
          alert("Submit successful");
          this.$router.push("/result");
        })
        .catch(() => {
          // testing
          console.log(this.questions);
          alert("submit failed");
          this.$router.push("/result");
        });
    }
  }
};
</script>

<style scoped lang="scss">
html {
  scroll-behavior: smooth;
}
.home {
  width: 60%;
  margin-left: auto;
  margin-right: auto;
  // image section
  .frontImg {
    margin: 20px 0;
    box-shadow: 0 0.125em 0.275em 0 rgba(0, 0, 0, 0.125);
    border-radius: 15px;
    height: 240px;
    background-image: url(../assets/front.png);
    background-size: 100% 100%;
  }
  // heading section
  .heading {
    text-align: left;
    margin: 20px 0;
    padding: 20px;
    padding-top: 10px;
    box-shadow: 0 0.125em 0.275em 0 rgba(0, 0, 0, 0.125);
    background-color: white;
    border-top: 15px solid #6c7c1c;
    border-radius: 15px;
    h1 {
      font-size: 2.4em;
    }
    h3 {
      color: gray;
    }
  }
  // submit button
  .submit {
    display: inline-block;
    background-color: #4caf50;
    box-shadow: 0 8px 0 #999;
    font-size: 24px;
    cursor: pointer;
    text-align: center;
    outline: none;
    border: none;
    color: white;
    margin: 20px;
    padding: 15px 25px;
    border-radius: 10px;
    &:hover {
      background-color: #3e8e41;
    }
    &:active {
      box-shadow: 0 5px #666;
      background-color: #3e8e41;
      transform: translateY(4px);
    }
  }
}
@media (max-width: 1024px) {
  .home {
    width: 100%;
  }
}
</style>
