<template>
    <div>
        <message v-show="!hasPosts" :message="noPostsMessage.text" :header="noPostsMessage.header">
            <div v-if="noPostsMessage.header == 'No Posts'">
                <p>{{ noPostsMessage.text }}</p>
                <button class="button is-info mt-3" @click.prevent="$router.push('/upload')">Upload Post</button>
            </div>
            <div v-else-if="noPostsMessage.header == 'No Favorites'">
                <p>{{ noPostsMessage.text }}</p>
                <button class="button is-info mt-3" @click.prevent="$router.push('/')">Find Favorites</button>
            </div>
        </message>
        <div class="feed">
            <post-card v-for="post in postList" :post="post" :key="post.postId" :isFavorited="favoritedPostIds.has(post.postId)"
            v-on:postHasBeenClicked="(postId) => seePostDetails(postId)"/>
            <!-- <p v-show="!hasPosts">{{ noPostsMessage }}</p> -->
        </div>
    </div>
</template>

<script>
import PostCard from './PostCard.vue'
import postService from '@/services/PostService.js';
import Message from './Message.vue';

export default {
  components: { PostCard, Message },
    name: "post-list",
    data() {
        return {
            hasPosts: false,
            noPostsMessage: {header: "", text: "There are no posts for this page."},
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

            const updateState = (res, header = "", message = "") => {
                    this.postList = res.data;
                    if (res.data.length) {
                        this.hasPosts = true;
                    } else {
                        this.hasPosts = false;
                        this.noPostsMessage.text = message;
                        this.noPostsMessage.header = header;
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
                .then((res) => updateState(res, "No Favorites", "You don't have any posts in your favorites collection. Go the home page to see what others are sharing and click the star on a post to add it to your favorites."))
                .catch(err => {
                    console.log(err.message)
                })
                break;
            case "posts":
                postService.listByAccountId(this.$store.state.accountId)
                .then((res) => updateState(res, "No Posts", "Welcome to TEGram! Share your pictures with the world by uploading a post."))
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