import axios from "axios";

const apiSectors = axios.create({
  baseURL: "https://localhost:7206/api/Sectors",
});

export default apiSectors;
