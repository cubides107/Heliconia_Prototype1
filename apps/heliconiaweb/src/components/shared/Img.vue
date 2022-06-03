<template>
  <div>
    <div class="clearfix">
      <b-img left :src="enlace" fluid alt="imagen" ></b-img>
    </div>
  </div>
</template>

<script>
export default {
  name: "Imagen",
  //propiedades
  props: {
    enlace: String,
  },
};
</script>

<style scoped>
</style>