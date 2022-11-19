import axios from "axios";

const apiSegments = axios.create({
  baseURL: "https://localhost:7206/api/Segments",
});

export default apiSegments;
