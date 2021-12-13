import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token');
const currentUser = JSON.parse(localStorage.getItem('user'));
const currentAccountId = Number.parseInt(localStorage.getItem('accountId'));

if(currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    accountId: currentAccountId || undefined
  },
  mutations: {
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user',JSON.stringify(user));
    },
    SET_ACCOUNT(state, accountId) {
      state.accountId = accountId;
      // Number.toString returns a function; replaced with ES6 template literal to cast accountId to string
      localStorage.setItem('accountId', `${accountId}` );
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      localStorage.removeItem('accountId');
      state.token = '';
      state.user = {};
      state.accountId = undefined;
      axios.defaults.headers.common = {};
    }
  }
})
