<template>
  <div class="card account-details">
      <div class="card-header">
          <div class="card-header-title">
                <div>
                    <p>{{ account.username }} <span v-if="account.email" class="has-text-weight-light" > | {{ account.email }}</span><span v-else><button @click.prevent="contextToggle" class="button is-ghost">add an email</button></span></p>
                    
                    <p class="has-text-weight-normal is-size-5" >Member since {{ new Date(account.memberSince).toLocaleString('default', { month: 'long', year: 'numeric' }) }}</p>
                </div>
          </div>
          <div class="image is-128x128 m-4">
              <img :src="account.profileImage" class="avatar"/>
              </div>
      </div>
      <div class="card-content">
          <p> {{account.bio}} </p>
      </div>
      <div class="card-footer">
            <div class="level m-4" style="width: 100%">
                <p class="level-item has-text-centered">Total Posts: {{ accountDetails.numberOfPosts}}</p>
                <p class="level-item has-text-centered">Likes Received: {{accountDetails.numberOfLikesReceived}}</p>
                <p class="level-item has-text-centered">Likes Given: {{accountDetails.numberOfLikesGiven}}</p>
            </div>
      </div>
            <button class="button is-ghost" @click.prevent="contextToggle">Edit Account</button>

        <div :class="{ modal: true,  'is-active': showContext}">
            <div @click.prevent="hideContext" class="modal-background"></div>
            <div class="modal-card">
                <header class="modal-card-head">
                    <p class="modal-card-title">Edit Post</p>
                    <font-awesome-icon icon="times" @click="hideContext"/>
                </header>
                <section class="modal-card-body">
                    <update-account v-on:accountUpdated="accountHasBeenUpdated"/>
                </section>
            </div>
        </div>

  </div>
</template>

<script>
    import AccountService from '../services/AccountService';
    //import Modal from '../components/Modal.vue';
    import UpdateAccount from '../components/UpdateAccount.vue';

export default {
    name: "account-details",
    components: { UpdateAccount },
    data() {
        return {
            account: null,
            accountDetails: null,
            callModal: 0,
            showContext: false
        }
    },
    methods: {
        contextToggle(){
            this.showContext = !this.showContext;
        },
        hideContext() {
            this.showContext = false;
        },
        getAccount() {
            console.log('got account');
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
           this.hideContext();
            this.getAccount();
        }
    },
    created() {
        this.getAccount();
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