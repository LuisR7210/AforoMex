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
  position: fixed;
  bottom: 0;
  width: 100%;
  background-color: #222;
  height: 50px;
}
p {
    text-align: center;
    color: white;
}
</style>