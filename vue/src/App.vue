<template>
  <div id="app">
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <router-link to="/favorites"> |  Favorites </router-link>
      <router-link to= "/posts"> |  Posts </router-link>
      <router-link to ="/upload"> |  Upload</router-link>
    </div>
    <router-view />
  </div>
</template>
<style >
@import "https://cdn.jsdelivr.net/npm/bulma@0.9.3/css/bulma.min.css"; 
</style>
