<template>
    <p v-if="loading">Loading...</p>
    <form @submit.prevent="updateAccount" class="update-account-form" v-else>
        <label for="email">Email</label>
    <input
            type="text"
            id="email"
            class="input"
            placeholder=""
            v-model="updatedAccount.email"
            autofocus
        />
        <label for="profile-image">Profile Picture URL</label>
        <input
            type="text"
            id="profile-image"
            class="input"
            placeholder=""
            v-model="updatedAccount.profileImage"
        />
        <label for="bio">Bio</label>
        <textarea id="bio" v-model="updatedAccount.bio" placeholder="Share a little about yourself."></textarea>

        <button class="button is-info mt-4" type="submit">Update</button>
    </form>
</template>

<script>
import AccountService from '../services/AccountService';

export default {
    name: "update-account",
    data() {
        return {
            account: null,
            updatedAccount: {
                email: "",
                profileImage:  "",
                bio: ""
            },
            loading: false
        }
    },
    watch: {
        account () {
            this.updatedAccount.email = this.account.email;
            this.updatedAccount.profileImage = this.account.profileImage;
            this.updatedAccount.bio = this.account.bio;
        }
    },
    methods: {
        updateAccount() {
            AccountService.getAccount(this.$store.state.accountId).then(res => {
                this.account = res.data;
                this.account.email = this.updatedAccount.email || this.account.email;
                this.account.profileImage = this.updatedAccount.profileImage || this.account.profileImage;
                this.account.bio = this.updatedAccount.bio || this.account.bio;

                this.updatedAccount.email = this.account.email || "";
                this.updatedAccount.profileImage = this.account.profileImage || "";
                this.updatedAccount.bio = this.account.bio || "";

                this.loading = true;
                AccountService.updateAccount(this.account).then(() => {
                    this.loading = false;
                    this.$emit("accountUpdated");
                });
            })
        }
    },
    created() {
        AccountService.getAccount(this.$store.state.accountId).then(res => {
            this.account = res.data;
        })
    }

}
</script>

<style>
</style>