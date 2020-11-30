import CONFIG from "../config";
import axios from "axios";
import APIService from "./APIService";
export default class TrechoService {
  static createTrecho(idLocalOrigem, idLocalDestino) {
    const loggedUser = APIService.getLoggedUser();
    return axios.post(
      `${CONFIG.API_URL_BASE}/Trecho`,
      {
        idLocalOrigem,
        idLocalDestino
      },
      {
        headers: {
          authorization: `Bearer ${loggedUser}`,
          "Content-Type": "application/json"
        }
      }
    );
  }

  static editTrecho(id, idLocalOrigem, idLocalDestino) {
    const loggedUser = APIService.getLoggedUser();
    return axios.put(
      `${CONFIG.API_URL_BASE}/Trecho/${id}`,
      {
        idLocalOrigem,
        idLocalDestino
      },
      {
        headers: {
          authorization: `Bearer ${loggedUser}`,
          "Content-Type": "application/json"
        }
      }
    );
  }

  static deleteTrecho(id) {
    const loggedUser = APIService.getLoggedUser();
    return axios.delete(`${CONFIG.API_URL_BASE}/Trecho/${id}`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }

  static trechos() {
    const loggedUser = APIService.getLoggedUser();
    return axios.get(`${CONFIG.API_URL_BASE}/Trecho/`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }
  static getTrecho(id) {
    const loggedUser = APIService.getLoggedUser();
    return axios.get(`${CONFIG.API_URL_BASE}/Trecho/${id}`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }
}
