import Vue from 'vue'
import App from './App.vue'
import router from './router/index'
import store from './store/index'
import axios from 'axios'


//ki context

import kiContext from '@kiyoaki_w/vue-context'
Vue.use(kiContext)
import { faArrowRight, faArrowLeft, faArrowUp, faArrowDown } from '@fortawesome/free-solid-svg-icons'
library.add(faArrowRight, faArrowLeft, faArrowUp, faArrowDown)

//

//Font Awesome config
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faEllipsisH, faStar, faHeart } from '@fortawesome/free-solid-svg-icons'

library.add(faEllipsisH, faStar, faHeart)

Vue.component('font-awesome-icon', FontAwesomeIcon)
// Up to here




Vue.config.productionTip = false

axios.defaults.baseURL = process.env.VUE_APP_REMOTE_API;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
