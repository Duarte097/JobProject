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
                <div class="header-buttons3">
                    <button @click="alterarUsuario" class="perfil-form-btn">Inicio</button>
                </div>
                <div class="header-buttons4">
                    <button @click="documentos" class="perfil-form-btn">Documentos</button>
                </div>
                <div class="header-buttons">
                  <button @click="logout" class="header-btn">
                      <img src="../assets/Sair.png" alt="Sair" class="icon"/>
                  </button>
                </div>
            </div>
        </header>
      <div class="container-Inicial">
          <span class="Inicial-form-title" @click="openDocument">
            <div class="banner">
                <div class="container">
                <h1>Gestão de Documentos</h1>
                <h2>para empresas que pensam grande!</h2>
                <p>Centralize, organize e acesse os seus documentos de forma rápida e segura com nosso software.</p>
                <!--button>Quero conhecer o Digitaldoc</button-->
                </div>
                <img src="../assets/banner.png" alt="Imagem da banner">
            </div>
          </span>
      </div>
  </div>
</template>

<script>
import UserService from '@/services/UsuariosService';
import '../Style.css';

export default {
    data() {
        return {
            nome: "",
        };
    },
    methods: {
        async carregarDados() {
            try {
                const usuario = await UserService.obterUsuarios();
                this.nome = usuario.nome;
            } catch (error) {
                alert('Erro ao buscar usuários: ' + error.response.data);
            }
        },
        logout() {
            localStorage.removeItem('token');
            this.$router.push('/');
        },
        goToProfile() {
            this.$router.push('/perfil'); 
        },
        documentos() {
            this.$router.push('/documentos');
        }
    },
    mounted() {
        this.carregarDados();
    }
};
</script>
