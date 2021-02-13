<template>
  <div class="resultBarChart">
    <VueEcharts :option="option" style="height: 300px" ref="chart" />
  </div>
</template>

<script>
import { VueEcharts } from "vue3-echarts";

export default {
  name: "ResultBarChart",
  components: {
    VueEcharts,
  },
  computed: {
    getResults() {
      return this.items[0].result;
    },
  },
  props: {
    question: String,
    items: Array,
    id: String,
  },
  data() {
    return {
      resultsSum: 0,
      votes: [],
      labelOption: {
        show: true,
        position: "insideBottom",
        distance: 15,
        rotate: 90,
        align: "left",
        verticalAlign: "middle",
        formatter: "{c}",
        fontSize: 16,
      },

      option: {
        tooltip: {
          trigger: "axis",
          axisPointer: {
            type: "shadow",
          },
        },
        legend: {
          data: [],
        },
        xAxis: {
          type: "category",
          axisTick: { show: false },
          data: [],
        },
        yAxis: {
          type: "value",
        },

        series: [
          {
            name: "Forest",
            type: "bar",
            barGap: 0,
            label: this.labelOption,

            data: [320, 332, 301, 334, 390],
          },
          {
            name: "Steppe",
            type: "bar",
            label: this.labelOption,

            data: [220, 182, 191, 234, 290],
          },
          {
            name: "Desert",
            type: "bar",
            label: this.labelOption,

            data: [150, 232, 201, 154, 190],
          },
          {
            name: "Wetland",
            type: "bar",
            label: this.labelOption,

            data: [98, 77, 101, 99, 40],
          },
        ],
      },
    };
  },

  created() {
    let legendData = [];
    for (const result in this.items[0].result) {
      legendData.push(result);
    }
    this.option.legend.data = legendData;

    let xAxisData = [];
    for (const item of this.items) {
      xAxisData.push(item.name);
    }
    this.option.xAxis.data = xAxisData;

    let seriesData = [],
      l = this.items.length;
    while (l--) {
      let tempData = { type: "bar", name: legendData[l] };
      let tempResult = [];
      for (const item of this.items) {
        tempResult.unshift(item.result[legendData[l]]);
      }
      tempData.data = tempResult;
      seriesData.unshift(tempData);
    }
    this.option.series = seriesData;
  },
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
}
</style>
