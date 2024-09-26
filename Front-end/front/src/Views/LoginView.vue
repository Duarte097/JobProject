<template>
    <div class="container-login">
        <div class="wrap-login">
            <form class="login-form" @submit.prevent="login">
                <span class = "login-form-title">
                    Login
                </span>
                <div class="wrap-input" :classname="{'has-val input' : email !== ''}">
                    <input class="input" type="email" v-model="email" required />
                    <span class="focus-input" data-placeholder="Email"></span>
                </div>
                <div class="wrap-input" :class="{'has-val' : password !== ''}">
                    <input class="input" type="password" v-model="password" required />
                    <span class="focus-input" data-placeholder="Senha"></span>
                </div>
                <div class="container-login-form-btn">
                    <button type="submit" class="login-form-btn">Login</button>
                </div>
                <div class="text=center">
                    <a href="#" class="txt1">Esqueceu a senha?</a>
                </div>
                <div class="text=center">
                    <a @click.prevent="goToRegister" href="#" class="txt1">Cadastrar</a>
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
      email: '',
      password: ''
    };
  },
  methods: {
    async login() {
      try {
        const response = await axios.post('api/auth/login', {
          email: this.email,
          senhaHash: this.password
        });
        //localStorage.setItem('token', response.data.Token);
        localStorage.setItem('token', response.data.Token || response.data.token); // Ajuste conforme a resposta
        console.log('token:', localStorage.getItem('token'));
        localStorage.setItem('idUsuarios', response.data.user.idUsuarios);
        this.$router.push('/home');
      } catch (error) {
        alert('Falha no login: ' + error.response.data);
      }
    },
    goToRegister() {
      this.$router.push('/register'); // Redireciona para a página de cadastro
    }
  }
};
</script>
  


  