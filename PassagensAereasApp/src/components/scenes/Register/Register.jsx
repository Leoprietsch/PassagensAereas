import React from "react";
import { Redirect, Link } from "react-router-dom";
import { Alert, Button, Form, FormGroup, Label, Input } from "reactstrap";
import LoginService from "../../../Services/LoginService";
import "./register.css";

export default class Register extends React.Component {
  constructor() {
    super();
    this.state = this.getInitialState();
    this.handleChange = this.handleChange.bind(this);
    this.onClickRegisterButton = this.onClickRegisterButton.bind(this);
  }

  getInitialState() {
    return {
      primeiroNome: "",
      ultimoNome: "",
      cpf: "",
      dataDeNascimento: "",
      email: "",
      senha: "",
      error: ""
    };
  }

  onClickRegisterButton() {
    const conta = this.state;

    return LoginService.register(
      conta.primeiroNome,
      conta.ultimoNome,
      conta.cpf,
      conta.dataDeNascimento,
      conta.email,
      conta.senha
    );
  }

  handleChange(event) {
    const target = event.target;
    const value = target.value;
    const name = target.name;
    console.log(name);
    console.log(value);
    this.setState({
      [name]: value
    });
  }

  isLoggedin() {
    return localStorage.getItem("user_logged");
  }

  renderAlert() {
    return this.state.error ? (
      <Alert color="danger">{this.state.error}</Alert>
    ) : (
      undefined
    );
  }

  render() {
    return (
      <div className="register--content">
        {this.isLoggedin() ? <Redirect to="/home" /> : undefined}
        <div className="register--header">UpSKY</div>
        <Form>
          <FormGroup className="mb-2 mr-sm-2 mb-sm-0" id="formregister">
            <Label for="primeiroNome" className="mr-sm-2">
              Primeiro Nome
            </Label>
            <Input
              onChange={this.handleChange}
              type="text"
              name="primeiroNome"
              id="primeiroNome"
              maxlength="128"
              placeholder="Seu primeiro nome"
            />
          </FormGroup>
          <FormGroup className="mb-2 mr-sm-2 mb-sm-0" id="formregister">
            <Label for="ultimoNome" className="mr-sm-2">
              Último Nome
            </Label>
            <Input
              onChange={this.handleChange}
              type="text"
              name="ultimoNome"
              id="ultimoNome"
              maxlength="128"
              placeholder="Seu último nome"
            />
          </FormGroup>
          <FormGroup className="mb-2 mr-sm-2 mb-sm-0" id="formregister">
            <Label for="cpf" className="mr-sm-2">
              CPF
            </Label>
            <Input
              onChange={this.handleChange}
              type="text"
              name="cpf"
              id="cpf"
              maxlength="11"
              placeholder="Seu CPF"
            />
          </FormGroup>
          <FormGroup className="mb-2 mr-sm-2 mb-sm-0" id="formregister">
            <Label for="dataDeNascimento">Datetime</Label>
            <Input
              onChange={this.handleChange}
              type="date"
              name="dataDeNascimento"
              id="dataDeNascimento"
              placeholder="date placeholder"
            />
          </FormGroup>
          <FormGroup className="mb-2 mr-sm-2 mb-sm-0" id="formregister">
            <Label for="email" className="mr-sm-2">
              E-mail
            </Label>
            <Input
              autoComplete="username email"
              onChange={this.handleChange}
              type="email"
              name="email"
              id="email"
              placeholder="Seu e-mail"
            />
          </FormGroup>
          <FormGroup className="mb-2 mr-sm-2 mb-sm-0" id="formregister">
            <Label for="senha" className="mr-sm-2">
              Senha
            </Label>
            <Input
              autoComplete="current-password"
              onChange={this.handleChange}
              type="password"
              name="senha"
              id="senha"
              placeholder="Sua senha"
            />
          </FormGroup>
          <Link to="/login">
            <Button
              color="primary"
              onClick={this.onClickRegisterButton}
              to="/login"
            >
              Registrar
            </Button>
          </Link>
          <Link to="/login">
            <Button color="secondary" to="/login">
              Já tenho conta
            </Button>
          </Link>
        </Form>
        {this.renderAlert()}
      </div>
    );
  }
}
