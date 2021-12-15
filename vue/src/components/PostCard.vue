<template>
    <div class="tegram-card post-card">
        <div class="post-img-header level is-mobile">
            <post-account class="level-left" :accountId="post.accountId"/>
            <div class="level-right">
                <!-- <ellipsis-button class="mr-3" @ellipsisClicked="contextToggle"  v-bind:post="post" v-show="$store.state.accountId == post.accountId" :postId="post.postId"/> -->
                <font-awesome-icon class="mr-3" icon="ellipsis-h" @click="contextToggle" />
                <favorite-button class="" :postId="post.postId" :isFavorited="isFavorited" />
            </div>
        </div>
        <div class="post-img-div has-text-centered">
            <div class="container">
                <img class="post-img" :src="post.mediaLink" v-on:click="notifyParent">
            </div>
            <p class="img-div">{{ post.caption }}</p>
        </div>
        <div>
            <likes :postId="post.postId" />
            <comments-list :postId="post.postId"/>
        </div>

        <div :class="{ modal: true,  'is-active': showContext}">
            <div @click.prevent="hideContext" class="modal-background"></div>
            <div class="modal-card">
                <header class="modal-card-head">
                    <p class="modal-card-title">Edit Post</p>
                </header>
                <section class="modal-card-body">
                    <edit-post-card :post="post" />
                </section>
                <footer class="modal-card-foot">
                    <button @click.prevent="deletePost" class="button is-danger">Delete Post</button>
                    <button @click.prevent="hideContext" class="button close">Cancel</button>
                </footer>
            </div>
        </div>
    </div>
</template>

<script>
import CommentsList from './CommentsList.vue'
// import EllipsisButton from './EllipsisButton.vue'
import FavoriteButton from './FavoriteButton.vue'
import Likes from './Likes.vue'
import PostAccount from './PostAccount.vue'
import EditPostCard from './EditPostCard.vue'
import PostService from "@/services/PostService.js"

export default {
    name: "post-card",
    components: {
        CommentsList,
        // EllipsisButton,
        FavoriteButton,
        PostAccount,
        Likes,
        EditPostCard,
        },
        props: ["post", "isFavorited"],
    data() {
        return {
            showContext: false,
        }
    },
    methods: {
        deletePost(){
            if (confirm('Are you sure you want to delete this post?')) {
                PostService.delete(this.post.postId)
                .then((res)=>{
                    console.log(res)
                    alert('This post has been deleted.')
                    // reload feed
                    this.$router.go();
                })
                .catch((err) => {
                    console.log(err)
                    alert('Something went wrong, try again later.')
                })
                // do some loading anim bs
            }
        },
        contextToggle(){
            this.showContext = !this.showContext;
        },
        hideContext() {
            this.showContext = false;
        },
        notifyParent() {
            this.$emit('postHasBeenClicked', this._props.post.postId);
        }
    }
}
</script>

<style>
/*probably going to want to link to an external style sheet for consistency sake*/
</style>