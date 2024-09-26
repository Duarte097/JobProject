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
                    <span class="login-form-title">
                       Editar Arquivo
                    </span>
                    <div class="wrap-input" :class="{'has-val' : descricao !== ''}">
                        <input class="input" type="text" v-model="descricao" required />
                        <span class="focus-input" data-placeholder="Descrição"></span>
                    </div>
                    <div class="wrap-input" :class="{'has-val' : versao !== ''}">
                        <input class="input" type="text" v-model="versao" required />
                        <span class="focus-input" data-placeholder="Versão"></span>
                    </div>
                    <div class="wrap-input">
                        <div class="categoria-container" style="color: gray">
                            <div >Categoria: </div>
                            <select v-model="selected" style="background-color: gray">
                                <option disabled value="" >Por favor selecione uma</option>
                                <option>Contratos</option>
                                <option>Faturas</option>
                                <option>Relatórios</option>
                            </select>
                        </div>
                    </div>
                    <div class="wrap-input">
                        <div class="status-container">
                            <div style="color: gray">Status:</div>
                            <input type="radio" id="ativo" value="Ativo" v-model="status" />
                            <label for="ativo" style="color: gray">Ativo</label>

                            <input type="radio" id="inativo" value="Inativo" v-model="status" />
                            <label for="inativo" style="color: gray">Inativo</label>
                        </div>
                    </div>
                    <div class="wrap-input">
                        <div class="status-container">
                            <input type="file" ref="fileInput" @change="onFileChange" style="display:none">
                            <button @click="triggerFileInput" class="header-btn">
                                <img src="../assets/Upload.png" alt="Upload" class="icon"/>
                            </button>
                        </div>
                    </div>
                    <div class="container-login-form-btn">
                        <button type="submit" class="login-form-btn">Salvar</button>
                    </div>
                    <div class="text=center">
                        <a @click.prevent="documentos" href="#" class="txt1">Cancelar</a>
                    </div>
                </form>
            </div>
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
            iddocumento:"",
            nomeDocumento: "",
            descricao: "",
            categoria: "",
            status: "",
            versao: "",
            idusuario:"",
            selected: ""
        };
    },
    methods: {
        triggerFileInput(event) {
            event.preventDefault();
            this.$refs.fileInput.click();
        },
        onFileChange(event) {
            const file = event.target.files[0];
            if (file) {
                this.nomeDocumento = file.name; // Puxa o nome do arquivo selecionado
                console.log("Arquivo selecionado:", this.nomeDocumento);
            } else {
                console.log("Nenhum arquivo selecionado");
            }
        },
        async obterDocumentos() {
            try {
                const response = await axios.get('Documentos/visualizar',);
                this.iddocumento = response.data.IdDocumento;
                this.nomeDocumento = response.data.nomeDocumento;
                this.descricao = response.data.descricao;
                this.versao = response.data.versao // Salvar todos os documentos em um array
            } catch (error) {
                alert('Erro ao buscar documentos: ' + error.response?.data || error.message);
            }
        },
        async Alterar(){
            try {
                await axios.put(`editar/${this.iddocumento}`, {
                    nome: this.nomeDocumento,
                    descricao: this.descricao,
                    versao: this.versao,
                    categoria: this.selected || '',
                    status: this.status === 'Ativo' ? 1 : 0
                });
                this.$router.push('/documentos');
            } catch (error) {
                alert('Falha no editar: ' + error.response.data);
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
        },
        async obterUsuarios() {
            try {
                const token = localStorage.getItem('token');
                const userId = localStorage.getItem('idUsuarios'); 
                const response = await axios.get(`Usuarios/${userId}`); 

                if (!token) {
                    this.$router.push('/');
                    return;
                }
                this.nome = response.data.nome;
            } catch (error) {
                alert('Erro ao buscar usuários: ' + error.response.data);
            }
        }
    },
    mounted() {
        this.obterUsuarios();
    }
};
</script>
