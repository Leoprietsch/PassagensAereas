import React from "react";
import { Jumbotron } from "reactstrap";

export default class NotFound extends React.Component {
  render() {
    return (
      <div>
        <Jumbotron>
          <h1 className="display-3">Not Found</h1>
        </Jumbotron>
      </div>
    );
  }
}
