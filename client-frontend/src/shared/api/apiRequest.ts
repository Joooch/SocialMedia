import axios, { AxiosRequestConfig } from "axios";

//let backendURL = "https://localhost:7042"
let backendURL = "" // use proxy instead

export function get<T = any>(path: string, config?: AxiosRequestConfig | undefined) {
    return axios.get<T>(backendURL + path, config)
}

export function post<T = any>(path: string, data?: any, config?: AxiosRequestConfig | undefined) {
    return axios.post<T>(backendURL + path, data, config)
}

export function put<T = any>(path: string, config?: AxiosRequestConfig | undefined) {
    return axios.put<T>(backendURL + path, config)
}