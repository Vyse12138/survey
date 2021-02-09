<template>
  <div class="multipleChoice">
    <!-- question and * mark -->
    <h3 class="questionBody">
      {{ body }}
      <span class="isCompulsory" v-if="isCompulsory">*</span>
    </h3>

    <!-- warning section if required question is not being answered -->
    <div :id="id" class="required" v-if="unfinished">
      <h3>Please finish this question!</h3>
    </div>

    <!-- question items section: -->

    <!-- 1. question with one item -->
    <table class="questionItem" v-if="items.length === 1">
      <tr class="" v-for="option in getFirstItemOptions" v-bind:key="option">
        <th>{{ option }}</th>
        <!-- given options -->
        <td>
          <input
            type="radio"
            :value="option"
            :name="id"
            @click="saveAnswerWithSingleItem"
          />
        </td>
      </tr>
    </table>

    <!-- 2. question with multiple items -->
    <table class="questionItems" v-if="items.length !== 1">
      <!-- row for the options -->
      <tr>
        <th></th>
        <!-- given options -->
        <th v-for="option in getFirstItemOptions" v-bind:key="option">
          {{ option }}
        </th>
      </tr>
      <!-- each item and its radios input -->
      <tr class="choice" v-for="item in getItems" v-bind:key="item">
        <!-- item name -->
        <th>{{ item.name }}</th>
        <!-- radio inputs -->
        <td v-for="option in item.options" v-bind:key="option">
          <!-- binding input id, name and value -->
          <input
            type="radio"
            :id="item.name"
            :value="option"
            :name="id + '-' + item.name"
            @click="saveAnswerWithMultipleItems"
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
    unfinished: Boolean
  },
  computed: {
    getFirstItemOptions() {
      return this.items[0].options;
    },
    getItems() {
      return this.items;
    }
  },
  methods: {
    // update answer
    saveAnswerWithSingleItem(e) {
      this.$emit("saveAnswerWithSingleItem", this.id, e.target.value);
    },
    saveAnswerWithMultipleItems(e) {
      this.$emit(
        "saveAnswerWithMultipleItems",
        this.id,
        e.target.id,
        e.target.value
      );
    }
  }
};
</script>

<style scoped lang="scss">
// question box
.multipleChoice {
  margin: 20px 0;
  padding: 20px;
  padding-top: 10px;
  box-shadow: 0 0.125em 0.275em 0 rgba(0, 0, 0, 0.125);
  background-color: white;
  border-left: 10px solid white;
  &:hover {
    border-left: 10px solid #4285f4;
  }
  border-radius: 10px;
  // question heading
  .questionBody {
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
