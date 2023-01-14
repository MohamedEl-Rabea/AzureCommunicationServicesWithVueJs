<template>
    <div>
        <h3>Incoming Call from: {{ callerId }}</h3>
        <button id="call-button" type="button" @click="accept">
            Accept
        </button>
        &nbsp;
        <button id="hang-up-button" type="button" @click="decline">
            Decline
        </button>
    </div>
</template>

<script>
export default {
    computed: {
        callerId: {
            get: function () {
                return this.$store.state.incoming.callerId
            }
        },
    },
    methods: {
        accept() {
            this.$store.dispatch('acceptCall');
        },
        decline() {
            this.$store.dispatch('declineCall');
        }
    }
}
</script>

<style scoped>
a {
    color: #42b983;
}
</style>