<template>
  <div>
    <login-card />
  </div>
</template>

<script>
import LoginCard from '../components/LoginCard.vue';



export default {
  name: "login",
  components: {LoginCard},
};
</script>
<style>
  html {
    background-image: url("/loginBackground.png");
    background-position: top;  
    background-size: 100%;
    }
</style>