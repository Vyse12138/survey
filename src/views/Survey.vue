<template>
  <!-- survey page -->
  <div class="survey">
    <!-- front image -->
    <div v-if="gratitude">
      <h1>Thank you for completing the survey!</h1>
    </div>
    <div v-if="saved">
      <h1>Please come back to it later!</h1>
      <button class="submit continue" @click.prevent="refresh">
        CONTINUE
      </button>
    </div>
    <div v-if="!gratitude && !saved">
      <div class="frontImg" />

      <!-- loading and error indicator section -->
      <h2 class="loading" v-if="loading">Loading...</h2>
      <h2 class="loading" v-if="error">There was some error...</h2>

      <!-- survey section -->
      <div v-if="!loading && !error">
        <!-- survey heading section -->
        <div class="heading" v-if="!loading && !error">
          <h1>{{ data.surveyName }}</h1>
          <h3>{{ data.description }}</h3>
        </div>

        <!-- question section -->
        <div v-for="question in data.questions" v-bind:key="question.id">
          <!-- multiple choice questions -->
          <MultipleChoice
            v-if="question.items.length > 0"
            @saveAnswerWithSingleItem="saveAnswerWithSingleItem"
            @saveAnswerWithMultipleItems="saveAnswerWithMultipleItems"
            :body="question.body"
            :isCompulsory="question.isCompulsory"
            :items="question.items"
            :id="question.id"
            :unfinished="unfinished[question.id]"
          />
          <!-- open ended questions -->
          <OpenEnded
            v-if="question.items.length === 0"
            @saveAnswer="saveAnswer"
            :id="question.id"
            :body="question.body"
            :isCompulsory="question.isCompulsory"
            :unfinished="unfinished[question.id]"
            :answer="question.answer"
          />
        </div>
        <!-- show save button when not loading and no error -->
        <button class="submit save" @click.prevent="saveSurvey">
          SAVE
        </button>
        <!-- show submit button when not loading and no error -->
        <button class="submit" @click.prevent="submitSurvey">
          SUBMIT
        </button>
      </div>
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
  props: {
    guid: String
  },
  data() {
    return {
      //response data with template data type
      data: {
        uniqueID: "",
        surveyID: "",
        surveyName: "",
        description: "",
        email: null,
        isCompleted: 0,
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
      unfinished: [],
      gratitude: false,
      saved: false
    };
  },
  created() {
    let guid = window.location.pathname.substring(1);
    console.log(guid);
    // -----------------------
    // fetching data from api
    axios
      // .get(`/api/userSurvey${guid}`)
      // mockUnfinishedQuestion: https://run.mocky.io/v3/16282fa7-5087-4ef5-938c-24db50727c51
      // mockEmptyQuestion: https://run.mocky.io/v3/d61f43dc-4082-4b03-b05e-efdfeb0a2ded
      .get("https://run.mocky.io/v3/16282fa7-5087-4ef5-938c-24db50727c51")
      .then(response => {
        // assigning data
        this.data = response.data;
        if (this.data.isCompleted) {
          this.gratitude = true;
        }
      })
      .catch(() => {
        // show error message
        this.error = true;
      })
      .finally(() => {
        // finish loading
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

    // update answer at radio checked
    saveAnswerWithMultipleItems(questionID, itemID, value) {
      this.data.questions
        .find(e => e.id === questionID)
        .items.find(e => e.name === itemID).answer = value;
    },

    // update answer when inputing to open ended question
    saveAnswer(questionID, value) {
      this.data.questions.find(e => e.id === questionID).answer = value;
    },

    // method to refresh page
    refresh() {
      window.location.reload();
    },

    // function to save Survey
    saveSurvey() {
      // post answers to server
      axios
        .post("/api/userSurvey", this.data)
        .then(() => {
          // this.$router.push("/gratitude");
          this.saved = true;
        })
        .catch(() => {
          // testing upload failed
          console.log(this.data);
          alert("submit failed");
          // this.$router.push("/gratitude");
          this.saved = true;
        });
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
                  // scroll to warning section
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
                // scroll to warning section
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

      //set survey to completed
      this.data.isCompleted = 1;
      // post answers to server
      axios
        .post("/api/userSurvey", this.data)
        .then(() => {
          // go to gratitude page
          // this.$router.push("/gratitude");
          this.gratitude = true;
        })
        .catch(() => {
          // testing upload failed
          console.log(this.data);
          alert("submit failed");
          // this.$router.push("/gratitude");
          this.gratitude = true;
        });
    }
  }
};
</script>

<style lang="scss">
body {
  background-color: #f0f2e8;
}
// survey page
.survey {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
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
    width: 150px;
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
    // hover and active effect
    &:hover {
      background-color: #3e8e41;
    }
    &:active {
      box-shadow: 0 5px #666;
      background-color: #3e8e41;
      transform: translateY(4px);
    }
  }
  .save {
    width: 150px;
    background-color: red;
    &:hover {
      background-color: #c50000;
    }
    &:active {
      box-shadow: 0 5px #666;
      background-color: #c50000;
      transform: translateY(4px);
    }
  }
  .continue {
    width: auto;
  }
}
// responsive design
@media (max-width: 1024px) {
  .survey {
    width: 100%;
  }
}
</style>
