<template>
  <div class="gratitude">
    <h1>Thank you!</h1>
  </div>
</template>

<script>
export default {
  name: "Gratitude"
};
</script>

<style lang="scss" scoped>
.button {
  display: inline-block;
  background-color: #4caf50;
  box-shadow: 0 8px 0 #999;
  color: white;
  padding: 1em;
  margin: 20px;
  position: relative;
  text-decoration: none;
  border-radius: 10px;
  &:hover {
    background-color: #3e8e41;
  }
  &:active {
    box-shadow: 0 5px #666;
    background-color: #3e8e41;
    transform: translateY(4px);
  }
}
</style>
