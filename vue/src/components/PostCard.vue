<template>
    <div class="tegram-card post-card">
        <div class="post-img-header level is-mobile">
            <post-account class="level-left" :accountId="post.accountId"/>
            <div class="level-right">
                <ellipsis-button class="mr-3"  v-bind:post="post" v-show="$store.state.accountId == post.accountId" :postId="post.postId"/>
                <favorite-button class="" :postId="post.postId" :isFavorited="isFavorited" />
            </div>
        </div>
        <div class="post-img-div has-text-centered">
            <img :src="post.mediaLink" v-on:click="notifyParent">
            <p class="img-div">{{ post.caption }}</p>
        </div>
        <likes :postId="post.postId" />
        <comments-list :postId="post.postId"/>
    </div>
</template>

<script>
import CommentsList from './CommentsList.vue'
import EllipsisButton from './EllipsisButton.vue'
import FavoriteButton from './FavoriteButton.vue'
import Likes from './Likes.vue'
import PostAccount from './PostAccount.vue'

export default {
    name: "post-card",
    components: {
        CommentsList,
        EllipsisButton,
        FavoriteButton,
        PostAccount,
        Likes
        },
        props: ["post", "isFavorited"],
    data() {
        return {

        }
    },
    methods: {
        notifyParent() {
            this.$emit('postHasBeenClicked', this._props.post.postId);
        }
    }
}
</script>

<style>
/*probably going to want to link to an external style sheet for consistency sake*/
</style>