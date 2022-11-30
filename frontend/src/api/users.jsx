import axios from "axios";

const apiUsers = axios.create({
  baseURL: "https://localhost:7206/api/Users",
});

export default apiUsers;
