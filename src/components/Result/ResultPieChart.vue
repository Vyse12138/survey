<template>
  <div class="resultPieChart">
    <VueEcharts :option="option" style="height: 300px" ref="chart" />
  </div>
</template>

<script>
import { VueEcharts } from "vue3-echarts";

export default {
  name: "ResultPieChart",
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
      option: {
        title: {
          textAlign: "left",
          text: `${this.question}`,
          subtext: "",
          left: "left",
        },
        tooltip: {
          trigger: "item",
        },
        legend: {
          orient: "vertical",
          right: "20%",
          top: "middle",
        },
        series: [
          {
            type: "pie",
            radius: "70%",
            center: ["30%", "50%"],
            data: [],
            label: {
              normal: {
                show: true,
                position: "inside",
                formatter: "{d}%",
              },
            },
            emphasis: {
              itemStyle: {
                shadowBlur: 10,
                shadowOffsetX: 0,
                shadowColor: "rgba(0, 0, 0, 0.5)",
              },
            },
          },
        ],
      },
    };
  },

  created() {
    for (const result in this.getResults) {
      this.resultsSum += this.getResults[result];
    }
    this.option.title.subtext = `${this.resultsSum} responses`;
    let votes = [];
    for (const result in this.getResults) {
      let temp = { value: this.getResults[result], name: result };
      votes.push(temp);
    }
    this.option.series[0].data = votes;
  },
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
}
</style>
