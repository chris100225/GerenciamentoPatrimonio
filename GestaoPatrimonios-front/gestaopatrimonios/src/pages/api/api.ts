import axios from "axios";


const apiLocal = "http://localhost:5164/api/";

const apiRemota = "";

export const api = axios.create({
  baseURL: apiLocal
})

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("Token");

  if (token) {
    config.headers.Authorization = "Bearer " + token;
  }

  return config;
});