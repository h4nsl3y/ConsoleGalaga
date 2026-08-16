import axios from "axios";

export const baseAPI = axios.create({
    baseURL: "http://localhost:10001/",
    headers: {
        "Content-Type": "application/json",
    },
});

export default baseAPI;