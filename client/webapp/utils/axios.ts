import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "http://localhost/api",
  headers: {
    ContentType: "application/json",
  },
});

export default axiosInstance;
