<template>
  <!-- results page -->
  <div class="result" v-if="securityCheck()">
    <!-- survey heading section -->
    <div class="heading">
      <h1>{{ title }}</h1>
      <h3>{{ description }}</h3>
    </div>

    <div v-for="result in results" v-bind:key="result.id">
      <!-- pie charts -->
      <ResultPieChart
        v-if="result.items.length === 1"
        :question="result.body"
        :items="result.items"
        :id="result.id"
      />
      <!-- bar charts -->
      <ResultBarChart
        v-if="result.items.length !== 1"
        :question="result.body"
        :items="result.items"
        :id="result.id"
      />
    </div>
  </div>
</template>

<script>
import ResultPieChart from "@/components/Result/ResultPieChart.vue";
import ResultBarChart from "@/components/Result/ResultBarChart.vue";
import axios from "axios";

export default {
  name: "Result",
  components: {
    ResultPieChart,
    ResultBarChart,
  },
  props: {
    surveyID: String,
    securityKey: String,
  },
  data() {
    return {
      //response data
      resultSet: [],
      title: "",
      description: "",
      results: [],
      //indicator for loading and fetching error
      loading: true,
      error: false,
    };
  },
  methods: {
    //security check
    securityCheck() {
      if (this.securityKey.match(/kyle/)) {
        return true;
      } else {
        return false;
      }
    },
  },
  created() {
    // fetching data from api
    axios
      // get result data (shuold be replace with a url with this.surveyID)
      .get("https://run.mocky.io/v3/1443b031-b427-49c3-8442-32ab520d9f2e")
      .then((response) => {
        // assigning respnse to corresponding data

        // all the results set
        this.resultSet = response.data;
        // result title and description
        this.title = this.resultSet[0].surveyName;
        this.description = this.resultSet[0].description;
        // array of quesitons and vote conunts
        this.results = [];
        // assign template for each question
        // iteratate each question
        for (const question of this.resultSet[0].questions) {
          if (question.isCompulsory === 1 && question.items.length !== 0) {
            // temp to represent each question
            let temp = {};
            // set id and body for each question
            temp.id = question.id;
            temp.question = question.body;
            // iterate the items to put them in the temp
            temp.items = [];
            for (let item of question.items) {
              // temp for each item
              let tempItem = {};
              tempItem.name = item.name;
              tempItem.result = {};
              // iterate the options
              for (let option of item.options) {
                tempItem.result[option] = 0;
              }
              temp.items.push(tempItem);
            }
            this.results.push(temp);
          }
        }
        for (const indivdual of this.resultSet) {
          if (indivdual.isCompleted) {
            console.log(indivdual);
            for (const question of indivdual.questions) {
              if (question.isCompulsory && question.items.length !== 0) {
                console.log(question);
                for (const item of question.items) {
                  console.log(item);
                  let i = 0;
                  let j = 0;
                  let k = "poor";
                  this.results[i].items[j].result[k] += 1;
                }
              }
            }
          }
        }
      })
      .catch(() => {
        // when there is an error, show error message
        this.error = true;
      })
      .finally(() => {
        // finish loading
        this.loading = false;
      });
  },
};
</script>

<style scoped lang="scss">
// result page styling
.result {
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
}
// responsive design
@media (max-width: 1024px) {
  .result {
    width: 100%;
  }
}
</style>
