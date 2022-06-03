<template>
  <div>
    <NavBar v-if="verificarAcceso"></NavBar>
    <Beginning v-else></Beginning>
    <Footer></Footer>
  </div>
</template>

<script>
import NavBar from './components/shared/NavBar.vue'
import Beginning from './views/Beginning.vue'
import Footer from './components/shared/Footer'
import {existeItemLocal} from '@/assets/localStorage/index.js'

export default {
  name: 'App',

  components: {
    NavBar,
    Beginning,
    Footer
  },

  computed:{
    verificarAcceso(){
      return existeItemLocal("tokenheliconia");
    }
  }
}
</script>

<style>

</style>
