<template>
    <div class="login">
      <h2>Login</h2>
      <form @submit.prevent="handleLogin">
        <div>
          <label for="email">Email:</label>
          <input type="email" v-model="email" required />
        </div>
        <div>
          <label for="password">Senha:</label>
          <input type="password" v-model="password" required />
        </div>
        <button type="submit">Login</button>
      </form>
      <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
    </div>
  </template>
  
  <script>
  import axios from '../auth';

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
        const response = await axios.post('auth/login', {
          email: this.email,
          senhaHash: this.password
        });
        localStorage.setItem('token', response.data.Token);
        this.$router.push('/home');
      } catch (error) {
        alert('Falha no login: ' + error.response.data);
      }
    }
  }
};
</script>
  
  <style>
  .login {
    max-width: 400px;
    margin: auto;
  }
  
  .error {
    color: red;
  }
  </style>


  