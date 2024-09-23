import { createApp } from 'vue';
import { createRouter, createWebHistory } from 'vue-router';
import App from './App.vue';
import HomeView from './Views/HomeView.vue';
import LoginView from './Views/LoginView.vue';

const routes = [
    { path: '/login', component: LoginView },
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
      next({ path: '/login' });
    } else {
      next();
    }
  });
  
  export default router;
createApp(App)
    .use(router)
    .mount('#app');
