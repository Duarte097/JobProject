import axios from '../auth';

class  DocumentosService {
    constructor() {
        this.nomeUsuario = "";
        this.papel = "";
        this.documentos = [];
    }

    // Método para obter todos os documentos
    async obterDocumentos() {
        try {
            const response = await axios.get('Documentos/visualizar');
            this.documentos = response.data;
            return response.data;
        } catch (error) {
            throw new Error('Erro ao buscar documentos: ' + error.response?.data || error.message);
        }
    }

    // Método para deletar documento
    async deletarDocumento(idDocumento) {
        try {
            await axios.delete(`Documentos/deletar/${idDocumento}`, {
                headers: {
                    Papel: this.papel,
                },
            });
            return true;
        } catch (error) {
            throw new Error('Erro ao deletar documento: ' + error.response?.data || error.message);
        }
    }

    // Método para download de documento
    async Download(idDocumento, nome) {
        try {
            const response = await axios.get(`Documentos/download/${idDocumento}`, {
                responseType: 'blob',
            });
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const a = document.createElement('a');
            a.style.display = 'none';
            a.href = url;
            a.download = nome; // nome do arquivo para download
            document.body.appendChild(a);
            a.click();
            window.URL.revokeObjectURL(url);
        } catch (error) {
            throw new Error('Erro ao baixar documento: ' + error.response?.data || error.message);
        }
    }
}

export default new DocumentosService();
