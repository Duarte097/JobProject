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
        <div class="container-login">
            <div class="wrap-login">
                <form class="login-form" @submit.prevent="cadastro">
                    <span class = "login-form-title">
                        Alteração do usuario
                    </span>
                    <div class="wrap-input" :class="{'has-val' : nome !== ''}">
                        <input class="input" type="text" v-model="nome" required />
                        <span class="focus-input" data-placeholder="Nome"></span>
                    </div>

                    <!--div class="wrap-input" :class="{'has-val' : papel !== ''}">
                        <input class="input" type="text" v-model="papel" required />
                        <span class="focus-input" data-placeholder="Papel"></span>
                    </div-->
                    <div class="wrap-input" :class="{'has-val' : password !== ''}">
                        <input class="input" type="password" v-model="password" required />
                        <span class="focus-input" data-placeholder="Senha"></span>
                    </div>
                    <div class="container-login-form-btn">
                        <button type="submit" @click.prevent="Alterar" class="login-form-btn">Salvar</button>
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
import axios from '../auth';
import '../Style.css';
import UserService from '@/services/UsuariosService';

export default {
    data() {
        return {
            nome: "",
            papel: "",
            password: "" 
        };
    },
    methods: {
        async carregarDados() {
            try {
                const usuario = await UserService.obterUsuarios();
                this.nome = usuario.nome;
                this.papel = usuario.papel;
                this.password = usuario.senhaHash;
                alert('Alterações feitas com sucesso!!');
            } catch (error) {
                alert('Erro ao buscar usuários: ' + error);
            }
        },
        async Alterar(){
            try {
                const userId = localStorage.getItem('idUsuarios'); 
                await axios.put(`Usuarios/${userId}`, {
                    nome: this.nome,
                    papel: this.papel,
                    senhaHash: this.password
                });
                this.$router.push('/home');
            } catch (error) {
                alert('Falha no cadastro: ' + error.response.data);
            }
        },
        logout() {
            localStorage.removeItem('token');
            localStorage.removeItem('userId');
            this.$router.push('/');
        },
        goToProfile() {
            this.$router.push('/perfil');
        },
        alterarUsuario() {
            this.$router.push('/home');
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
