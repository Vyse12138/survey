<template>
  <div class="home">
    <router-link to="/survey" class="button">
      SURVEY
    </router-link>
  </div>
</template>

<script>
export default {
  name: "Home"
};
</script>

<style lang="scss" scoped>
.button {
  background-color: red;
  box-shadow: 0 10px 0 darkred;
  color: white;
  padding: 1em;
  margin: 20px;
  position: relative;
  text-decoration: none;
  border-radius: 10px;
}

.button:hover {
  background-color: #ce0606;
}

.button:active {
  box-shadow: none;
  top: 10px;
}
</style>
