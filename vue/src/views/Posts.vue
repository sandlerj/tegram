<template>
<div>
  <post-list  />
</div>
</template>

<script>

//import posts from accountservice?
import PostList from "@/components/PostList.vue"
export default {
    name: "Posts",
    components: {
        PostList,
    }

}
</script>

<style>
  
</style>