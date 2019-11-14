import React, { Component } from "react";
import "../../styles/addproduct.css";
import Axios from "axios";
import Popup from "reactjs-popup";

class AddProduct extends Component {
  state = {
    product: {
      name: "",
      category: "",
      price: 0,
      noOfItems: 0
    },
    errors: {
      name: "error",
      category: "error",
      price: "error",
      noOfItems: "error"
    }
  };

  validateString = str => {
    const error = new RegExp("^([A-Za-z]+)(\\s[A-Za-z]+)*\\s?$").test(str)
      ? ""
      : "Please eneter string value";
    console.log(error);
    return error;
  };

  validateInteger = num => {
    const error = new RegExp("^[1-9]+[0-9]*$").test(num)
      ? ""
      : "Please eneter integer value";
    console.log(error);
    return error;
  };

  handleChange = event => {
    event.preventDefault();
    const { id, value } = event.target;
    let errors = this.state.errors;
    switch (id) {
      case "name":
        errors.name = this.validateString(value);
        break;
      case "category":
        errors.category = this.validateString(value);
        break;
      case "price":
        errors.price = this.validateInteger(value);
        break;
      case "noOfItems":
        errors.noOfItems = this.validateInteger(value);
        break;
      default:
        console.log("invalid id");
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
      <div className="popup">
        <div className="box-header">
          <h3 className="box-title">Add Product</h3>
        </div>
        <form>
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
            <label
              htmlFor="productCategory"
              className="col-sm-2 col-form-label"
            >
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
                disabled={!this.validateForm(this.state.errors)}
              >
                Submit
              </button>
            </div>
          </div>
        </form>
      </div>
    );
  }
}

export default AddProduct;
