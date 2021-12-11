<template>
    <div class="comment-list">
        <p v-for="comment in commentList" :key="comment.CommentId"><b>{{comment.AccountId}}</b> {{comment.Text}}</p>
        <p v-if="!commentsLoaded">Comments loading...</p>
    </div>
</template>

<script>
import CommentService from '@/services/CommentService.js'
export default {
    name: "comment-list",
    props: {
        "postId": Number,
        "limit": {
            type: Number,
            default: -1 //all
        }
    },
    data() {
        return {
            commentList:[],
            commentsLoaded: false,
        }
    },
    methods: {
        loadComments() {
            CommentService.getComments(this.postId)
            .then((res)=> {
                this.commentList = res.data;
                this.commentsLoaded = true;
            })
            .catch((err) => {
                alert('Something went wrong');
                console.log(err);
            })
        },
    },
    created() {
        this.loadComments();
    }
}
</script>

<style>

</style>