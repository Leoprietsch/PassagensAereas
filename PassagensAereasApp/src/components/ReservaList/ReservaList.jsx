import React from "react";
import "./reservalist.css";
import { Redirect } from "react-router-dom";
import { Table, Modal, ModalHeader, ModalFooter, Button } from "reactstrap";
import APIService from "../../Services/APIService";
import ReservaService from "../../Services/ReservaService";
export default class Home extends React.Component {
  constructor() {
    super();
    this.state = this.getInitialState();
    this.toggle = this.toggle.bind(this);
    this.deleteReserva = this.deleteReserva.bind(this);
  }

  componentWillMount() {
    const user = APIService.getLoggedUser();
    if (user === null) {
      this.setState({
        shouldRedirect: true
      });
    } else {
      this.getReservas();
    }
  }

  shouldRedirect() {
    return this.state.shouldRedirect;
  }

  toggle(id) {
    this.setState({
      modal: !this.state.modal,
      idToDelete: id
    });
  }

  getInitialState() {
    return {
      reservas: [],
      modal: false,
      idToDelete: "",
      shouldRedirect: false,
      error: ""
    };
  }

  getReservas() {
    return ReservaService.reservas()
      .then(result => {
        let reservas = result.data;
        this.setState({
          reservas: reservas,
          modal: false
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

  deleteReserva(id) {
    ReservaService.deleteReserva(id).then(() => {
      this.getReservas();
    });
  }

  renderOpcionais(reservaOpcional) {
    return reservaOpcional.map((reservaOpcional, key) => {
      return (
        <span key={key}>
          {reservaOpcional.opcional.nome}
          <br />
        </span>
      );
    });
  }

  renderList() {
    return this.state.reservas.map((reserva, key) => {
      return (
        <tr key={key}>
          <th scope="row">{reserva.id}</th>
          <td>{reserva.trecho.localOrigem.nome}</td>
          <td>{reserva.trecho.localDestino.nome}</td>
          <td>{reserva.classeDeVoo.descricao}</td>
          <td>{this.renderOpcionais(reserva.reservaOpcional)}</td>
          <td>{reserva.valor}</td>
          <td>
            <Button
              color="danger"
              size="sm"
              onClick={() => this.toggle(reserva.id)}
            >
              Cancelar
            </Button>
          </td>
        </tr>
      );
    });
  }

  render() {
    return (
      <div className="list">
        {this.shouldRedirect() ? <Redirect to="/login" /> : undefined}
        <Modal isOpen={this.state.modal} className={this.props.className}>
          <ModalHeader>Deseja mesmo cancelar essa reserva?</ModalHeader>
          <ModalFooter>
            <Button
              color="danger"
              onClick={() => {
                this.deleteReserva(this.state.idToDelete);
              }}
            >
              Cancelar Reserva
            </Button>
            <Button color="secondary" onClick={this.toggle}>
              Voltar
            </Button>
          </ModalFooter>
        </Modal>
        <Table responsive>
          <thead>
            <tr>
              <th>ID</th>
              <th>Local Origem</th>
              <th>Local Destino</th>
              <th>Classe</th>
              <th>Opcionais</th>
              <th>Valor</th>
              <th />
            </tr>
          </thead>
          <tbody>{this.renderList()}</tbody>
        </Table>
      </div>
    );
  }
}
