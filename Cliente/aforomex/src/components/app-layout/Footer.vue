<template>
  <div>
    <footer>
        <p id="copyright">Copyright AforoMex® {{ year }}. Todos los derechos reservados.</p>
    </footer>
  </div>
</template>

<script>
export default {
  data() {
    return {
      year: (new Date).getFullYear()
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
footer {
  width: 100%;
  background-color: #222;
}
p {
    text-align: center;
    color: white;
}
</style>