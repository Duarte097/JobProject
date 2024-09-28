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
                    <button @click="carregarArquivo" class="perfil-form-btn">Carregar Arquivo</button>
                </div>
                <!--div class="header-buttons">
                    <input type="file" ref="fileInput" @change="onFileChange" style="display:none">
                    <button @click="triggerFileInput" class="perfil-form-btn">Carregar Arquivo</button>
                </div-->
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
                        <th scope="col">Versão</th>
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
                        <td>{{ this.status }}</td>
                        <td>{{ documento.versaoAtual }}</td>
                        <td>
                            <div class="d-flex justify-content-center">
                                <div class="icon-container" @click="Editar(documento.idDocumento)">
                                    <!--(documento.idDocumento)-->
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
                                <div class="icon-container" @click="Download(documento.idDocumento, documento.nome)">
                                    <span class="fa fa-download pointer"></span>
                                </div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
import DocumentosService from '@/services/DocumentosService';
import UserService from '@/services/UsuariosService';


export default {
    data() {
        return {
            nomeUsuario: "",
            status:"",
            documentos: [],
        };
    },
    methods: {
        async carregarDados() {
            try {
                const usuario = await UserService.obterUsuarios();
                this.nomeUsuario = usuario.nome;
                this.papel = usuario.papel;
                
                const documentos = await DocumentosService.obterDocumentos();
                this.documentos = documentos;
                this.status = documentos.status;
                if(this.status == 0)
                {
                    this.status = 'Inativo'
                    
                }else
                {
                    this.status = 'Ativo'
                }
            } catch (error) {
                alert(error.message);
            }
        },
        async deletarDocumento(idDocumento) {
            try {
                const sucesso = await DocumentosService.deletarDocumento(idDocumento);
                if (sucesso) {
                    this.documentos = await DocumentosService.obterDocumentos(); // Atualizar a lista de documentos
                    alert('Documento deletado com sucesso.');
                }
            } catch (error) {
                alert(error.message);
            }
        },
        async Download(idDocumento, nome) {
            try {
                await DocumentosService.Download(idDocumento, nome);
            } catch (error) {
                alert('Erro ao baixar documento: ' + error.message);
            }
        },
        async Editar(idDocumento) {
            if(this.papel == "Usuario"){
                alert("Usuarios não tem permissão para editar documentos.");
            }else
            {
                this.$router.push(`/editar/${idDocumento}`);
            }
            
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
        carregarArquivo(){
            this.$router.push('/uploadArquivo');
        }
    },
    mounted() {
        this.carregarDados();
    }
};
</script>