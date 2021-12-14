<template>
<edit-post-card :post="post"/>
</template>

<script>
import EditPostCard from '@/components/EditPostCard.vue'
import Postservice from '../services/PostService.js'
export default {
    name: "EditPost",
    components: {
        EditPostCard
    },
    data() {
        return {
        post: {
          postId: 0,
          caption: "test",
           mediaLink: "test",
           timestamp: "1231",
           accountId: 0,
        },
        }
        },
    methods: {
        
        GetPost() {
            Postservice.get(this.$route.params.postId).then(res => {
                this.post = res.data;
            })
            
        }
        
    },
    created() {
        this.GetPost()
    }
}
</script>

<style>

</style>