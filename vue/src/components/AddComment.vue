<template>
    <div class="add-comment">
        <textarea v-model="text" placeholder="What say you?" class="mt-3"></textarea>
        <button @click.prevent="submitComment()" class="button is-info mt-3">Say it!</button>
    </div>
</template>

<script>
import CommentService from '@/services/CommentService.js'
export default {
    name: "add-comment",
    props: {
        "postId":Number,
        "showAdd":Boolean
    },
    data(){
        return{
            text: ""
        }
    },
    methods: {
        submitComment() {
            if (this.text == "") {
                return; // no empty comments 
            }
            let comment = {
                AccountId : this.$store.state.accountId,
                PostId : this.postId,
                Timestamp : new Date().toJSON(),
                Text : this.text 
            }
            CommentService.createComment(comment)
            .then(() => {
                this.text = "";
                this.$emit("reloadComments");
            })
            .catch((err) => {
                alert("Something went wrong, try again later.");
                console.log(err)
            })

        }
    }
}
</script>

<style>
textarea {
    padding: 0.3rem;
    display: block;
    width: 100%;
}
button {
    display: block;
}
</style>