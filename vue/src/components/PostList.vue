<template>
  <div>
      <post-card v-for="post in postList" :post="post" :key="post.postId" />
  </div>
</template>

<script>
import PostCard from './PostCard.vue'

import postService from '@/services/PostService.js';
import accountService from '@/services/AccountService.js';

export default {
  components: { PostCard },
    name: "post-list",
    data() {
        return {
            postList: [],
            isLoading: true
        }
    },
    props: {
        "accountId" : {
            type: Number,
            default: undefined
        },
    },
    computed: {
    },
    created() {
        // i don't have these services, will write once i have them
            // if accountId undefined, return all (feed)
            if (this.accountId == undefined) {
                //return feed
                postService.list()
                .then((res) => {
                    // check if okay, then
                    this.postList = res.data // i think??
                    this.isLoading = false;
                })
                .catch(err => {

                })
            } else if (this.accountId <= 0) {
            // else if accountId <= 0, GET favorites for user
                postService.getFavorites(this.accountId)
                .then((res) => {
                    this.postList = res.data;
                    this.isLoading = false;
                })
                .catch(err => {

                })
            } else {
                // else, accountId return just posts for that account OR emptylist if 404 from GET
                accountService.viewPosts(this.accountId)
                .then((res) => {
                    this.postList = res.data;
                    this.isLoading = false;
                })
                .catch(err => {

                })
            }
    }
}
</script>

<style>

</style>