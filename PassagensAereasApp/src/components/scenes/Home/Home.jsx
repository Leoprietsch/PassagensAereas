import React from "react";
import { Redirect, Link } from "react-router-dom";
import {
  UncontrolledDropdown,
  DropdownToggle,
  DropdownItem,
  DropdownMenu,
  Navbar,
  NavbarBrand,
  Nav,
  NavItem
} from "reactstrap";
import ReservaList from "../../ReservaList/ReservaList";
import "./home.css";

export default class Home extends React.Component {
  constructor() {
    super();
    this.state = {
      update: 0
    };
    this.logout = this.logout.bind(this);
  }

  logout() {
    localStorage.removeItem("user_logged");
    localStorage.removeItem("user_id");
    localStorage.removeItem("user_role");
  }

  isLoggedin() {
    if (
      localStorage.getItem("user_logged") === null ||
      localStorage.getItem("user_id" === null)
    ) {
      return <Redirect to="/login" />;
    } else {
      return undefined;
    }
  }

  renderBarraAdmin() {
    if (localStorage.getItem("user_role") === "Admin") {
      return (
        <UncontrolledDropdown nav inNavbar>
          <DropdownToggle nav caret>
            OPÇÕES
          </DropdownToggle>
          <DropdownMenu right>
            <DropdownItem>
              <Link
                style={{ textDecoration: "none", color: "black" }}
                to="/classeDeVoo"
              >
                Classes De Voo
              </Link>
            </DropdownItem>
            <DropdownItem>
              <Link
                style={{ textDecoration: "none", color: "black" }}
                to="/local"
              >
                Locais
              </Link>
            </DropdownItem>
            <DropdownItem>
              <Link
                style={{ textDecoration: "none", color: "black" }}
                to="/trecho"
              >
                Trechos
              </Link>
            </DropdownItem>

            <DropdownItem>
              <Link
                style={{ textDecoration: "none", color: "black" }}
                to="/opcional"
              >
                Opcionais
              </Link>
            </DropdownItem>
          </DropdownMenu>
        </UncontrolledDropdown>
      );
    }
  }

  render() {
    return (
      <div className="home">
        {this.isLoggedin()}
        <div className="home--navbar">
          <Navbar color="light" light expand="md">
            <NavbarBrand>
              <img
                src="http://wfarm3.dataknet.com/static/resources/icons/set63/ca9ab1a3.png"
                alt=""
              />UpSKY
            </NavbarBrand>
            <Nav className="ml-auto" navbar>
              <NavItem>
                <Link
                  style={{ textDecoration: "none", color: "black" }}
                  to="/criarReserva"
                >
                  NOVA RESERVA
                </Link>
              </NavItem>
              <NavItem>
                <Link
                  onClick={this.logout}
                  style={{ textDecoration: "none", color: "black" }}
                  to="/login"
                >
                  SAIR
                </Link>
              </NavItem>
              {this.renderBarraAdmin()}
            </Nav>
          </Navbar>
        </div>
        <div className="home--content">
          <div className="home--title">
            <h1>RESERVAS</h1>
          </div>
          <ReservaList shouldLogout={this.logout} />
        </div>
      </div>
    );
  }
}
