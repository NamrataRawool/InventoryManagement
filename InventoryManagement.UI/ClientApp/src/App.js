import React, { Component } from "react";
import NavBar from "./components/navbar";
//import Counters from "./components/counters";
import AddProduct from "./components/AddProduct";

class App extends Component {
  render() {
    return (
      <React.Fragment>
        <NavBar />
        <br />
        <main className="container">
          <AddProduct />
        </main>
      </React.Fragment>
    );
  }
}

export default App;
