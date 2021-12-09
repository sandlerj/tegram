<template>
    <div>
        <form ></form> <!-- axious action: PUT /posts with post and uploadImg as the request body -->
    </div>
</template>

<script>
export default {
    name: "upload-post-card",
    data() {
        return {
            post : {
                accountId: this.$store.accountId,
                MediaLink: "",
                caption: ""
            }
        }
    }
}
</script>

<style>

</style>