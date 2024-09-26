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
                        Upload de Arquivo
                    </span>
                    <div class="wrap-input" :class="{'has-val' : descricao !== ''}">
                        <input class="input" type="text" v-model="descricao" required />
                        <span class="focus-input" data-placeholder="Descrição"></span>
                    </div>
                    <div class="wrap-input" :class="{'has-val' : versao !== ''}">
                        <input class="input" type="text" v-model="versao" required />
                        <span class="focus-input" data-placeholder="Versão"></span>
                    </div>
                    <div class="status-container">
                        <div style="color: gray">Categoria: {{ selected }}</div>
                        <select v-model="selected">
                            <option disabled value="">Por favor selecione uma</option>
                            <option>Contratos</option>
                            <option>Faturas</option>
                            <option>Relatórios</option>
                        </select>
                    </div>
                    <div class="status-container">
                        <div style="color: gray">Status:</div>
                        <input type="radio" id="ativo" value="Ativo" v-model="status" />
                        <label for="ativo" style="color: gray">Ativo</label>

                        <input type="radio" id="inativo" value="Inativo" v-model="status" />
                        <label for="inativo" style="color: gray">Inativo</label>
                    </div>
                    <div class="status-container">
                        <input type="file" ref="fileInput" @change="onFileChange" style="display:none">
                        <button @click="triggerFileInput" class="header-btn">
                            <img src="../assets/Upload.png" alt="Upload" class="icon"/>
                        </button>
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
        async cadastro() {
            try {
                const formData = new FormData();
                const userId = localStorage.getItem('idUsuarios'); 
                const token = localStorage.getItem('token');
                
                // O nome do documento será o nome do arquivo selecionado
                const file = this.$refs.fileInput.files[0];
                if (file) {
                    formData.append('file', file);  // certifique-se que o nome corresponde ao que o backend espera
                    formData.append('nome', this.nomeDocumento || file.name); // Nome do documento pode ser o nome definido pelo usuário ou pelo arquivo
                } else {
                    alert("Por favor, selecione um arquivo antes de fazer o upload.");
                    return;
                }

                this.idusuario = userId;
                // Não precisa adicionar o UsuarioId, pois será recuperado no backend
                formData.append('files', file);  // Para o arquivo
                formData.append('nome', this.nomeDocumento || file.name);
                formData.append('descricao', this.descricao || '');
                formData.append('categoria', this.selected || '');
                formData.append('versaoAtual', this.versao || '');
                formData.append('UsuarioId', this.idusuario);
                formData.append('status', this.status === 'Ativo' ? 1 : 0); // Certifique-se de que o nome está correto
                
                console.log("Dados que estão sendo enviados:", {
                    nome: this.nomeDocumento,
                    descricao: this.descricao,
                    categoria: this.selected,
                    versao: this.versao,
                    file: file.name
                });

                await axios.post('Documentos/upload', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data',
                        'Authorization': `Bearer ${token}`
                    }
                });

                this.$router.push('/documentos');
            } catch (error) {
                if (error.response && error.response.status === 401) {
                    alert('Sua sessão expirou. Por favor, faça login novamente.');
                    //this.logout(); // Descomente se você estiver implementando logout
                } else {
                    console.error('Erro ao cadastrar documento:', error.response.data);
                    alert('Erro ao cadastrar documento: ' + JSON.stringify(error.response.data));
                }
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
