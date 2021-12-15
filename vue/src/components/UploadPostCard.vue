<template>
<div class="columns">
    <div class="post-upload-form container box">
        <div class="column has-text-centered is-size-1">
        <h2>Upload Post</h2>
        </div>

         
        <div class="column has-text-centered">
        <label class="file is-centered" for="uploadImg">
            <input type="file" name="uploadImg" v-on:change="handleFileUpload($event)"/>
        </label>
        </div>
        
        <div class="column has-text-centered ">
            
        <label for="Caption">
            <input type="text" name="Caption" class="caption-input is-centered" id="Caption" placeholder="Add a caption..." v-model="post.caption"/>
            </label>
        </div>

        <div class="column has-text-centered">
        <button class="button is-success is-normal" @click.prevent="submitFile()">Post Photo!</button>
        </div>
        <div class="loading-div" v-if="isUploading">
            <img src="/loading.gif">
            <p>Uploading...</p>
        </div>
        </div>
    </div>
</template>

<script>
import PostService from "@/services/PostService.js"
export default {
    name: "upload-post-card",
    data() {
        return {
            post : {
                accountId: this.$store.state.accountId,
                MediaLink: "",
                caption: "",
                uploadImg: "",
            },
            isUploading: false
        }
    },
    methods: {
        handleFileUpload(event) {
            this.post.uploadImg = event.target.files[0];
        },
        submitFile(){
            let formData = new FormData();
            formData.append("uploadImg", this.post.uploadImg);
            formData.append("AccountId", this.post.accountId);
            formData.append("Caption", this.post.caption);
            formData.append("Timestamp", new Date().toJSON());
            
            this.isUploading = true;

            PostService.create(formData)
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