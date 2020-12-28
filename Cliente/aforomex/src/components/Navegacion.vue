<template>
  <div>
    <h2>AforoMex</h2>
    <nav>
      <router-link class="spacing" v-for="routes in links" 
      v-bind:key="routes.id"
      :to="`${routes.page}`">{{routes.text}}</router-link>
    </nav>
  </div>
</template>

<script>
export default {
  name: "Navegacion",
  data() {
    return {
      links: [
        {
          id: 0,
          text: 'Inicio',
          page:'/inicio'
        },
        {
          id: 1,
          text: 'About',
          page:'/login'
        },
        {
          id: 2,
          text: 'Contact',
          page:'/nuevoConsumidor'
        }
      ]
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.spacing {
  margin-right: 10px;
}
</style>