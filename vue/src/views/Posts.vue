<template>
<div>
  <account-details v-show="this.$route.name == 'posts'"/>
  <post-list  />
</div>
</template>

<script>

//import posts from accountservice?
import PostList from "@/components/PostList.vue"
import AccountDetails from "@/components/AccountDetails.vue"
export default {
    name: "Posts",
    components: {
        PostList,
        AccountDetails
    }

}
</script>

<style>

</style>