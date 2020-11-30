import APIService from "./APIService";
import CONFIG from "../config";
import axios from "axios";
export default class ClasseDeVooService {
  static createClasseDeVoo(descricao, valorFixoDoVoo, valorPorMilha) {
    const loggedUser = APIService.getLoggedUser();
    return axios.post(
      `${CONFIG.API_URL_BASE}/ClasseDeVoo`,
      {
        descricao,
        valorFixoDoVoo,
        valorPorMilha
      },
      {
        headers: {
          authorization: `Bearer ${loggedUser}`,
          "Content-Type": "application/json"
        }
      }
    );
  }

  static editClasseDeVoo(id, descricao, valorFixoDoVoo, valorPorMilha) {
    const loggedUser = APIService.getLoggedUser();
    return axios.put(
      `${CONFIG.API_URL_BASE}/ClasseDeVoo/${id}`,
      {
        descricao,
        valorFixoDoVoo,
        valorPorMilha
      },
      {
        headers: {
          authorization: `Bearer ${loggedUser}`,
          "Content-Type": "application/json"
        }
      }
    );
  }

  static deleteClasseDeVoo(id) {
    const loggedUser = APIService.getLoggedUser();
    return axios.delete(`${CONFIG.API_URL_BASE}/ClasseDeVoo/${id}`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }

  static classesDeVoo() {
    const loggedUser = APIService.getLoggedUser();
    return axios.get(`${CONFIG.API_URL_BASE}/ClasseDeVoo`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }

  static getClasseDeVoo(id) {
    const loggedUser = APIService.getLoggedUser();
    return axios.get(`${CONFIG.API_URL_BASE}/ClasseDeVoo/${id}`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }
}
