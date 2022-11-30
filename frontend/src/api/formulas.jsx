import axios from "axios";

const apiFormulas = axios.create({
  baseURL: "https://localhost:7206/api/Formulas",
});

export default apiFormulas;
