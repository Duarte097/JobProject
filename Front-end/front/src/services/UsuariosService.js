import axios from '../auth';
class UsuariosService{
    async obterUsuarios() {
        try {
            const userId = localStorage.getItem('idUsuarios'); 
            const response = await axios.get(`Usuarios/${userId}`);
            return response.data; 
        } catch (error) {
            alert('Erro ao buscar usuários: ' + error.response.data);
            throw error; 
        }
    }
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
    }
}

export default new UsuariosService();