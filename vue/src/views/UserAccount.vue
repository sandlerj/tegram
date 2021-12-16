<template>
<div>
  <account-details :accountId="accountId" />
  <post-list  :feedAccountId="accountId" />
</div>
</template>

<script>

//import posts from accountservice?
import PostList from "@/components/PostList.vue"
import AccountDetails from "@/components/AccountDetails.vue"

export default {
    name: "user-account",
    components: {
        PostList,
        AccountDetails
    },
    computed: {
        accountId() {
            let result = this.$route.query.accountId;
            if (!result || Number.isNaN(Number.parseInt(result))) {
                return this.$store.state.accountId;
            }
            return Number.parseInt(result);
        }
    },

}
</script>

<style>
  
</style>