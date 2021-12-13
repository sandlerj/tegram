<template>
  <post-card :post="post" :key="postId" :isFavorited="favoritedPostIds.has(postId)"/>
</template>

<script>
import postService from '@/services/PostService.js';
import PostCard from './PostCard.vue'

export default {
  components: { PostCard },
    name: "single-post",
    data() {
        return {
            postId: this.$route.params.postId,
            post: null,
            favoritedPostIds: new Set()
        }
    },
    methods: {
        getFavoritePostIds() {
            postService.getFavorites(this.$store.state.accountId)
            .then((res) => {
                this.favoritedPostIds = new Set(res.data.map(x => x.postId));
            })
            .catch((err) => {
                console.log("Issue retrieving favorites");
                console.log(err);
            })
        }
    },
    created() {
        postService.get(this.postId).then((res) => {
            this.post = res.data;
        });
        this.getFavoritePostIds();
    }
}
</script>

<style>

</style>