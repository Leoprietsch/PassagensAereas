import CONFIG from "../config";
import axios from "axios";
import APIService from "./APIService";
export default class OpcionalService {
  static createOpcional(nome, descricao, valor) {
    const loggedUser = APIService.getLoggedUser();
    return axios.post(
      `${CONFIG.API_URL_BASE}/Opcional`,
      {
        nome,
        descricao,
        valor
      },
      {
        headers: {
          authorization: `Bearer ${loggedUser}`,
          "Content-Type": "application/json"
        }
      }
    );
  }

  static editOpcional(id, nome, descricao, valor) {
    const loggedUser = APIService.getLoggedUser();
    return axios.put(
      `${CONFIG.API_URL_BASE}/Opcional/${id}`,
      {
        nome,
        descricao,
        valor
      },
      {
        headers: {
          authorization: `Bearer ${loggedUser}`,
          "Content-Type": "application/json"
        }
      }
    );
  }

  static deleteOpcional(id) {
    const loggedUser = APIService.getLoggedUser();
    return axios.delete(`${CONFIG.API_URL_BASE}/Opcional/${id}`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }

  static opcionais() {
    const loggedUser = APIService.getLoggedUser();
    return axios.get(`${CONFIG.API_URL_BASE}/Opcional`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }
  static getOpcional(id) {
    const loggedUser = APIService.getLoggedUser();
    return axios.get(`${CONFIG.API_URL_BASE}/Opcional/${id}`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }
}
