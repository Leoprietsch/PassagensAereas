import APIService from "./APIService";
import CONFIG from "../config";
import axios from "axios";
export default class LocalService {
  static createLocal(latitude, longitude, nome) {
    const loggedUser = APIService.getLoggedUser();
    return axios.post(
      `${CONFIG.API_URL_BASE}/Local`,
      {
        latitude,
        longitude,
        nome
      },
      {
        headers: {
          authorization: `Bearer ${loggedUser}`,
          "Content-Type": "application/json"
        }
      }
    );
  }

  static editLocal(id, latitude, longitude, nome) {
    const loggedUser = APIService.getLoggedUser();
    return axios.put(
      `${CONFIG.API_URL_BASE}/Local/${id}`,
      {
        latitude,
        longitude,
        nome
      },
      {
        headers: {
          authorization: `Bearer ${loggedUser}`,
          "Content-Type": "application/json"
        }
      }
    );
  }

  static deleteLocal(id) {
    const loggedUser = APIService.getLoggedUser();
    return axios.delete(`${CONFIG.API_URL_BASE}/Local/${id}`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }

  static locais() {
    const loggedUser = APIService.getLoggedUser();
    return axios.get(`${CONFIG.API_URL_BASE}/Local/`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }
  static getLocal(id) {
    const loggedUser = APIService.getLoggedUser();
    return axios.get(`${CONFIG.API_URL_BASE}/Local/${id}`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }
}
