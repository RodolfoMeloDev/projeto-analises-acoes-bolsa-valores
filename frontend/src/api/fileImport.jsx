import axios from "axios";

const apiFileImport = axios.create({
  baseURL: "https://localhost:7206/api/FileImports",
  headers: {
    Authorization: "Bearer " + localStorage.getItem("token"),
  },
});

export default apiFileImport;
