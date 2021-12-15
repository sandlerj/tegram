<template>
  <div class="favorites-list">
      <post-list :feedAccountId="$store.accountId"/>
      </div>
</template>

<script>
import PostList from "@/components/PostList.vue"
export default {
    name: "favorite",
    components: {
        PostList
    }

}
</script>

<style>

</style>