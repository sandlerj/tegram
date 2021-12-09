<template>
  <div>
      <post-card v-for="post in postList" :post="post" :key="post.postId" />
  </div>
</template>

<script>
import PostCard from './PostCard.vue'
export default {
  components: { PostCard },
    name: "post-list",
    data() {

    },
    props: {
        "accountId" : {
            type: Number,
            default: undefined
        },
    },
    computed: {
        postList() {
            //if accountId undefined, return all (feed)

            // else if accountId <= 0, GET favorites for user
            
            //else,  accountId return just posts for that account OR emptylist if 404 from GET
        }
    }
}
</script>

<style>

</style>