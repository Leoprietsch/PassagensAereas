import CONFIG from "../config";
import axios from "axios";
import APIService from "./APIService";
export default class ReservaService {
  static createReserva(idTrecho, idClasseDeVoo, idsOpcionais) {
    const idUsuario = APIService.getLoggedUserId();
    console.log(idUsuario);
    const loggedUser = APIService.getLoggedUser();
    console.log(loggedUser);

    return axios.post(
      `${CONFIG.API_URL_BASE}/usuario/${idUsuario}/reserva`,
      {
        idTrecho,
        idClasseDeVoo,
        idsOpcionais
      },
      {
        headers: {
          authorization: `Bearer ${loggedUser}`,
          "Content-Type": "application/json"
        }
      }
    );
  }

  static deleteReserva(id) {
    const idUsuario = APIService.getLoggedUserId();
    const loggedUser = APIService.getLoggedUser();
    return axios.delete(
      `${CONFIG.API_URL_BASE}/usuario/${idUsuario}/reserva/${id}`,
      {
        headers: {
          authorization: `Bearer ${loggedUser}`,
          "Content-Type": "application/json"
        }
      }
    );
  }

  static reservas() {
    const idUsuario = APIService.getLoggedUserId();
    const loggedUser = APIService.getLoggedUser();
    return axios.get(`${CONFIG.API_URL_BASE}/usuario/${idUsuario}/Reserva/`, {
      headers: {
        authorization: `Bearer ${loggedUser}`,
        "Content-Type": "application/json"
      }
    });
  }
  static getReserva(id) {
    const idUsuario = APIService.getLoggedUserId();
    const loggedUser = APIService.getLoggedUser();
    return axios.get(
      `${CONFIG.API_URL_BASE}/usuario/${idUsuario}/Reserva/${id}`,
      {
        headers: {
          authorization: `Bearer ${loggedUser}`,
          "Content-Type": "application/json"
        }
      }
    );
  }

  static calculateValor(idTrecho, idClasseDeVoo, idsOpcionais) {
    const loggedUser = APIService.getLoggedUser();
    return axios.post(
      `${CONFIG.API_URL_BASE}/usuario/reserva/valor`,
      {
        idTrecho,
        idClasseDeVoo,
        idsOpcionais
      },
      {
        headers: {
          authorization: `Bearer ${loggedUser}`,
          "Content-Type": "application/json"
        }
      }
    );
  }
}
