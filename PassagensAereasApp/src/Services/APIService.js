export default class APIService {
  static getLoggedUser() {
    const token = localStorage.getItem("user_logged");
    if (token) {
      return localStorage.getItem("user_logged");
    }
    return null;
  }

  static getLoggedUserId() {
    const id = localStorage.getItem("user_id");
    if (id) {
      return localStorage.getItem("user_id");
    }
    return null;
  }

  static getLoggedUserRole() {
    const role = localStorage.getItem("user_role");
    if (role) {
      return localStorage.getItem("user_role");
    }
    return null;
  }
}
