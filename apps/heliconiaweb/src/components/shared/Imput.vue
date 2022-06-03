<template>
  <div>
    <div class="mb-3">
      <input
        class="form-control"
        :type="imputType"
        :id="imputId"
        :aria-describedby="HelpId"
        v-model="campo"
        :disabled="desactivado"
        :placeholder="placeholder"
      />
      <div class="form-text" :id="HelpId">
        {{HelpTxt}} 
        <span class="badge badge-pill badge-warning" v-if="campoCorrecto">Campo invalido</span>
        <span class="badge badge-pill badge-success" v-if="campoOpcional">Campo opcional</span>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Imput",
  components: {
    
  },
  data() {
    return {
      campo: ""
    }
  },
  
  props: {
    imputId: String,
    imputType: String,
    HelpId: String,
    HelpTxt: String,
    campoCorrecto: Boolean,
    campoOpcional: Boolean,
    desactivado: {
      type: Boolean,
      default: false
    },
    placeholder:{
      type: String,
      default: ""
    }
  },
  watch:{
    campo(newValue, oldValue){
      this.$emit("GetValorCampoImput", {newValue, oldValue});
    }
  },
  methods: {
    ResetearCampo(){
      this.campo = "";
    }
  },
};
</script>

<style>
</style>