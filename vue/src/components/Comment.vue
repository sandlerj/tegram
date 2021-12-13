<template>
    <div class="comment">
        <b>{{comment.username}}</b> {{comment.text}}
    </div>
</template>

<script>
export default {
    name: "comment",
    props: {
        "postId": Number,
        "limit": {
            type: Number,
            default: -1 //all
        },
        "comment": {
            accountId: Number,
            commentId: Number,
            postId: Number,
            timestamp: String,
            text: String,
            usrname: String
        }
    },
}
</script>

<style>

</style>