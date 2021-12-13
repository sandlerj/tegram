<template>
    <p class="comment mt-2">
        <b>{{comment.username}}</b> {{comment.text}}
    </p>
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