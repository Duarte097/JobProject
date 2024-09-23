import axios from 'axios';

// Crie uma instância do Axios
const axiosInstance = axios.create({
  baseURL: 'http://localhost:5086/', // Ajuste para a sua URL da API
});

// Adicione um interceptor para incluir o token
axiosInstance.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
}, (error) => {
  return Promise.reject(error);
});

// Adicione um interceptor para lidar com respostas
axiosInstance.interceptors.response.use((response) => {
  return response;
}, (error) => {
  if (error.response.status === 401) {
    localStorage.removeItem('token');
    window.location.href = '/login'; // Redireciona para a página de login
  }
  return Promise.reject(error);
});

export default axiosInstance;
