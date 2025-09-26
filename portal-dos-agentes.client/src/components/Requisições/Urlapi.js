import axios from "axios";
const api = axios.create({
  baseURL: /*"https://api-sga-iwsq.onrender.com"*/ "https://localhost:7293",
  timeout: 20000
})
export default api;
