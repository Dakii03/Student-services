import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7043/",
  timeout:15000,
    headers:{"Content-Type":"application/json"}
});

export const StudentAPI = {
  getAll: () => api.get("/api/Students"),
  getById: (id) => api.get(`/api/Students/${id}`),
  update: (id, data) => api.put(`/api/Students/${id}`, data)
};

export const StudentPredmetAPI = {
  getAll: () => api.get("/api/StudentPredmets")
};

export const PredmetAPI = {
  getAll: () => api.get("/api/Predmets")
};

export const IspitAPI = {
  getAll: () => api.get("/api/Ispits")
};

export const IspitniRokAPI = {
  getAll: () => api.get("/api/IspitniRoks")
};

export const ZapisnikAPI = {
  getAll: () => api.get("/api/Zapisniks")
};
