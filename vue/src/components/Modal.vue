<template>
    <div class="message custom-modal" v-show="isOpen">
        <div class="message-header">
            <h2>{{ title }}</h2>
            <font-awesome-icon icon="times" @click="toggleModal"/>
        </div>
        <div class="message-body">
            <slot></slot>
        </div>
    </div>
</template>

<script>
export default {
    name: "modal",
    data() {
        return {
            isOpen: false
        }
    },
    props: ["toggle", "title"],
    watch: {
        toggle () {
            this.toggleModal();
        }
    },
    methods: {
        toggleModal () {
            this.isOpen = !this.isOpen;
        }
    }

}
</script>

<style>
    .custom-modal {
        position: fixed;
        z-index: 999;
        left: 50%;
        transform: translateX(-50%);
        background-color: white;
    }

    .custom-modal::before {
        content: '';
        position: fixed;
        top: -50vh;
        left: -50vw;
        width: 200vw;
        height: 200vh;
        background-color: white;
        opacity: 0.5;
    }
</style>