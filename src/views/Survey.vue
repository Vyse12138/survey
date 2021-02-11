<template>
  <div class="home">
    <!-- front image -->
    <div class="frontImg" />

    <!-- loading and error indicator section -->
    <h2 class="loading" v-if="loading">Loading...</h2>
    <h2 class="loading" v-if="error">There was some error...</h2>

    <!-- survey section -->
    <div v-if="!loading && !error">
      <!-- survey heading section -->
      <div class="heading" v-if="!loading && !error">
        <h1>{{ data.title }}</h1>
        <h3>{{ data.description }}</h3>
      </div>
      <div v-for="question in data.questions" v-bind:key="question.id">
        <!-- question section -->
        <MultipleChoice
          v-if="question.items"
          @saveAnswerWithSingleItem="saveAnswerWithSingleItem"
          @saveAnswerWithMultipleItems="saveAnswerWithMultipleItems"
          :body="question.body"
          :isCompulsory="question.isCompulsory"
          :items="question.items"
          :id="question.id"
          :unfinished="unfinished[question.id]"
        />
        <OpenEnded
          v-if="!question.items"
          @saveAnswer="saveAnswer"
          :id="question.id"
          :body="question.body"
          :isCompulsory="question.isCompulsory"
          :unfinished="unfinished[question.id]"
        />
      </div>
      <!-- show submit button when not loading and no error -->
      <button class="submit" @click.prevent="submitSurvey">
        submit
      </button>
    </div>
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
      data: {
        title: "",
        description: "",
        questions: [
          {
            id: "",
            body: "",
            isCompulsory: 0,
            items: [
              {
                name: "",
                answer: null,
                options: []
              }
            ]
          }
        ]
      },
      //indicator for loading and fetching error
      loading: true,
      error: false,
      //indicator for unfinished compulsory questions
      unfinished: []
    };
  },
  created() {
    // fetching data from api
    axios
      .get("https://run.mocky.io/v3/6469b3d9-9fa7-4e90-938c-86b2f7a1c136")
      .then(response => {
        this.data = response.data;
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
    saveAnswerWithSingleItem(questionID, value) {
      this.data.questions.find(
        e => e.id === questionID
      ).items[0].answer = value;
    },
    saveAnswerWithMultipleItems(questionID, itemID, value) {
      this.data.questions
        .find(e => e.id === questionID)
        .items.find(e => e.name === itemID).answer = value;
    },
    saveAnswer(questionID, value) {
      this.data.questions.find(e => e.id === questionID).answer = value;
    },
    // function to post data
    submitSurvey() {
      // input validation to ensure every compulsory question is answered
      this.data.questions.forEach(question => {
        if (question.isCompulsory === 1) {
          if (question.items) {
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
          } else {
            this.unfinished[question.id] = false;
            if (question.answer === null) {
              this.unfinished[question.id] = true;
              setTimeout(() => {
                document.getElementById(question.id).scrollIntoView({
                  behavior: "smooth",
                  block: "center"
                });
              }, 0);
              throw new Error("A compulsory question is not being answered.");
            }
          }
        }
      });
      // post answers to server
      axios
        .post("url goes here", this.data)
        .then(() => {
          // testing
          console.log(this.data);
          alert("Submit successful");
          this.$router.push("/result");
        })
        .catch(() => {
          // testing
          console.log(this.data);
          alert("submit failed");
          this.$router.push("/result");
        });
    }
  }
};
</script>

<style scoped lang="scss">
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
