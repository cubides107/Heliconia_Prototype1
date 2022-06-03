<template>
  <div>
    <b-navbar toggleable class="navbar-color">
      <b-navbar-brand :to="{ name: 'Home' }" class="navbar-title">
        <b-icon icon="house-fill" font-scale="1.2" class="icono-color"></b-icon>
      </b-navbar-brand>

      <b-nav-form>
        <b-form-input
          size="sm"
          class="mr-sm-2"
          placeholder="Buscar categoria"
        ></b-form-input>
        <b-button size="sm" class="my-2 my-sm-0" type="submit">Buscar</b-button>
      </b-nav-form>

      <b-navbar-toggle target="navbar-toggle-collapse">
        <template #default="{ expanded }">
          <b-icon
            v-if="expanded"
            icon="chevron-bar-up"
            font-scale="1.2"
            class="icono-color"
          ></b-icon>
          <b-icon
            v-else
            icon="star-fill"
            animation="fade"
            font-scale="1.2"
            class="icono-color"
          ></b-icon>
        </template>
      </b-navbar-toggle>

      <b-collapse id="navbar-toggle-collapse" is-nav>
        <b-navbar-nav class="ml-auto">
          <b-nav-item href="#">Link 1</b-nav-item>
          <b-nav-item href="#">Link 2</b-nav-item>
          <b-nav-item href="#" disabled>Disabled</b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>

    <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: "NavBar",
};
</script>

<style>
.navbar-color {
  background-color: rgba(252, 204, 212, 0.612);
}

.icono-color {
  color: rgba(36, 99, 102, 0.612);
}
</style>