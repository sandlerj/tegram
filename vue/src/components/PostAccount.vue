<template>
    <div class="account-header"> 
        <figure class="image is-64x64">
            <img class="is-rounded" :src="!account.profileImg ? '/profilePlaceholder.png' : account.profileImg" :alt="account.username">
        </figure>
        <span class="account-username ml-3"><b>{{account.username}}</b></span>
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
                this.account.profileImg = res.data.profileImage;
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