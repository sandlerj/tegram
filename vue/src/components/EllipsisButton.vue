<template>
   <!-- <div id="ellipsis-button" @click="hideContextMenu()" @contextmenu.prevent="showContextMenu($event)"> -->
<div id="ellipsis-button" >
<edit-post-card v-show="isModalVisable = true" />
        <font-awesome-icon icon="ellipsis-h" @click="toggleMenu($event)" v-click-outside="hideContextMenu"/>
        <ki-context 
            ref="kiContext"
            minWidth='1em'
            maxWidth='15em'
            backgroundColor='#fbfbfb'
            fontSize='15px'
            textColor='#35495e'
            iconColor='#41b883'
            borderRadius='0.1'
        />
        
    </div>
</template>

<script>
import postService from '@/services/PostService.js';
import ClickOutside from 'vue-click-outside'
import EditPostCard from './EditPostCard.vue';



export default {
    name: "ellipsis-button",
    props: {
        "postId": Number
    },
    components: {
        EditPostCard
    },
    data() {
        return{
            isContextVisible: false,
            isModalVisable: false,
            items: [
                { name: "Edit" },
                { name: "Delete" }
            ]
        }
    },
    methods: {
        toggleMenu(event) {
            if (!this.isContextVisible){
                this.isContextVisible = true;
                this.showContextMenu(event)
            } else {
                this.hideContextMenu()
            }
        },
        showContextMenu (event){
      let items = [
        {
          icon: "arrow-up",
          text: 'Edit',
          click: () => {
            if (confirm('Are you sure you want to update this post?')) {
              this.isModalVisable = true;
            }
            


          }
        },
        {
          icon: 'arrow-right',
          text: 'Delete',
          divider: true,
          click: () => {
            if (confirm('Are you sure you want to delete this post?')) {
                postService.delete(this.postId)
                .then((res)=>{
                    console.log(res)
                    alert('This post has been deleted.')
                    // reload feed
                    this.$router.push({name: "posts"});
                })
                .catch((err) => {
                    console.log(err)
                    alert('Something went wrong, try again later.')
                })
            }
          }
        }
      ];
      this.$refs.kiContext.show(event, items);
    },
    hideContextMenu (){
      this.$refs.kiContext.hide();
      this.isContextVisible = false
    }
    },
    directives: {
    ClickOutside
  }
}
</script>

<style>

</style>