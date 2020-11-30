import React from "react";

import { Switch, Route, Redirect } from "react-router-dom";
import Home from "./components/scenes/Home/Home";
import Login from "./components/scenes/Login/Login";
import NotFound from "./components/scenes/NotFound/NotFound";
import Register from "./components/scenes/Register/Register";
import CreateReserva from "./components/scenes/Reserva/CreateReserva/CreateReserva";

import "./app.css";

class App extends React.Component {
  render() {
    return (
      <div className="App">
        <Switch>
          <Route path="/404" component={NotFound} />
          <Route path="/" exact component={Home} />
          <Route path="/home" exact component={Home} />
          <Route path="/login" exact component={Login} />
          <Route path="/register" exact component={Register} />
          <Route path="/criarReserva" exact component={CreateReserva} />
          <Redirect to="/404" />
        </Switch>
      </div>
    );
  }
}

export default App;
