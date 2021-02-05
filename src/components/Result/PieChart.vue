<template>
  <div class="pieChart">
    <!-- question and * mark -->
    <h3 class="question">{{ question }}</h3>
    <h5 class="question">{{ response.get(items[0].name) }} respones</h5>
    
  </div>
</template>

<script>
export default {
  name: "PieChart",
  data() {
    return {
      response: new Map()
    }
  },
  props: {
    question: String,
    items: Array,
    id: String
  },
  mounted() {
    this.items.forEach(item => {
      let result = item.result,
        responseCount = 0;
      for (const [, count] of Object.entries(result)) {
        responseCount += count;
      }
      this.response.set(item.name, responseCount);
    });
  },
};
</script>

<style scoped lang="scss">
// question box
.pieChart {
  margin: 20px 0;
  padding: 20px;
  padding-top: 10px;
  box-shadow: 0 0.125em 0.275em 0 rgba(0, 0, 0, 0.125);
  background-color: white;
  border-left: 10px solid #4285f4;
  border-radius: 10px;
  // question heading
  .question {
    text-align: left;
    .isCompulsory {
      color: red;
    }
  }
  // warning
  .required {
    background-color: #ff9800;
    color: white;
    border-radius: 10px;
    padding: 0px 30px;
    opacity: 0.66;
    float: left;
  }
  // input
  .input {
    width: 100%;
    margin: 15px 0;
  }
}
</style>
