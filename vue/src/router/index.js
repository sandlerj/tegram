import Vue from 'vue'
import Router from 'vue-router'
//import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Logout from '../views/Logout.vue'
import NewAccount from '../views/NewAccount.vue'
import store from '../store/index'
import Posts from '../views/Posts.vue'
import PostDetail from '../views/PostDetail.vue'
import Favorites from '../views/Favorites.vue'
import UploadPost from '../views/UploadPost.vue'
import PostCard from '@/components/PostCard.vue'
import EditPost from '../views/EditPost.vue'
import UserAccount from "@/views/UserAccount.vue"


Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Posts,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/register",
      name: "register",
      component: NewAccount,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: '/posts',
      name: 'posts',
      component: Posts,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/posts/:postId',
      name: 'postDetail',
      component: PostDetail,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/favorites',
      name: 'favorites',
      component: Favorites,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/upload',
      name: 'upload',
      component: UploadPost,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/update',
      name: 'update',
      component: EditPost,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/update/:postId',
      name: 'update',
      component: EditPost,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/test',
      name: 'test',
      component: PostCard
    },
    {
      path: '/account',
      name: 'account',
      component: UserAccount
    }

  ]
})

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    next("/login");
  } else {
    // Else let them go to their next destination
    next();
  }
});

export default router;
