<template>
  <div class="card account-details">
      <div class="card-header">
          <div class="card-header-title">{{ account.username }} | {{ account.email }}</div>
          <div class="card-header-icon">
              <img :src="account.profileImage" class="avatar"/>
              </div>
      </div>
      <div class="card-content">
          <div class="level">
          <p class="level-item has-text-centered">Total Posts: {{ accountDetails.numberOfPosts}}</p>
          <p class="level-item has-text-centered">Likes Received: {{accountDetails.numberOfLikesReceived}}</p>
          <p class="level-item has-text-centered">Likes Given: {{accountDetails.numberOfLikesGiven}}</p>
          </div>
      </div>
      <div class="card-footer">
        <a href="#" class="card-footer-item">Edit Account</a>
      </div>
  </div>
</template>

<script>
    import AccountService from '../services/AccountService';
export default {
    name: "account-details",
    data() {
        return {
            account: null,
            accountDetails: null
        }
    },
    methods: {
        getAccount() {
            AccountService.getAccount(this.$store.state.accountId).then(res => {
                this.account = res.data;
            })
        },
        getAccountDetails() {
            AccountService.getAccountDetails(this.$store.state.accountId).then(res => {
                this.accountDetails = res.data;
            })
        }
    },
    created() {
        this.getAccount();
        this.getAccountDetails();
    },
    updated() {
        this.getAccountDetails();
    }
}
</script>

<style>
.avatar {
    width: 50px;
    height: 50px;
    border-radius: 50%;
}

.account-details {
    margin: 2rem;
}
</style>