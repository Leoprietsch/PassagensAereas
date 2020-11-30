import React from "react";
import { Link, Redirect } from "react-router-dom";
import {
  CustomInput,
  Alert,
  Button,
  Navbar,
  NavbarBrand,
  Nav,
  NavItem,
  Form,
  FormGroup,
  Label,
  Input
} from "reactstrap";
import "./createreserva.css";
import TrechoService from "../../../../Services/TrechoService";
import ClasseDeVooService from "../../../../Services/ClasseDeVooService";
import OpcionalService from "../../../../Services/OpcionalService";
import ReservaService from "../../../../Services/ReservaService";

import APIService from "../../../../Services/APIService";

export default class CreateReserva extends React.Component {
  constructor() {
    super();
    this.state = this.getInitialState();
    this.handleChange = this.handleChange.bind(this);
    this.createReserva = this.createReserva.bind(this);
    this.handleCheckBox = this.handleCheckBox.bind(this);
    this.calculateValor = this.calculateValor.bind(this);
  }

  componentWillMount() {
    const user = APIService.getLoggedUser();
    if (user === null) {
      this.setState({
        shouldRedirect: true
      });
    } else {
      this.getTrechos();
      this.getOpcionais();
      this.getClassesDeVoo();
    }
  }

  getInitialState() {
    return {
      trechos: [],
      classesDeVoo: [],
      opcionais: [],
      trecho: "",
      classeDeVoo: "",
      idsOpcionais: [],
      valor: "",
      shouldRedirect: false,
      error: ""
    };
  }

  handleChange(event) {
    const target = event.target;
    const value = target.value;
    const name = target.name;
    this.setState({
      [name]: value
    });
  }

  handleCheckBox(id) {
    var idsOpcionaisSelecionados = this.state.idsOpcionais.slice();
    if (idsOpcionaisSelecionados.includes(id)) {
      const index = idsOpcionaisSelecionados.indexOf(id);
      if (index !== -1) {
        idsOpcionaisSelecionados.splice(index, 1);
      }
    } else {
      idsOpcionaisSelecionados.push(id);
    }
    this.setState({
      idsOpcionais: idsOpcionaisSelecionados
    });
  }

  calculateValor() {
    const reserva = this.state;
    if (reserva.classeDeVoo === "" || reserva.trecho === "") {
      return;
    } else {
      return ReservaService.calculateValor(
        reserva.trecho,
        reserva.classeDeVoo,
        reserva.idsOpcionais
      ).then(result => {
        var valor = result.data;
        this.setState({
          valor: valor
        });
      });
    }
  }

  createReserva() {
    const reserva = this.state;
    return ReservaService.createReserva(
      reserva.trecho,
      reserva.classeDeVoo,
      reserva.idsOpcionais
    )
      .then(result => {
        this.setState({
          shouldRedirect: true
        });
      })
      .catch(err => {
        this.setState({
          error: err.response.data.error
        });
      });
  }

  isLoggedin() {
    if (localStorage.getItem("user_logged") === null) {
      return <Redirect to="/login" />;
    } else {
      return undefined;
    }
  }

  renderAlert() {
    return this.state.error ? (
      <Alert color="danger">{this.state.error}</Alert>
    ) : (
      undefined
    );
  }

  shouldRedirect() {
    return this.state.shouldRedirect;
  }

  getTrechos() {
    return TrechoService.trechos()
      .then(result => {
        let trechos = result.data;
        this.setState({
          trechos: trechos
        });
      })
      .catch(err => {
        this.setState({
          shouldRedirect: true,
          error: err.response.data.error
        });
        this.props.shouldLogout();
      });
  }

  getClassesDeVoo() {
    return ClasseDeVooService.classesDeVoo()
      .then(result => {
        let classesDeVoo = result.data;
        this.setState({
          classesDeVoo: classesDeVoo
        });
      })
      .catch(err => {
        this.setState({
          shouldRedirect: true,
          error: err.response.data.error
        });
        this.props.shouldLogout();
      });
  }

  getOpcionais() {
    return OpcionalService.opcionais()
      .then(result => {
        let opcionais = result.data;
        this.setState({
          opcionais: opcionais
        });
      })
      .catch(err => {
        this.setState({
          shouldRedirect: true,
          error: err.response.data.error
        });
        this.props.shouldLogout();
      });
  }

  renderTrechos() {
    return this.state.trechos.map((trecho, key) => {
      return (
        <option key={key} value={trecho.id}>
          {trecho.localOrigem.nome} -> {trecho.localDestino.nome}
        </option>
      );
    });
  }

  renderClassesDeVoo() {
    return this.state.classesDeVoo.map((classeDeVoo, key) => {
      return (
        <option key={key} value={classeDeVoo.id} id={classeDeVoo.id}>
          {classeDeVoo.descricao}
        </option>
      );
    });
  }

  renderOpcionais() {
    if (this.state.opcionais.length > 0) {
      return this.state.opcionais.map((opcional, key) => {
        return (
          <CustomInput
            key={key}
            type="checkbox"
            id={opcional.nome}
            value={opcional.id}
            label={opcional.nome}
            onChange={() => this.handleCheckBox(opcional.id)}
          />
        );
      });
    }
  }

  render() {
    return (
      <div className="createReserva">
        {this.shouldRedirect() ? <Redirect to="/home" /> : undefined}
        {this.isLoggedin()}
        <div className="createReserva--navbar">
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
                  to="/home"
                >
                  VOLTAR
                </Link>
              </NavItem>
              <NavItem>
                <Link
                  style={{ textDecoration: "none", color: "black" }}
                  to="/login"
                >
                  SAIR
                </Link>
              </NavItem>
            </Nav>
          </Navbar>
        </div>

        <div className="createReserva--content">
          <Form>
            <FormGroup>
              <Label for="trecho">Trechos</Label>
              <Input
                defaultValue="0"
                type="select"
                bsSize="lg"
                id="trecho"
                name="trecho"
                onChange={this.handleChange}
              >
                <option value="0" disabled>
                  Escolha um trecho
                </option>
                {this.renderTrechos()}
              </Input>
            </FormGroup>
            <FormGroup>
              <Label for="classeDeVoo">Classes de Voo</Label>
              <Input
                defaultValue="0"
                type="select"
                bsSize="lg"
                id="classeDeVoo"
                name="classeDeVoo"
                onChange={this.handleChange}
              >
                <option value="0" disabled>
                  Escolha uma classe
                </option>
                {this.renderClassesDeVoo()}
              </Input>
            </FormGroup>
            <FormGroup className="opcionais">
              <Label for="opcionais">Opcionais</Label>
              <div>{this.renderOpcionais()}</div>
            </FormGroup>
            <FormGroup className="botoes valor">
              <Button color="success" onClick={this.createReserva} to="/home">
                Reservar
              </Button>
              <div>
                <Button
                  color="success"
                  onClick={this.calculateValor}
                  to="/home"
                >
                  Calcular Valor
                </Button>
                <Input
                  type="text"
                  bsSize="lg"
                  id="classeDeVoo"
                  name="classeDeVoo"
                  value={this.state.valor}
                  onChange={this.handleChange}
                />
              </div>
              {this.renderAlert()}
            </FormGroup>
          </Form>
        </div>
      </div>
    );
  }
}
