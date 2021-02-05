<template>
  <div class="openEnded">
    <!-- question and * mark -->
    <h3 class="questionBody">
      {{ body }}
      <span class="isCompulsory" v-if="isCompulsory === '1'">*</span>
    </h3>

    <!-- warning section if required question is not being answered -->
    <div class="required" v-if="unfinished">
      <h3>Please finish this question!</h3>
    </div>

    <!-- input area -->
    <textarea class="input" rows="10" />
  </div>
</template>

<script>
export default {
  name: "OpenEnded",
  props: {
    body: String,
    isCompulsory: String,
    unfinished: Boolean
  }
};
</script>

<style scoped lang="scss">
// question box
.openEnded {
  margin: 20px 0;
  padding: 20px;
  padding-top: 10px;
  box-shadow: 0 0.125em 0.275em 0 rgba(0, 0, 0, 0.125);
  background-color: white;
  border-left: 10px solid #4285f4;
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
  // input
  .input {
    width: 100%;
    margin: 15px 0;
  }
}
</style>
