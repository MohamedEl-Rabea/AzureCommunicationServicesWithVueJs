<template>
  <div>
    <input id="callee-id-input" type="text" placeholder="Who would you like to call?"
      style="margin-bottom:1em; width: 200px;" v-model="userToCall" />
    <div>
      <button id="call-button" type="button" :disabled="!outgoing.canCall" @click="startCall">
        Start Call
      </button>
      &nbsp;
      <button id="hang-up-button" type="button" :disabled="!outgoing.canHangUp" @click="hangUp">
        Hang Up
      </button>
    </div>
  </div>
</template>
<script>
export default {
  name: "OutgoingCall",
  computed: {
    outgoing: {
      get: function () {
        return this.$store.getters['getOutgoing']
      }
    },
    userToCall: {
      get() {
        return this.$store.state.outgoing.userToCall
      },
      set(value) {
        this.$store.commit('userToCall', value)
      }
    }
  },
  methods: {
    startCall() {
      this.$store.dispatch('startCall');
    },
    hangUp() {
      this.$store.dispatch('hangUp');
    }
  },
}
</script>

<style scoped>
a {
  color: #42b983;
}
</style>