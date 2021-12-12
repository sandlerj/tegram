<template>
    <div class="add-comment">
        <input v-model="text" placeholder="What say you?">
        <button @click.prevent="submitComment()">Say it!</button>
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

</style>