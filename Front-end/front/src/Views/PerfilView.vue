<template>
    <div>
        <header class="header">
            <div class="header-container">
              <div class="header-buttons2">
                  <button @click="goToProfile" class="header-btn">
                    <img src="../assets/perfil.png" alt="Perfil" class="icon">
                  </button>
                </div>
                <span class="username">{{ username }}</span>
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
            <div class="container-Altera">
                <form class="login-form" @submit.prevent="cadastro">
                    <span class = "login-form-title">
                        Alteração do usuario
                    </span>
                    <div class="wrap-input" :class="{'has-val' : nome !== ''}">
                    <input class="input" type="text" v-model="nome" required />
                    <span class="focus-input" data-placeholder="Nome"></span>
                    </div>

                    <div class="wrap-input" :class="{'has-val' : papel !== ''}">
                    <input class="input" type="text" v-model="papel" required />
                    <span class="focus-input" data-placeholder="Papel"></span>
                    </div>
                    <div class="wrap-input" :class="{'has-val' : email !== ''}">
                        <input class="input" type="email" v-model="email" required />
                        <span class="focus-input" data-placeholder="Email"></span>
                    </div>
                    <div class="wrap-input" :class="{'has-val' : password !== ''}">
                        <input class="input" type="password" v-model="password" required />
                        <span class="focus-input" data-placeholder="Senha"></span>
                    </div>
                    <div class="container-login-form-btn">
                        <button type="submit" class="login-form-btn">Salvar</button>
                    </div>
                    <div class="text=center">
                        <a @click.prevent="goToLogin" href="#" class="txt1">Cancelar</a>
                    </div>
                </form>
            </div>
        </div>
    </div>
  </template>
  
  <script>
  export default {
      data() {
          return {
              username: 'Leonardo Ramalho Duarte' // Substitua pelo nome do usuário logado
          };
      },
      methods: {
          logout() {
              localStorage.removeItem('token');
              this.$router.push('/login');
          },
          goToProfile() {
              this.$router.push('/perfil'); // Ajuste a rota para a página de perfil
          },
          documentos() {
              this.$router.push('/documentos');
          }
      }
  };
  </script>
  