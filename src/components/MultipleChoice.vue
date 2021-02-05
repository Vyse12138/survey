<template>
  <div class="MultipleChoice">
    <!-- question and * mark -->
    <h3 class="questionHeading">
      {{ body }}
      <span class="isCompulsory" v-if="isCompulsory">*</span>
    </h3>

    <!-- warning section if required question is not being answered -->
    <div :id="id" class="required" v-if="finished">
      <h3>Please finish this question!</h3>
    </div>

    <!-- question items section -->

    <!-- 1. question with one item -->
    <table class="questionItem" v-if="items.length === 1">
      <tr class="" v-for="option in items[0].options" v-bind:key="option">
        <th>{{ option }}</th>
        <!-- given options -->
        <td>
          <input
            type="radio"
            :id="items[0].name"
            :value="option"
            :name="id + '-' + items[0].name"
            @click="saveAnswer"
          />
        </td>
      </tr>
    </table>

    <!-- 2. question with multiple items -->
    <table class="questionItems" v-if="items.length !== 1">
      <tr>
        <th></th>
        <!-- given options -->
        <th
          class="choice"
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
            :id="item.name"
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
    id: String,
    finished: Boolean
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
// question box
.MultipleChoice {
  margin: 20px 0;
  padding: 20px;
  padding-top: 10px;
  box-shadow: 0 0.125em 0.275em 0 rgba(0, 0, 0, 0.125);
  background-color: white;
  border-left: 10px solid #4285f4;
  border-radius: 10px;
  // question heading
  .questionHeading {
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
  // question body for single item question
  .questionItem {
    border-spacing: 15px;
    font-size: 1.2em;
    text-align: left;
    clear: both;
    input[type="radio"] {
      width: 1.5em;
      height: 1.5em;
    }
  }
  // question body for mutiple items questions
  .questionItems {
    width: 100%;
    border-spacing: 15px;
    .choice {
      th {
        text-align: left;
      }
    }
    tr {
      font-size: 1.2em;
      input[type="radio"] {
        width: 1.5em;
        height: 1.5em;
      }
    }
  }
}
</style>
