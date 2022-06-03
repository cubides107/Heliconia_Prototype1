<template>
  <div class="mb-3">
    <b-form-select v-model="Selected" :options="Options" :size="tamano"></b-form-select>
  </div>
</template>

<script>
  export default {
    name: 'SelectOne',

    props:{
        Options: Array,
        Selected: Object,
        tamano: String
    }
  }
</script>