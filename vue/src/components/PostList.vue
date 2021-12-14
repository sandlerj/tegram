<template>
  <div class="feed">
      <post-card v-for="post in postList" :post="post" :key="post.postId" :isFavorited="favoritedPostIds.has(post.postId)"
      v-on:postHasBeenClicked="(postId) => seePostDetails(postId)"/>
      <p v-show="!hasPosts">{{ noPostsMessage }}</p>
  </div>
</template>

<script>
import PostCard from './PostCard.vue'
import postService from '@/services/PostService.js';

export default {
  components: { PostCard },
    name: "post-list",
    data() {
        return {
            hasPosts: false,
            noPostsMessage: "There are no posts for this page.",
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
    watch:{
    $route (){
        this.getFavoritePostIds();
        this.updatePostList();
    }
    }, 
    methods: {
        seePostDetails(postId) {
            this.$router.push({ path: `posts/${postId}`});
        },
        getFavoritePostIds() {
            postService.getFavorites(this.$store.state.accountId)
            .then((res) => {
                this.favoritedPostIds = new Set(res.data.map(x => x.postId));
            })
            .catch((err) => {
                console.log("Issue retrieving favorites");
                console.log(err);
            })
        },
        updatePostList() {

            const updateState = (res, message = "") => {
                    this.postList = res.data;
                    if (res.data.length) {
                        this.hasPosts = true;
                    } else {
                        this.hasPosts = false;
                        this.noPostsMessage = message;
                    }
                    this.isLoading = false;
            }

            switch (this.$route.name) {
            case "home":
                postService.list()
                .then((res) => updateState(res))
                .catch(err => {
                    console.log(err.message)
                })
                break;
            case "favorites":
                postService.getFavorites(this.$store.state.accountId)
                .then((res) => updateState(res, "Add some posts to your favorites page and they will appear here!"))
                .catch(err => {
                    console.log(err.message)
                })
                break;
            case "posts":
                postService.listByAccountId(this.$store.state.accountId)
                .then((res) => updateState(res, "Upload a new post and it will appear here!"))
                .catch(err => {
                    console.log(err.message)
                })
                break;
            default:
                break;
        }
        }
    },
    created() {
        this.getFavoritePostIds();
        this.updatePostList();
    }
}
</script>

<style>

</style>