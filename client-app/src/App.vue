<template>
  <div>
    <CommunicationService />
  </div>
</template>

<script>
import CommunicationService from './components/CommunicationService.vue'

export default {
  components: { CommunicationService }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
