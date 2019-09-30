import React, { Component } from "react";

class NavBar extends Component {
  render() {
    return (
      <div>
        <script src="../node_modules/bootstrap/dist/js/bootstrap.js"></script>
        <script src="../node_modules/bootstrap/dist/js/bootstrap.bundle.js"></script>
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
          <a className="navbar-brand" href="#">
            Inventory Management
          </a>
          <button
            className="navbar-toggler"
            type="button"
            data-toggle="collapse"
            data-target="#navbarNavAltMarkup"
            aria-controls="navbarNavAltMarkup"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNavAltMarkup">
            <div className="navbar-nav">
              <a className="nav-item nav-link active">
                Home <span className="sr-only">(current)</span>
              </a>
              <a className="nav-item nav-link">Features</a>
              <a className="nav-item nav-link">Pricing</a>
            </div>
          </div>
        </nav>
      </div>
    );
  }
}

export default NavBar;
