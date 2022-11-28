import axios from "axios";

const apiUsers = axios.create({
  baseURL: "https://localhost:7206/api/Users",
  headers: {
    Authorization: "Bearer " + localStorage.getItem("token"),
  },
});

export default apiUsers;
