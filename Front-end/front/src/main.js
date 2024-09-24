import { createApp } from 'vue';
import { createRouter, createWebHistory } from 'vue-router';
import App from './App.vue';
import HomeView from './Views/HomeView.vue';
import LoginView from './Views/LoginView.vue';
import CadastroView from './Views/CadastroView.vue';
import DocumentosView from './Views/DocumentosView.vue';
import PerfilView from './Views/PerfilView.vue';
import '@fortawesome/fontawesome-free/css/all.css';
import '@fortawesome/fontawesome-free/js/all.js';
import 'bootstrap/dist/css/bootstrap.min.css';

const routes = [
    { path: '/', component: LoginView },
    { path: '/register', component: CadastroView }, 
    { path: '/documentos', component: DocumentosView},
    { path: '/perfil', component: PerfilView},
    { path: '/home', component: HomeView, meta: { requiresAuth: true } },
    // Outras rotas...
  ];
  
  const router = createRouter({
    history: createWebHistory(),
    routes,
  });
  
  router.beforeEach((to, from, next) => {
    const token = localStorage.getItem('token');
    
    // Verifique se a rota requer autenticação
    if (to.meta.requiresAuth && !token) {
      // Se não estiver autenticado, redirecione para a página de login
      next({ path: '/' });
    } else {
      next();
    }
  });
  
export default router;

createApp(App)
  .use(router)
  .mount('#app');
