<template>
<section class="section">
  <div class="container">
    <div class="columns">
   <div id="register" class="column text-centered container box is-one-quarter-desktop">
    <form class="form-register" @submit.prevent="register">
      <h1 class="text h3 mb-3 font-weight-normal has-text-centered">Please Sign Up</h1>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>

      <div class="field mt-6 has-text-centered">
      <label for="username" class="sr-only">Username</label>
      <div class="control">
      <input
        type="text"
        id="username"
        class="form-control"
        placeholder="Username"
        v-model="user.username"
        required
        autofocus
      />
      </div>
      </div>

      <div class="field mt-6 has-text-centered">
      <label for="password" class="sr-only">Password</label>
      <div class="control">
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      />
      </div>
      </div>

      <div class="field mt-6 has-text-centered">
        <label for="password" class="sr-only">Confirm Password</label>
         <div class="control">
      <input
        type="password"
        id="confirmPassword"
        class="form-control"
        placeholder="Confirm Password"
        v-model="user.confirmPassword"
        required
      />
      </div>
      </div>

      <div class="has-text-centered">
      <label class="checkbox">
        <input type="checkbox"> I'm not a Robot</label>
        </div> 
      <div class="has-text-centered">  
      <button class="button is-success btn btn-lg btn-primary btn-block" type="submit">
        Create Account
      </button>
      </div>
      


      <div class="has-text-centered">
        <label>Already Have an Account?</label>
        </div>
        <div class="has-text-centered">
    <button class="button has-button-centered"><router-link :to="{ name: 'login' }">Sign In</router-link></button>
      </div>
      
    </form>
  </div>
  </div>
  </div>
  
  </section>
</template>

<script>
import authService from "@/services/AuthService.js"
export default {
    name: "create-account-card",
     data() {
    return {
      user: {
        username: '',
        password: '',
        confirmPassword: '',
        role: 'user',
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: '/login',
                query: { registration: 'success' },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
};
</script>

<style>

</style>