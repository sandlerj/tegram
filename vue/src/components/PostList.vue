<template>
  <div>
      <post-card v-for="post in postList" :post="post" :key="post.postId" :isFavorited="favoritedPostIds.has(post.postId)"/>
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
            isLoading: true,
            favoritedPostIds: new Set(),
        }
    },
    props: {
        "feedAccountId" : {
            type: Number,
            default: undefined
        },
    },
    computed: {
        
    },
    methods: {
        getFavoritePostIds() {
            postService.getFavorites(this.$store.state.accountId)
            .then((res) => {
                this.favoritedPostIds = new Set(res.data)
            })
            .catch((err) => {
                console.log("Issue retrieving favorites");
                console.log(err);
            })
        }
    },
    created() {
        this.getFavoritePostIds();
        if (this.feedAccountId == undefined) {
            //return feed
            postService.list()
            .then((res) => {
                // check if okay, then
                this.postList = res.data // i think??
                this.isLoading = false;
            })
            .catch(err => {
                console.log(err.message)
            })
        } else if (this.feedAccountId <= 0) {
            console.log('yeah???')
        // else if accountId <= 0, GET favorites for user
            postService.getFavorites(this.feedAccountId)
            .then((res) => {
                this.postList = res.data;
                this.isLoading = false;
            })
            .catch(err => {
                console.log(err.message)
            })
        } else {
            // else, accountId return just posts for that account OR emptylist if 404 from GET
            accountService.viewPosts(this.feedAccountId)
            .then((res) => {
                this.postList = res.data;
                this.isLoading = false;
            })
            .catch(err => {
                console.log(err);
            })
        }
    }
}
</script>

<style>

</style>