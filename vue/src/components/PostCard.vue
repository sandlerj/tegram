<template>
    <div>
        <div class="post-img-header">
            <post-account :accountId="post.accountId"/>
            <ellipsis-button v-show="$store.state.accountId == post.accountId" :postId="post.postId"/>
            <favorite-button :postId="post.postId" :isFavorited="isFavorited" />
        </div>
        <img :src="post.mediaLink">
        <div class="post-img-div">
            <p class="img-div">{{ post.caption }}</p>
            <likes :postId="post.postId" />
        </div>
        <comments :postId="post.postId"/>
    </div>
</template>

<script>
import Comments from './Comments.vue'
import EllipsisButton from './EllipsisButton.vue'
import FavoriteButton from './FavoriteButton.vue'
import Likes from './Likes.vue'
import PostAccount from './PostAccount.vue'

export default {
    name: "post-card",
    components: {
        Comments,
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

    }
}
</script>

<style>
/*probably going to want to link to an external style sheet for consistency sake*/

</style>