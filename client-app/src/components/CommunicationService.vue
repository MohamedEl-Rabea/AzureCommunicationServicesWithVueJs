<template>
    <div>
        <h1>Vue + Azure Communication Services</h1>
        <h3>Your user ID: {{ auth }} </h3>
        <div id="video" v-show="showVideo">
            <Video />
        </div>
        <div v-show="showOutgoing">
            <OutgoingCall />
        </div>
        <div v-show="showIncoming">
            <IncomingCall />
        </div>
    </div>
</template>

<script>
import OutgoingCall from './OutgoingCall.vue'
import IncomingCall from './IncomingCall.vue'
import Video from './Video.vue'

export default {
    components: { OutgoingCall, IncomingCall, Video },
    created() {
        this.$store.dispatch('getAuthData');
    },
    computed: {
        auth: {
            get: function () {
                return this.$store.getters['getAuthData']
            }
        },
        showVideo: {
            get: function () {
                return this.$store.state.call.showVideo
            }
        },
        showOutgoing: {
            get: function () {
                return this.$store.state.call.outgoing
            }
        },
        showIncoming: {
            get: function () {
                return this.$store.state.call.incoming
            }
        }
    }
}
</script>

<style scoped>
a {
    color: #42b983;
}
</style>