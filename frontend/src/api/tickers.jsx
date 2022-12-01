import axios from "axios";

const apiTickers = axios.create({
  baseURL: "https://localhost:7206/api/Tickers",
});

export default apiTickers;
