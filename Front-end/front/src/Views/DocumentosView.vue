<template>
    <div class="container-Inicial">
 
        <div class="wrap-Inicial">
            <span class = "login-form-title">
            Bem Vindo!
            </span>
            <div class="container-login-form-btn">
            <button type="submit" class="Inicial-form-btn">Alterar Usuario</button>
            </div>
            <div class="container-login-form-btn">
            <button type="submit" @click="documentos" class="Inicial-form-btn">Documentos</button>
            </div>
            <div class="container-login-form-btn">
            <button type="submit" @click="logout" class="Inicial-form-btn">Sair</button>
            </div>
        </div>
    </div>
  </template>
  
  <script>
  export default {
    methods: {
      logout() {
        localStorage.removeItem('token');
        this.$router.push('/');
      }
    },
    documentos(){
      this.$router.push('/documentos');
    }
  };
  </script>
  
  <style scoped>
  /* Estilos para a home */
  </style>
  