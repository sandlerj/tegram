<template>
    <div class="post-upload-form">
        <h2>Upload Post</h2>
        <label for="uploadImg">Select Photo <input type="file" name="uploadImg" v-on:change="handleFileUpload($event)"/></label>
        <label for="Caption">Caption<input type="text" name="Caption" class="caption-input" v-model="post.caption"/></label>
        <button @click.prevent="submitFile()">Post Photo!</button>
        <div class="loading-div" v-if="isUploading">
            <img src="/loading.gif">
            <p>Uploading...</p>
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
                accountId: this.$store.accountId,
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