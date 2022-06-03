<template>
  <div class="mt-3">
    <Imput placeholder="ingrese un correo" imputType="email"></Imput>
    <Imput placeholder="ingrese una contraseña" imputType="text"></Imput>

    <b-button pill variant="outline-primary">ingresar</b-button>
  </div>
</template>

<script>
import Imput from "../shared/Imput.vue";

export default {
  name: "Login",

  components: {
    Imput
  },

  computed: {},
};
</script>

<style>
</style>