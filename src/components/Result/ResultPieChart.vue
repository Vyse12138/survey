<template>
  <div class="resultPieChart">
    <!-- question and * mark -->
    <h3 class="question">{{ question }}</h3>
    <div class="flex">
      <canvas class="canvas" width="300" height="300" />
      <ul>
        <li class="mark" v-for="(result, name) in getResults" v-bind:key="name">
          {{ name }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: "ResultPieChart",
  computed: {
    getResults() {
      return this.items[0].result;
    }
  },
  data() {
    return {
      resultsSum: 0,
      canvas: null
    };
  },
  props: {
    question: String,
    items: Array,
    id: String
  },
  mounted() {
    for (const result in this.getResults) {
      this.resultsSum += this.getResults[result];
    }
  }
};
</script>

<style scoped lang="scss">
// question box
.resultPieChart {
  margin: 20px 0;
  padding: 20px;
  padding-top: 10px;
  box-shadow: 0 0.125em 0.275em 0 rgba(0, 0, 0, 0.125);
  background-color: white;
  border-radius: 10px;
  // question heading
  .question {
    text-align: left;
    font-weight: bold;
  }
  .flex {
    display: flex;
    flex-wrap: wrap;
    .canvas {
      background-color: gray;
      width: 50%;
    }
    .mark {
      text-align: left;
      flex: 100%;
    }
  }
}
</style>
