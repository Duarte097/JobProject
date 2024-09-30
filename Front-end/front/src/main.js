import { createApp } from 'vue';
import { createRouter, createWebHistory } from 'vue-router';
import App from './App.vue';
import HomeView from './Views/HomeView.vue';
import LoginView from './Views/LoginView.vue';
import CadastroView from './Views/CadastroView.vue';
import DocumentosView from './Views/DocumentosView.vue';
import PerfilView from './Views/PerfilView.vue';
import UploadArquivo from './Views/CadastroDocumentosView.vue';
import EditarDocumento from './Views/EditarDocumentoView.vue';
import '@fortawesome/fontawesome-free/css/all.css';
import '@fortawesome/fontawesome-free/js/all.js';
import 'bootstrap/dist/css/bootstrap.min.css';

const routes = [
    { path: '/', component: LoginView },
    { path: '/register', component: CadastroView }, 
    { path: '/documentos', component: DocumentosView},
    { path: '/uploadArquivo', component: UploadArquivo},
    { path: '/perfil', component: PerfilView},
    { path: '/editar/:idDocumento', component: EditarDocumento},
    { path: '/home', component: HomeView, meta: { requiresAuth: true } },

  ];
  
  const router = createRouter({
    history: createWebHistory(),
    routes,
  });
  
  router.beforeEach((to, from, next) => {
    const token = localStorage.getItem('token');

    if (to.meta.requiresAuth && !token) {

      next({ path: '/' });
    } else {
      next();
    }
  });
  
export default router;

createApp(App)
  .use(router)
  .mount('#app');
