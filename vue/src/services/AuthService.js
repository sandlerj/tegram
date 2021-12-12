import axios from 'axios';

export default {

  login(user) {
    return axios.post('/login', user)//needs accountId
  },

  register(user) {
    return axios.post('/register', user)
  }

}
