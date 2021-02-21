<template>
  <div class="resultBarChart">
    <VueEcharts :option="option" style="height: 300px" />
  </div>
</template>

<script>
import { VueEcharts } from "vue3-echarts";

export default {
  name: "ResultBarChart",
  components: {
    VueEcharts
  },
  computed: {
    getResults() {
      return this.items[0].result;
    }
  },
  props: {
    question: String,
    items: Array,
    id: String
  },
  data() {
    return {
      // option for plotting chart
      option: {
        title: {
          textAlign: "left",
          text: `${this.question}`,
          subtext: "",
          left: "left"
        },
        tooltip: {
          trigger: "axis",
          axisPointer: {
            type: "shadow"
          }
        },
        legend: {
          data: []
        },
        xAxis: {
          type: "category",
          axisTick: { show: false },
          data: []
        },
        yAxis: {
          type: "value"
        },
        series: []
      }
    };
  },

  created() {
    // update responses sum
    let resultsSum = 0;
    for (const result in this.getResults) {
      resultsSum += this.getResults[result];
    }
    // display it on subtitle
    this.option.title.subtext = `${resultsSum} responses`;

    // update chart legends
    let legendData = [];
    for (const result in this.items[0].result) {
      legendData.push(result);
    }
    this.option.legend.data = legendData;

    // update legends on x axis
    let xAxisData = [];
    for (const item of this.items) {
      xAxisData.push(item.name);
    }
    this.option.xAxis.data = xAxisData;

    // update responses data
    let seriesData = [],
      l = legendData.length;
    while (l--) {
      let tempData = { type: "bar", name: legendData[l] };
      let tempResult = [];
      for (const item of this.items) {
        tempResult.push(item.result[legendData[l]]);
      }
      tempData.data = tempResult;
      seriesData.unshift(tempData);
    }
    this.option.series = seriesData;
  }
};
</script>

<style scoped lang="scss">
// result section
.resultBarChart {
  margin: 20px 0;
  padding: 20px;
  padding-top: 10px;
  box-shadow: 0 0.125em 0.275em 0 rgba(0, 0, 0, 0.125);
  background-color: white;
  border-radius: 10px;
}
</style>
