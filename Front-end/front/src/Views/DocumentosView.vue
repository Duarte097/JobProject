<template>
    <div>
        <header class="header">
            <div class="header-container">
                <div class="header-buttons2">
                    <button @click="goToProfile" class="header-btn">
                        <img src="../assets/perfil.png" alt="Perfil" class="icon">
                    </button>
                </div>
                <span class="username">{{ nomeUsuario }}</span>
                <div class="header-buttons3">
                    <button @click="inicio" class="perfil-form-btn">Inicio</button>
                </div>
                <div class="header-buttons4">
                    <button @click="documentosRoute" class="perfil-form-btn">Documentos</button>
                </div>
                <div class="header-buttons">
                    <input type="file" ref="fileInput" @change="onFileChange" style="display:none">
                    <button @click="triggerFileInput" class="header-btn">
                        <img src="../assets/Upload.png" alt="Upload" class="icon"/>
                    </button>
                </div>
                <div class="header-buttons">
                    <button @click="logout" class="header-btn">
                        <img src="../assets/Sair.png" alt="Sair" class="icon"/>
                    </button>
                </div>
            </div>
        </header>
        <div class="container">
            <div class="d-flex">
                <input v-model="newTask" type="text" placeholder="Enter task" class="form-control">
                <button @click="addTask" class="btn btn-warning rounded-0">Submit</button>
            </div>
            <table class="table table-bordered custom-table mt-5">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Nome</th>
                        <th scope="col">Descrição</th>
                        <th scope="col">Categoria</th>
                        <th scope="col">Status</th>
                        <th scope="col">Versão Atual</th>
                        <th scope="col" class="text-center">Editar</th>
                        <th scope="col" >Deletar</th>
                        <th scope="col">Download</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(documento, index) in documentos" :key="documento.idDocumento">
                        <th scope="row">{{ index + 1 }}</th>
                        <td>{{ documento.nome }}</td>
                        <td>{{ documento.descricao }}</td>
                        <td>{{ documento.categoria }}</td>
                        <td>{{ documento.status }}</td>
                        <td>{{ documento.versaoAtual }}</td>
                        <td>
                            <div class="d-flex justify-content-center">
                                <div class="icon-container" @click="Editar(documento.idDocumento)">
                                    <span class="fa fa-pen pointer"></span>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="d-flex justify-content-center">
                                <div class="icon-container" @click="deletarDocumento(documento.idDocumento)">
                                    <span class="fa fa-trash pointer"></span>
                                </div>
                            </div>   
                        </td>
                        <td>
                            <div class="d-flex justify-content-center">
                                <button @click="Upload" class="header-btn">
                                    <img src="../assets/Download.png" alt="Upload" class="icon"/>
                                </button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
import axios from '../auth';
import '../Style.css';

export default {
    data() {
        return {
            nomeUsuario: "",
            documento: [], // Array para armazenar os documentos
        };
    },
    methods: {
        async obterUsuarios() {
            try {
                const userId = localStorage.getItem('idUsuarios'); // Corrigido aqui
                const response = await axios.get(`Usuarios/${userId}`);
                this.nomeUsuario = response.data.nome; // Ajustado para "nomeUsuario"
            } catch (error) {
                alert('Erro ao buscar usuário: ' + error.response?.data || error.message);
            }
        },
        // Obter todos os documentos da API
        async obterDocumentos() {
            try {
                const response = await axios.get('documento');
                this.documentos = response.data; // Salvar todos os documentos em um array
            } catch (error) {
                alert('Erro ao buscar documentos: ' + error.response?.data || error.message);
            }
        },
        triggerFileInput() {
            this.$refs.fileInput.click();
        },

        // Função chamada quando o arquivo é selecionado
        onFileChange(event) {
            this.selectedFile = event.target.files[0];
            if (this.selectedFile) {
                this.uploadFile();
            }
        },
        // Função de upload
        async uploadFile() {
            try {
                if (!this.selectedFile) {
                    alert('Nenhum arquivo selecionado');
                    return;
                }

                const formData = new FormData();
                formData.append('file', this.selectedFile);
                formData.append('nome', 'Nome do Documento');
                formData.append('descricao', 'Descrição do Documento');
                formData.append('categoria', 'Categoria');
                formData.append('versao', '1.0');

                await axios.post('/documento/upload', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                });

                alert('Upload realizado com sucesso');
            } catch (error) {
                alert('Erro ao fazer upload: ' + error.response?.data || error.message);
            }
        },
        confirmarDeletar(idDocumento) {
            if (confirm("Tem certeza que deseja deletar este documento?")) {
                this.deletarDocumento(idDocumento);
            }
        },
        async deletarDocumento(idDocumento) {
            try {
                await axios.delete(`documento/${idDocumento}`); // Corrigido para usar a rota correta
                await this.obterDocumentos(); // Atualizar lista após a exclusão
                alert('Documento deletado com sucesso.');
            } catch (error) {
                alert('Erro ao deletar documento: ' + error.response?.data || error.message);
            }
        },
        Editar(idDocumento) {
            this.$router.push(`/documentos/editar/${idDocumento}`); // Redirecionar para página de edição
        },
        // Logout do usuário
        logout() {
            localStorage.removeItem('token');
            localStorage.removeItem('userId');
            this.$router.push('/');
        },
        goToProfile() {
            this.$router.push('/perfil');
        },
        inicio() {
            this.$router.push('/home');
        },
        documentosRoute() {
            this.$router.push('/documentos');
        },
        addTask() {
            if (this.newTask) {
                // Lógica para adicionar uma nova tarefa ou similar
                this.newTask = ''; // Limpar o campo após adicionar
            }
        },
        ChangeStatus() {
            // Implementar lógica para mudar o status do documento
        }
    },
    mounted() {
        this.obterDocumentos(); // Carregar documentos ao montar o componente
        this.obterUsuarios();   // Carregar usuário ao montar o componente
    }
};
</script>