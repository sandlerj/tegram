<template>
    <div class="account-header"> 
        <font-awesome-icon class="placeholder-icon" v-if="!account.profileImg" icon="user"/>           
        <img v-else :src="account.profileImg" :alt="account.username">
        <span class="account-username">{{account.username}}</span>
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
                console.log(`Something went wrong when accessing the server: ${err.message}`)
            })
        }
    },
    created() {
        this.loadAccount();
    }
}
</script>

<style>
.placeholder-icon {
    font-size: 2.5em;
}
</style>