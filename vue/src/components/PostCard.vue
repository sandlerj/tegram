<template>
    <div>
        <div class="post-img-header">
            <post-account :accountId="{{post.accountId}}"/>
            <ellipsis-button :postId="{{post.postId}}"/>
            <favorite-button :postId="{{post.postId}}"/>
        </div>
        <div class="post-img-div">
            <img src="{{ post.mediaLink }}">
            <p class="img-div">{{ post.caption }}</p>
        </div>
        <comments :postId="{{ post.postId }}"/>
    </div>
</template>

<script>
import Comments from './Comments.vue'
import EllipsisButton from './EllipsisButton.vue'
import FavoriteButton from './FavoriteButton.vue'
import PostAccount from './PostAccount.vue'

export default {
    name: "post-card",
    components: {
        Comments,
        EllipsisButton,
        FavoriteButton,
        PostAccount
        },
        props: ["post"],
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