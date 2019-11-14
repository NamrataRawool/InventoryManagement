import React, { Component } from "react";
import Popup from "reactjs-popup";

class EditProduct extends Component {
  state = {};
  render() {
    return (
      <div className="container">
        <Popup trigger={<button>Trigger</button>} position="top left">
          {close => (
            <div>
              Content here
              <a className="close" onClick={close}>
                &times;
              </a>
            </div>
          )}
        </Popup>
      </div>
    );
  }
}

export default EditProduct;
