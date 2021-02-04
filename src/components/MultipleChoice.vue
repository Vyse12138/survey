<template>
  <div class="MultipleChoice">
    <!-- question and * mark -->
    <h2 class="questionHeading">
      {{ body }}
      <span class="isCompulsory" v-if="isCompulsory">*</span>
    </h2>
    <!-- table for choices -->
    <table class="questionBody">
      <tr>
        <th></th>
        <!-- given options -->
        <th
          class="option"
          v-for="option in items[0].options"
          v-bind:key="option"
        >
          {{ option }}
        </th>
      </tr>
      <!-- each item and its radios input -->
      <tr class="choice" v-for="item in items" v-bind:key="item">
        <th>{{ item.name }}</th>
        <td class="option" v-for="option in item.options" v-bind:key="option">
          <!-- binding input id, name and value -->
          <input
            type="radio"
            :id="item.id"
            :value="option"
            :name="id + '-' + item.name"
            @click="saveAnswer"
          />
        </td>
      </tr>
    </table>
  </div>
</template>

<script>
export default {
  name: "MultipleChoice",
  props: {
    body: String,
    isCompulsory: Number,
    items: Array,
    id: String
  },
  methods: {
    // update answer
    saveAnswer(e) {
      this.$emit("sendAnswer", this.id, e.target.id, e.target.value);
    }
  }
};
</script>

<style scoped lang="scss">
.MultipleChoice {
  margin: 20px;
  padding: 20px;
  padding-top: 10px;
  margin-left: auto;
  margin-right: auto;
  box-shadow: 0 0.125em 0.275em 0 rgba(0, 0, 0, 0.125);
  background-color: white;
  border-radius: 15px;
  width: 60%;
  .questionHeading {
    text-align: left;
    .isCompulsory {
      color: red;
    }
  }
  .questionBody {
    width: 100%;
    border-spacing: 15px;
    tr {
      font-size: 1.2em;
      input[type="radio"] {
        border: 0px;
        width: 1.3em;
        height: 1.3em;
      }
    }
  }
}
</style>
