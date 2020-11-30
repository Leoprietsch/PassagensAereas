import React from "react";
import { Redirect, Link } from "react-router-dom";
import { Alert, Button, Form, FormGroup, Label, Input } from "reactstrap";
import LoginService from "../../../Services/LoginService";
import "./login.css";

export default class Login extends React.Component {
  constructor() {
    super();
    this.state = this.getInitialState();
    this.handleChange = this.handleChange.bind(this);
    this.onClickLoginButton = this.onClickLoginButton.bind(this);
  }

  getInitialState() {
    return {
      email: "",
      senha: "",
      error: "",
      token: ""
    };
  }

  onClickLoginButton() {
    const account = this.state;
    return LoginService.login(account.email, account.senha)
      .then(result => {
        const tk = result.data.token;
        this.setState({
          token: tk
        });
      })
      .catch(err => {
        this.setState({
          error: err.response.data.error
        });
      });
  }

  handleChange(event) {
    const target = event.target;
    const value = target.value;
    const name = target.name;
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
      <div className="login--content">
        {this.isLoggedin() ? <Redirect to="/home" /> : undefined}
        <div className="login--header">UpSKY</div>
        <Form>
          <FormGroup className="mb-2 mr-sm-2 mb-sm-0" id="formlogin">
            <Label for="email" className="mr-sm-2">
              Email
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
          <FormGroup className="mb-2 mr-sm-2 mb-sm-0" id="formlogin">
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
          <Button color="primary" onClick={this.onClickLoginButton} to="/login">
            Entrar
          </Button>
          <Link to="/register">
            <Button color="secondary">Registrar</Button>
          </Link>
        </Form>
        {this.renderAlert()}
      </div>
    );
  }
}
