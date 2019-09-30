import React, { Component } from "react";
import "../styles/addproduct.css";
import Axios from "axios";

class AddProduct extends Component {
  state = {
    product: {
      name: "",
      category: "",
      price: 0,
      noOfItems: 0
    },
    errors: {
      name: "",
      category: "",
      price: "",
      noOfItems: ""
    }
  };

  handleChange = event => {
    event.preventDefault();
    const { id, value } = event.target;
    let errors = this.state.errors;
    switch (id) {
      case "name":
        errors.name =
          value.length < 5 ? "Full Name must be 5 characters long!" : "";
        break;
      case "category":
        errors.category =
          value.length < 5 ? "Full Name must be 5 characters long!" : "";
        break;
      case "price":
        errors.price =
          value.length < 5 ? "Full Name must be 5 characters long!" : "";
        break;
      case "noOfItems":
        errors.noOfItems =
          value.length < 5 ? "Full Name must be 5 characters long!" : "";
        break;
    }
    this.setState({
      product: { ...this.state.product, [id]: event.target.value }
    });
  };

  validateForm = errors => {
    let valid = true;
    Object.values(errors).forEach(val => val.length > 0 && (valid = false));
    return valid;
  };

  handleSubmit = e => {
    e.preventDefault();
    if (this.validateForm(this.state.errors)) {
      Axios.post("api/Product/Add", this.state.product)
        .then(function(response) {
          console.log(response.data);
        })
        .catch(function(error) {
          console.log(error);
        });
    } else {
      console.error("Invalid Form");
    }
  };

  render() {
    return (
      <form>
        <h4>Product Details</h4>
        <br />
        <div className="form-group row">
          <label htmlFor="productName" className="col-sm-2 col-form-label">
            Name
          </label>
          <div className="col-sm-10">
            <input
              onChange={this.handleChange}
              type="text"
              className="form-control"
              id="name"
              required
            />
          </div>
        </div>
        <div className="form-group row">
          <label htmlFor="productCategory" className="col-sm-2 col-form-label">
            Category
          </label>
          <div className="col-sm-10">
            <input
              onChange={this.handleChange}
              type="text"
              className="form-control"
              id="category"
              required
            />
          </div>
        </div>
        <div className="form-group row">
          <label htmlFor="productPrice" className="col-sm-2 col-form-label">
            Price
          </label>
          <div className="col-sm-10">
            <input
              onChange={this.handleChange}
              type="text"
              className="form-control"
              id="price"
              required
            />
          </div>
        </div>
        <div className="form-group row">
          <label htmlFor="productPrice" className="col-sm-2 col-form-label">
            No. of Items
          </label>
          <div className="col-sm-10">
            <input
              onChange={this.handleChange}
              type="text"
              className="form-control"
              id="noOfItems"
              required
            />
          </div>
        </div>
        <div className="form-group row">
          <div className="col-sm-10">
            <button
              onClick={this.handleSubmit}
              type="submit"
              className="btn btn-primary"
            >
              Submit
            </button>
          </div>
        </div>
      </form>
    );
  }
}

export default AddProduct;
