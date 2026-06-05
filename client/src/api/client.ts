import axios from 'axios';

const api = axios.create({
  baseURL: '/api',
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response) {
      const message = error.response.data?.detail || error.response.data?.title || 'An error occurred';
      console.error(`API Error ${error.response.status}:`, message);
    }
    return Promise.reject(error);
  }
);

export default api;
