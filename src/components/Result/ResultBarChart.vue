<template>
  <div class="resultBarChart">
    aasd
  </div>
</template>

<script>

export default {
  name: "ResultBarChart",
  props: {
    question: String,
    items: Array,
    id: String
  },
  mounted() {

  }
};
</script>

<style scoped lang="scss">
// question box
.resultBarChart {
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
