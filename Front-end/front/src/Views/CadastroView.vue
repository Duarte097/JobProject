<template>
    <div class="container-login">
        <div class="wrap-login">
            <form class="login-form" @submit.prevent="cadastro">
                <span class = "login-form-title">
                    Cadastro de Usuarios
                </span>
                <div class="wrap-input" :class="{'has-val' : nome !== ''}">
                  <input class="input" type="text" v-model="nome" required />
                  <span class="focus-input" data-placeholder="Nome"></span>
                </div>

                <!--div class="wrap-input" :class="{'has-val' : papel !== ''}">
                  <input class="input" type="text" v-model="papel" required />
                  <span class="focus-input" data-placeholder="Papel"></span>
                </div-->
                <div class="wrap-input" :class="{'has-val' : email !== ''}">
                    <input class="input" type="email" v-model="email" required />
                    <span class="focus-input" data-placeholder="Email"></span>
                </div>
                <div class="wrap-input" :class="{'has-val' : password !== ''}">
                    <input class="input" type="password" v-model="password" required />
                    <span class="focus-input" data-placeholder="Senha"></span>
                </div>
                <div class="container-login-form-btn">
                    <button type="submit" class="login-form-btn">Cadastrar</button>
                </div>
                <div class="text=center">
                    <a @click.prevent="goToLogin" href="#" class="txt1">Cancelar</a>
                </div>
            </form>
        </div>
    </div>
  </template>
  
  <script>
  import axios from '../auth';
  import '../Style.css';

  export default {
  data() {
    return {
      nome: '',
      papel: '',
      email: '',
      password: ''
    };
  },
  methods: {
    async cadastro() {
      try {
        if(this.email == "leo_r1089@hotmail.com" || this.email == "leonardo_wow@outlook.com")
        {
            this.papel = "Administrador";
        }else
        {
            this.papel = "Usuario";
        }
         await axios.post('usuarios/register', {
          nome: this.nome,
          papel: this.papel,
          email: this.email,
          senhaHash: this.password
        });
        alert('Cadastro feito com sucesso!!');
        this.$router.push('/');
      } catch (error) {
        alert('Falha no cadastro: ' + error.response.data);
      }
    },
    goToLogin(){
      this.$router.push('/')
    }
  }
};
</script>
  


  