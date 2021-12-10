<template>
    <div id="ellipsis-button">
        <font-awesome-icon icon="ellipsis-h" 
        @blur="hideContextMenu()" 
        @click="toggleMenu($event)"
         />

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
export default {
    name: "ellipsis-button",
    props: {
        "postId": Number
    },
    data() {
        return{
            isContextVisible: false,
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
        showContextMenu(event) {
            event.data;
            console.log('show menu')
        },
        hideContextMenu() {
            this.isContextVisible = false;
            console.log('hide')
        }
    }
}
</script>

<style>

</style>