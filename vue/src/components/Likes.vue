<template>
    <div class="likes mb-3">
        <font-awesome-icon icon="heart" 
        :style="{ color : isLiked ? 'hotpink' : ''}" 
        @click.prevent="toggleLike"
        />
        &nbsp;
        <span class="like-text">
            <span class="number-of-likes" v-show="isLoaded">{{this.numberOfLikes}}</span> {{numberOfLikes == 1 ? "like" : "likes"}}
        </span>
    </div>
</template>

<script>
import PostService from "@/services/PostService.js";

export default {
    name: "likes",
    props: ["postId"],
    methods: {
        getAccounts() {
            PostService.getAllLikes(this.postId)
            .then(res => {
                this.accounts = new Set(res.data);
                this.isLiked = this.accounts.has(this.$store.state.accountId);
                this.isLoaded = true
            })
            .catch(err => {
                console.log(`An error occured while retrieving post data: ${err.message}`)
            })
        },
        toggleLike() {
            if (!this.isLoaded) return;

            if (!this.isLiked) {
                this.isLiked = true;
                // make post req
                PostService.likePost(this.postId, this.$store.state.accountId)
                .then((res)=>{
                    this.accounts = new Set(res.data);
                })
                .catch((err)=>{
                    console.log(`Something went wrong when accessing the server: ${err.message}`)
                })
            } else {
                this.isLiked = false;
                //make delete request
                PostService.unlikePost(this.postId, this.$store.state.accountId)
                .then((res)=>{
                    this.accounts = new Set(res.data);
                })
                .catch((err)=>{
                    console.log(`Something went wrong when accessing the server: ${err.message}`)
                })
            }
        },
        
    },
    data() {
        return {
            accounts: new Set(),
            isLiked: false,
            isLoaded: false,
        }
    },
    computed: {
        numberOfLikes() {
            return this.accounts.size;
        }
    },
    created() {
        this.getAccounts();
    }
}
</script>

<style>
.like-text {
    color: hotpink
}
</style>