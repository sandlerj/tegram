<template>
  <div class="card account-details">
      <div class="card-header">
      <modal :toggle="callModal" title="Edit Account">
        <update-account v-on:accountUpdated="accountHasBeenUpdated"/>
      </modal>
          <div class="card-header-title">{{ account.username }} | {{ account.email }}</div>
          <div class="image is-128x128 m-4">
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
        <button class="button is-ghost" @click.prevent="++callModal">Edit Account</button>
      </div>
  </div>
</template>

<script>
    import AccountService from '../services/AccountService';
    import Modal from '../components/Modal.vue';
    import UpdateAccount from '../components/UpdateAccount.vue';

export default {
    name: "account-details",
    components: { Modal, UpdateAccount },
    data() {
        return {
            account: null,
            accountDetails: null,
            callModal: 0
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
        },
        accountHasBeenUpdated() {
            ++this.callModal;
            this.getAccount();
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