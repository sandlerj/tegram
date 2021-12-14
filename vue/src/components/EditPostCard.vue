<template>
<div class="post-upload-form container box">
        <h2>Edit Post</h2>
        <img v-bind:src="editedPost.mediaLink">
        <label for="MediaLink">MediaLink<input type="text" name="MediaLink" class="mediaLink-input" v-model="editedPost.mediaLink" /></label>
        <label for="Caption">Caption<input type="text" name="Caption" class="caption-input" v-model="editedPost.caption"/></label>
        <button @click.prevent="updatePost()">Update Post!</button>
        <div class="loading-div" v-if="isUploading">
            <img src="/loading.gif">
            <p>Uploading...</p>
        </div>
        </div>
        
</template>

<script>
import PostService from "@/services/PostService.js"
export default {
    name: "edit-post-card",
    props: ["post"],
    
    
    data(){
      return {
        editedPost: {
          postId: this.post.postId,
          caption: this.post.caption,
           mediaLink: this.post.mediaLink,
           timestamp: this.post.timestamp,
           accountId: this.post.accountId,
        },
        isUploading: false
      }
    },
    watch: {
      post () {
        this.editedPost = this.post;
      }
    },
    methods: {
      updatePost(){
        this.isUploading = true,
        PostService.update(this.editedPost) 
        .then(()=> {
                this.isUploading = false;
                this.$router.push("/")
            })
            .catch((error)=> {
                console.log(error.message)
                alert("Something Went Wrong. Please Try Again Later.");
                this.isUploading = false;
                });
      }
    }
}
</script>

<style>

</style>