<template>
    <font-awesome-icon icon="star" 
    :style="{ color : favorited ? 'gold' : ''}" 
    @click.prevent="toggleFavorite" />
</template>

<script>
import PostService from "@/services/PostService.js"
export default {
    name: "favorite-button",
    props: {
        "postId": Number,
        "isFavorited": Boolean
    },
    data() {
        return {
            favorited: this.isFavorited,
        }
    },
    methods: {
       toggleFavorite() {
           if (this.favorited) {
               this.favorited = false;
                PostService.removeFavorite(this.postId, this.$store.accountId)
                .then(() => {
                    console.log("Post unfavorited")
                })
           } else {
               this.favorited = true;
               PostService.addFavorite(this.postId, this.$store.accountId)
               .then(()=> {
                   console.log("Post favorited")
               })
               .catch((err)=>{
                   console.log("An error occured in unfavoriting the post");
                   console.log(err);
               })
           }
       }
    }
}
</script>

<style>

</style>