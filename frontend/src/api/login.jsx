import axios from 'axios';

const apiLogin = axios.create({
    baseURL: "https://localhost:7206/api/Login"
});

export default apiLogin;
