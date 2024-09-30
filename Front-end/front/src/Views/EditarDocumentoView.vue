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
                        <button @click="documentosRoute" class="perfil-form-btn">Documentos</button>
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
                            <span class="focus-input" data-placeholder="Descricao"></span>
                        </div>
                        <!--div class="wrap-input" :class="{'has-val' : versao !== ''}">
                            <input class="input" type="text" v-model="versao" required />
                            <span class="focus-input" data-placeholder="Versão"></span>
                        </div-->
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
                        <!--div class="wrap-input">
                            <div class="status-container">
                                <input type="file" ref="fileInput" @change="onFileChange" style="display:none">
                                <button @click="triggerFileInput" class="header-btn">
                                    <img src="../assets/Upload.png" alt="Upload" class="icon"/>
                                </button>
                            </div>
                        </div-->
                        <div class="container-login-form-btn">
                            <button type="submit" @click.prevent="Alterar(this.idDocumento)" class="login-form-btn">Salvar</button>
                        </div>
                        <div class="text=center">
                            <a @click.prevent="documentosRoute" href="#" class="txt1">Cancelar</a>
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
                    this.nomeDocumento = file.name;
                    console.log("Arquivo selecionado:", this.nomeDocumento);
                } else {
                    console.log("Nenhum arquivo selecionado");
                }
            },
            async carregarDados() {
                try {
                    const userId = localStorage.getItem('idUsuarios'); 
                    const usuario = await UserService.obterUsuarios();
                    this.nome = usuario.nome;
                    this.idusuario = userId; 
                } catch (error) {
                    alert(error.message);
                }
            },
            async obterUmDocumento(idDocumento) {
                try {
                    console.log(idDocumento)
                    const documento = await axios.get(`Documentos/visualizaId/${idDocumento}`);
                    this.documentos = documento.data;
                    this.nomeDocumento = documento.data.nome;
                    this.descricao = documento.data.descricao;
                    this.selected = documento.data.categoria;
                    this.status = documento.status === 1 ? 'Inativo' : 'Ativo'; 
                    this.versao = documento.versao;

                    alert('Alterações feitas com sucesso!!');
                } catch (error) {
                    throw new Error('Erro ao buscar documentos: ' + error.documentos?.data || error.message);
                }
            },
            async Alterar(idDocumento) {
                try {
                    const userId = localStorage.getItem('idUsuarios'); 
                    const token = localStorage.getItem('token');

                    const data = {
                        Nome: this.nomeDocumento,
                        Descricao: this.descricao,
                        Categoria: this.selected || '',
                        Status: this.status === 'Ativo' ? 0 : 1,
                        UsuarioId: userId
                    };
                    console.log(this.nomeDocumento, this.descricao, this.selected, this.status, userId);
                    await axios.put(`Documentos/editar/${idDocumento}`, data, {
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${token}`
                        }
                    });

                    this.$router.push('/documentos');
                } catch (error) {
                    alert('Falha no editar: ' + error.response.data);
                    console.log(error.response.data);
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
            documentosRoute() {
                this.$router.push('/documentos');
            },
        },
        mounted() {
            this.idDocumento = this.$route.params.idDocumento;
            this.$nextTick(() => {
                this.obterUmDocumento(this.idDocumento);
                this.carregarDados();
            });
            this.carregarDados();
        }
    };
    </script>
