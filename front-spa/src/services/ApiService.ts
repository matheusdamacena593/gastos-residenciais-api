import axios, { type AxiosInstance } from "axios";
import NProgress from "nprogress";
import "nprogress/nprogress.css";

const baseUrl = import.meta.env.VITE_BASE_URL;

const apiService: AxiosInstance = axios.create({
  baseURL: baseUrl,
  timeout: 30_000,
});

NProgress.configure({ showSpinner: false });

let pendingRequests = 0;

apiService.interceptors.request.use(
  (config) => {
    pendingRequests += 1;
    if (pendingRequests === 1) NProgress.start();
    return config;
  },
  (error) => {
    pendingRequests = Math.max(0, pendingRequests - 1);
    if (pendingRequests === 0) NProgress.done();
    return Promise.reject(error);
  },
);

apiService.interceptors.response.use(
  (response) => {
    pendingRequests = Math.max(0, pendingRequests - 1);
    if (pendingRequests === 0) NProgress.done();
    return response;
  },
  (error) => {
    pendingRequests = Math.max(0, pendingRequests - 1);
    if (pendingRequests === 0) NProgress.done();
    return Promise.reject(error);
  },
);

export default apiService;
