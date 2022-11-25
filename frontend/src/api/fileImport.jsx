import axios from "axios";

const apiFileImport = axios.create({
  baseURL: "https://localhost:7206/api/FileImports",
});

export default apiFileImport;
