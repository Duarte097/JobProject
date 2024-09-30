import axios from 'axios';


const axiosInstance = axios.create({
  baseURL: 'http://localhost:5086/', 
});


axiosInstance.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
}, (error) => {
  return Promise.reject(error);
});


axiosInstance.interceptors.response.use((response) => {
  return response;
}, (error) => {
  if (error.response.status === 401) {
    localStorage.removeItem('token');
    //window.location.href = '/'; // Redireciona para a página de login
  }
  return Promise.reject(error);
});

export default axiosInstance;
