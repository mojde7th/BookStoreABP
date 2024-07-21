// src/services/authService.js
import axios from 'axios';

const API_URL = 'https://localhost:44355/api/customaccount';

export const login = (username, password) => {
    return axios.post(`${API_URL}/Login`, { username, password });
};
