import React, { Component } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import Home from "./Home";

import AddProduct from "./Product/AddProduct";
import EditProduct from "./Product/EditProduct";
import AddCategory from "./Category/AddCategory";
import EditCategory from "./Category/EditCategory";

class NavBar extends Component {
  render() {
    return (
      <Router>
        <div>
          <script src="../node_modules/bootstrap/dist/js/bootstrap.js"></script>
          <script src="../node_modules/bootstrap/dist/js/bootstrap.bundle.js"></script>
          <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <a className="navbar-brand" href="/">
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
              <div className="navbar-nav  ml-auto">
                <ul className="navbar-nav">
                  <li>
                    <a href={"/"} className="nav-link active">
                      DashBoard
                    </a>
                  </li>
                  <li className="nav-item dropdown">
                    <a
                      className="nav-link dropdown-toggle"
                      href="/"
                      id="navbarDropdown"
                      role="button"
                      data-toggle="dropdown"
                      aria-haspopup="true"
                      aria-expanded="false"
                    >
                      Product
                    </a>
                    <div
                      className="dropdown-menu"
                      aria-labelledby="navbarDropdown"
                    >
                      <a href={"/AddProduct"} className="dropdown-item">
                        Add Product
                      </a>
                      <a href={"/EditProduct"} className="dropdown-item">
                        Edit Product
                      </a>
                    </div>
                  </li>
                  <li className="nav-item dropdown">
                    <a
                      className="nav-link dropdown-toggle"
                      href="/"
                      id="navbarDropdown"
                      role="button"
                      data-toggle="dropdown"
                      aria-haspopup="true"
                      aria-expanded="false"
                    >
                      Category
                    </a>
                    <div
                      className="dropdown-menu"
                      aria-labelledby="navbarDropdown"
                    >
                      <a href={"/AddCategory"} className="dropdown-item">
                        Add Category
                      </a>
                      <a href={"/EditCategory"} className="dropdown-item">
                        Edit Category
                      </a>
                    </div>
                  </li>
                </ul>
              </div>
            </div>
          </nav>
          <Switch>
            <Route exact path="/" component={Home} />
            <Route path="/EditProduct" component={EditProduct} />
            <Route path="/AddProduct" component={AddProduct} />
            <Route path="/EditCategory" component={EditCategory} />
            <Route path="/AddCategory" component={AddCategory} />
          </Switch>
        </div>
      </Router>
    );
  }
}

export default NavBar;
