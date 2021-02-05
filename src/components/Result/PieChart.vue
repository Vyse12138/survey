<template>
  <div class="pieChart">
    <!-- question and * mark -->
    <h3 class="question">{{ question }}</h3>
    <p class="question">{{ response.get(items[0].name) }} respones</p>
    <div class="table" v-for="item in items" v-bind:key="item.name">
      <div class="pie"></div>
      <div class="info">
        <p>{{ item.name }}</p>
        <p v-for="(value, option) in item.result" v-bind:key="option"> 
          {{ option }}: {{ value }}
        </p>
      </div>
      <div class="a" />
    </div>
  </div>
</template>

<script>
export default {
  name: "PieChart",
  data() {
    return {
      response: new Map()
    };
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
    font-weight: bold;
  }
  .table {
    .pie {
      float: left;
      width: 200px;
      height: 200px;
      border-radius: 50%;
      background: yellowgreen;
      margin: 20px 50px;
      background: conic-gradient(deeppink 20%, #fb3 0, #fb3 30%, yellowgreen 0);
    }
    .info {
      width: 40%;
      float: right;
      text-align: left;
      font-weight: bold;
    }
    .a {
      height: 20px;
      clear: both;
    }
  }

}
</style>
