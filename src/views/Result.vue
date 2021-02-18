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
      .get("https://run.mocky.io/v3/6918a5dc-d23b-4913-b560-57e1bc0e7336")
      .then((response) => {
        // assigning respnse to corresponding data

        this.resultSet = response.data;
        this.title = this.resultSet[0].surveyName;
        this.description = this.resultSet[0].description;
        this.results = [];
        for (const question of this.resultSet[0].questions) {
          question.isCompulsory === 1 &&
            question.items.length !== 0 &&
            this.results.push(question);
        }
        let votes = [];
        for (const res of this.results) {
          let vote = {};
          for (const opt of res.items[0].options) {
            vote[opt] = 0;
          }
          votes.push(vote);
        }
        console.log(votes)
        for (const result of this.results) {
          let resultItems = [];
          for (const item of result.items) {
            resultItems.push({
              name: item.name,
              result: {
                "very pool": 11,
                poor: 22,
                average: 33,
              },
            });
          }

          result.items = resultItems;
        }
        console.log(this.results);
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
