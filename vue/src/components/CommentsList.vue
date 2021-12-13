<template>
  <div>
      <comment :postId="postId" v-for="comment in commentList" :key="comment.commentId" :comment="comment" v-show="showMore"/>
      <add-comment :postId="postId" v-show="showMore" v-on:reloadComments="loadComments"/>
      <p v-if="!showMore" class="toggle-comments" @click="showMoreComments()">Add/view more comments</p>
      <p v-else class="toggle-comments" @click="reduceComments()">Hide comments</p>
  </div>
</template>

<script>
import AddComment from './AddComment.vue'
import Comment from './Comment.vue'
import CommentService from '@/services/CommentService.js'
export default {
  components: { Comment, AddComment },
    name: "comments-list",
    props: {
        "postId" : Number
    },
    data() {
        return {
            numComments: 1,
            showMore: false,
            commentList:[],
            commentsLoaded: false,
        }
    },
    methods: {
        showMoreComments() {
            this.showMore = true;
            this.numComments = -1;
        },
        reduceComments() {
            this.showMore = false;
            this.numComments = 1;
        },
        loadComments() {
            CommentService.getComments(this.postId)
            .then((res)=> {
                this.commentList = res.data;
                this.commentsLoaded = true;
            })
            .catch((err) => {
                console.log(err);
            })
        }
    },
    created() {
        this.loadComments();
    }
}
</script>

<style>
.toggle-comments {
    font-weight: bold;
    color: blue;
}
.toggle-comments:hover {
    text-decoration: underline;
}
</style>