<template>
    <div class="account-header">            
        <img src="{{account.profileImg}}" alt="{{account.username}}'s profile image">
        <div class="account-username"></div>
    </div>
</template>

<script>
import AccountService from "@/services/AccountService.js";
export default {
    name: "post-account",
    props: {
        "accountId": Number
    },
    data() {
        return{
            account: {
                accountId: this.accountId,
                userId: 0,
                profileImg: "", // get placeholder gif to use while loading
                username: "",
            },
            isLoading: true,
        }
    },
    methods: {
        loadAccount() {
            AccountService.getAccount(this.accountId)
            .then(res =>{
                this.account.userId = res.data.userId;
                this.account.profileImg = res.data.ProfileImage;
                this.account.username = res.data.username
                this.isLoading = false;
            })
            .catch(err => {
                // maybe display some sort of info indicating an error?
                // possible load some placeholder username
            })
        }
    }
}
</script>

<style>

</style>