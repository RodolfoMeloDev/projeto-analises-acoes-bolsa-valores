import axios from "axios";

const apiLogin = axios.create({
  baseURL: "https://localhost:7206/api/BaseTicker",
});

export default apiLogin;
