<template>
  <div>
      <comment-list :postId="postId" :limit="numComments" />
      <add-comment :postId="postId" v-show="showMore"/>
      <p v-if="!showMore" class="toggle-comments" @click="showMoreComments()">Add/view more comments</p>
      <p v-else class="toggle-comments" @click="reduceComments()">Hide comments</p>
  </div>
</template>

<script>
import AddComment from './AddComment.vue'
import CommentList from './CommentList.vue'
export default {
  components: { CommentList, AddComment },
    name: "comments",
    props: {
        "postId" : Number
    },
    data() {
        return {
            numComments: 1,
            showMore: false
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
        }
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