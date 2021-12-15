<template>
  <div id="app">
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <router-link to="/favorites"> |  Favorites </router-link>
      <router-link to= "/posts"> |  Posts </router-link>
      <router-link to ="/upload"> |  Upload</router-link>
      <router-link to = "/update"> | Update</router-link>
    </div>
    <router-view />
  </div>
</template>
<style >
@import "/style.css";
</style>
