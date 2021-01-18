<template>
  <div id="app">
    <Navbar></Navbar>
    <router-view />
  </div>
</template>

<script>
import Navbar from "./components/app-layout/Navbar";
export default {
  name: "app",
  components: {
    Navbar: Navbar,
  },
  data() {
    return {
      estados: [],
    };
  },
};
</script>

<style>
html,
body {
  height: 100%;
}
body {
  display: flex;
  flex-direction: column;
}
#app {
  font-family: "Times New Roman", Times, serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  flex: 1 0 auto;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}

#footer {
  width: 100%;
  background-color: #222;
  flex-shrink: 0;
}
#footer > p {
  text-align: center;
  color: white;
}
</style>
