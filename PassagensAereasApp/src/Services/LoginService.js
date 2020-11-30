import CONFIG from "../config";
import axios from "axios";
export default class LoginService {
  static login(email, senha) {
    return axios
      .post(`${CONFIG.API_URL_BASE}/usuario/login`, {
        email: email,
        senha: senha
      })
      .then(result => {
        var token = result.data.token;
        var userId = result.data.idUsuario;
        var userRole = result.data.role;
        LoginService._settoken(token, userId, userRole);

        return result;
      });
  }

  static register(
    primeiroNome,
    ultimoNome,
    cpf,
    dataDeNascimento,
    email,
    senha
  ) {
    var data = `${dataDeNascimento}T00:00:00.000Z`;
    console.log(data);
    return axios
      .post(`${CONFIG.API_URL_BASE}/usuario`, {
        primeiroNome: primeiroNome,
        ultimoNome: ultimoNome,
        cpf: cpf,
        dataDeNascimento: data,
        email: email,
        senha: senha
      })
      .then(result => {
        axios
          .post(`${CONFIG.API_URL_BASE}/usuario/login`, {
            email: email,
            senha: senha
          })
          .then(result => {
            var token = result.data.token;
            var userId = result.data.idUsuario;
            var userRole = result.data.role;
            LoginService._settoken(token, userId, userRole);

            return result;
          });
        return result;
      });
  }

  static _settoken(token, userId, userRole) {
    localStorage.setItem("user_logged", token);
    localStorage.setItem("user_id", userId);
    localStorage.setItem("user_role", userRole);
  }
}
