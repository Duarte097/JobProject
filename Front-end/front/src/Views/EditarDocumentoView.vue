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
                        <button type="submit" @click.prevent="Alterar(documento.idDocumento)" class="login-form-btn">Salvar</button>
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
import DocumentosService from '@/services/DocumentosService';
import UserService from '@/services/UsuariosService';


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
            selected: "",
            documento: [],
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
        async carregarDados() {
            try {
                const userId = localStorage.getItem('idUsuarios'); 
                const usuario = await UserService.obterUsuarios();
                this.nomeUsuario = usuario.nome;
                this.idusuario = userId;
                
                const documentos = await DocumentosService.obterDocumentos();
                this.documentos = documentos;
                this.iddocumento = documentos.idDocumento;
                this.nomeDocumento = documentos.nomeDocumento;
                this.descricao = documentos.descricao;
                this.categoria = documentos.categoria;
                this.status = documentos.status;
            } catch (error) {
                alert(error.message);
            }
        },
        async Alterar(idDocumento){
            try {
                console.log(idDocumento);
                const formData = new FormData();
                const userId = localStorage.getItem('idUsuarios'); 
                const token = localStorage.getItem('token');

                this.idusuario = userId;
                formData.append("nome", this.nomeDocumento);
                formData.append("descricao", this.descricao);
                formData.append("versao", this.versao);
                formData.append("categoria", this.selected || '');
                formData.append('UsuarioId', this.idusuario);
                formData.append("status", this.status === 'Ativo' ? 1 : 0);

                // Adicionar o arquivo ao FormData, caso tenha sido selecionado
                const file = this.$refs.fileInput.files[0];
                if (file) {
                    formData.append("file", file);
                }
                
                await axios.put(`Documentos/editar/${idDocumento}`, formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data',
                        'Authorization': `Bearer ${token}`
                    }
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
    },
    mounted() {
        this.carregarDados();
    }
};
</script>
