<template>
  <div>
      <header class="header">
          <div class="header-container">
            <div class="header-buttons2">
                <button @click="goToProfile" class="header-btn">
                  <img src="../assets/perfil.png" alt="Perfil" class="icon">
                </button>
              </div>
              <span class="username">{{ nome }}</span>
              <div class="header-buttons">
                <button @click="logout" class="header-btn">
                    <img src="../assets/Sair.png" alt="Sair" class="icon"/>
                </button>
              </div>
          </div>
      </header>
      <div class="container-Inicial">
          <div class="wrap-Inicial">
              <span class="login-form-title">
                  Bem Vindo!
              </span>
              <div class="container-login-form-btn">
                  <button @click="alterarUsuario" class="Inicial-form-btn">Alterar Usuário</button>
              </div>
              <div class="container-login-form-btn">
                  <button @click="documentos" class="Inicial-form-btn">Documentos</button>
              </div>
          </div>
          <span class="Inicial-form-title" @click="openDocument">
              AQUI!
          </span>
      </div>
  </div>
</template>

<script>
import axios from '../auth';
import '../Style.css';

export default {
    data() {
        return {
            nome: "",
        };
    },
    methods: {
        async obterUsuarios() {
            try {
                const userId = localStorage.getItem('idUsuarios'); 
                const response = await axios.get(`Usuarios/${userId}`); 
                
                this.nome = response.data.nome;
            } catch (error) {
                alert('Erro ao buscar usuários: ' + error.response.data);
            }
        },
        logout() {
            localStorage.removeItem('token');
            this.$router.push('/');
        },
        goToProfile() {
            this.$router.push('/perfil'); // Ajuste a rota para a página de perfil
        },
        documentos() {
            this.$router.push('/documentos');
        }
    },
    mounted() {
        this.obterUsuarios();
    }
};
</script>
