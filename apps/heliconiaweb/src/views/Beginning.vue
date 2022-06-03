<template>
  <div>
    <div class="card mb-3">
      <div class="row no-gutters">
        <div class="col-md-4 d-flex justify-content-center">
          <Img
            class="mt-3"
            :enlace="ImagenLoqueo"
          ></Img>
        </div>
        <div class="col-md-8">
          <div class="card-body">
            <h5 class="card-title text-center">Ingrese, Mis Princesas</h5>
            <p class="card-text text-center">
              <small class="text-muted">En caso de presentar inconvenientes con el ingreso comuniquese con el administardor de la compañia</small>
            </p>
            <Login></Login>
            <MasOpciones></MasOpciones>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Login from "@/components/Beginning/Login.vue";
import Img from "@/components/shared/Img.vue";
import MasOpciones from "@/components/Beginning/MasOpciones"
import { ImagenEnlaces } from "@/assets/enums/Enlaces.js"

export default {
  name: "Beginning",

  components: {
    Login,
    Img,
    MasOpciones
  },

  computed:{
    ImagenLoqueo(){
      return ImagenEnlaces.ImagenLoqueo
    }
  }
};
</script>

<style>
</style>