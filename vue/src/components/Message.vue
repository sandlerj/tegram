<template>
  <div class="message" v-show="show" style="margin: 2rem">
  <div class="message-header">
    <p>{{ header }}</p>
    <font-awesome-icon icon="times" @click="closeMessage"/>
  </div>
  <div class="message-body">
      <slot></slot>
  </div>
</div>
</template>

<script>
export default {
    name: "message",
    props: ["header"],
    data() {
        return {
            show: true
        }
    },
    methods: {
        closeMessage() {
            this.show = false;
        }
    }

}
</script>

<style>

</style>