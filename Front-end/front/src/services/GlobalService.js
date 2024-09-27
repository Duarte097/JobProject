class GlobalService {
    constructor(router) {
        this.router = router;
    }
    goToProfile() {
        this.$router.push('/perfil');
    }
    inicio() {
        this.$router.push('/home');
    }
    documentosRoute() {
        this.$router.push('/documentos');
    }
}

export default GlobalService;