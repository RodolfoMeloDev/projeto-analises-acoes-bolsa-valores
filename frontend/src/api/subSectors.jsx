import axios from "axios";

const apiSubSectors = axios.create({
  baseURL: "https://localhost:7206/api/SubSectors",
});

export default apiSubSectors;
