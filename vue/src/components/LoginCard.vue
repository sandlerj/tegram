<template>
<div class="columns is-centered is-vcentered">
      <div class="column is-one-quarter-desktop">
        <div class="column has-text-centered">
          <div class="column is-center">
    <div id="login" class="column container box">
        <form class="form-signin" @submit.prevent="login">
          <h2 class="text is-size-3 has-text-centered">Welcome to TEgram</h2>
      <h1 class="text h3 mb-3  is-size-5 has-text-centered">Please Sign In</h1>
      
      <div
        class="alert alert-danger"
        role="alert"
        v-if="invalidCredentials"
      >Invalid username and password!</div>
      
  

      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >Thank you for registering, please sign in.</div>
      <div class="field">
      <label  class="sr-only is-ancestor">Username</label>
      <div class="control">
      <input
        type="text"
        id="username"
        class="input"
        placeholder="Username"
        v-model="user.username"
        required
        autofocus
      />
      </div>
      </div>
      
      
      <div class="field">
      <label for="password" class="sr-only is-parent is-vertical">Password</label>
      <div class="control">
      <input
        type="password"
        id="password"
        class="input"
        placeholder="Password"
        v-model="user.password"
        required
      />
      </div>
      </div>

      <div class="has-text-centered">
      <div class="mt-6 has-text-centered">
        <button class="button is-white">
      <router-link :to="{ name: 'register' }">Sign Up</router-link>
      </button>
       </div>
       </div>

       <div class="has-text-centered">
      <button class="button is-success has-button-centered" type="submit">Log In</button>
       </div>
      
        </form>
        
</div>
  </div>
    </div>
      </div>
        </div>
      
</template>

<script>
import authService from "../services/AuthService";

export default {
    name: "login-card",
    data() {
        return {
            user: {
                username: "",
                password: ""
            },
            invalidCredentials: false
        };
    },
    methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            // Maybe add additional mutation for storing accountId
            this.$store.commit("SET_ACCOUNT", response.data.accountId)
            this.$router.push("/");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    }
  }
};
</script>

<style>

</style>