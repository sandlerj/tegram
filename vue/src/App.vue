<template>
  <div id="app">
    <div class="is-hidden-touch" v-show="$store.state.token == ''" id="background-div">
      <img src="/loginBackground.png" >
    </div>
    <div id="nav" v-if="$store.state.token != ''" class="navbar">
      <div class="navbar-brand">
        <router-link class="logo navbar-item has-text-info" v-bind:to="{ name: 'home' }">TEgram</router-link>
        <a role="button" class="navbar-burger" aria-label="menu" aria-expanded="false" data-target="tegram-menu">
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
        </a>
      </div>
      <div id="tegram-menu" class="navbar-menu">
        <div class="navbar-start"></div>
        <div class="navbar-end">
          <router-link :to="{ name: 'home' }" class="navbar-item">Feed</router-link>
          <router-link to= "/posts" class="navbar-item">Profile</router-link>
          <router-link to ="/upload" class="navbar-item">Upload</router-link>
          <router-link to="/favorites" class="navbar-item">Favorites</router-link>
          <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''" class="navbar-item has-text-danger">Logout</router-link>
        </div>
      </div>
    </div>
    <router-view id="router-content" />
  </div>
</template>
<script>
// hamburger button script copied from bulma
document.addEventListener('DOMContentLoaded', () => {


  // Get all "navbar-burger" elements
  const $navbarBurgers = Array.prototype.slice.call(document.querySelectorAll('.navbar-burger'), 0);

  // Check if there are any navbar burgers
  if ($navbarBurgers.length > 0) {

    // Add a click event on each of them
    $navbarBurgers.forEach( el => {
      el.addEventListener('click', () => {

        // Get the target from the "data-target" attribute
        const target = el.dataset.target;
        const $target = document.getElementById(target);

        // Toggle the "is-active" class on both the "navbar-burger" and the "navbar-menu"
        el.classList.toggle('is-active');
        $target.classList.toggle('is-active');

      });
    });
  }

  /* When the user scrolls down, hide the navbar. When the user scrolls up, show the navbar */
var prevScrollpos = window.pageYOffset;
var touchedTop = true;
window.onscroll = function() {
  var currentScrollPos = window.pageYOffset;  
  if (currentScrollPos === 0) {
    touchedTop = true;
  } 
  if (currentScrollPos <= 100 && touchedTop) {
    document.getElementById("nav").style.position = "absolute";
    document.getElementById("nav").style.top = "0"; 
  } else {
    touchedTop = false;
    if (prevScrollpos > currentScrollPos) {
      document.getElementById("nav").style.top = "0"; 
      document.getElementById("nav").style.position = "fixed";
    } 
    else {
      document.getElementById("nav").style.top = "-100px";
      document.getElementById("nav").style.position = "absolute";
    }
  }
  prevScrollpos = currentScrollPos;
}

});
</script>
<style >
@import "/style.css";
</style>
