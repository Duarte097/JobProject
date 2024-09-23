<template>
    <div class="home">
      <h1>Bem-vindo!</h1>
      <button @click="logout">Sair</button>
    </div>
  </template>
  
  <script>
  export default {
    methods: {
      logout() {
        localStorage.removeItem('token');
        this.$router.push('/login');
      }
    }
  };
  </script>
  
  <style scoped>
  /* Estilos para a home */
  </style>
  