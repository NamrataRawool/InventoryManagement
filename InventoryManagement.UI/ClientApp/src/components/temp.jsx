<form>
  <h4>Product Details</h4>
  <br />
  <div className="form-group row">
    <label htmlFor="productName" className="col-sm-2 col-form-label">
      Name
    </label>
    <div className="col-sm-10">
      <input
        onChange={e => {
          this.setState({
            product: { ...this.state.product, name: e.target.value }
          });
        }}
        type="text"
        className="form-control"
        id="productName"
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
        onChange={e => {
          this.setState({
            product: { ...this.state.product, category: e.target.value }
          });
        }}
        type="text"
        className="form-control"
        id="productCategory"
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
        onChange={e => {
          this.setState({
            product: { ...this.state.product, price: e.target.value }
          });
        }}
        type="text"
        className="form-control"
        id="productPrice"
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
        onChange={e => {
          this.setState({
            product: { ...this.state.product, noOfItems: e.target.value }
          });
        }}
        type="text"
        className="form-control"
        id="productPrice"
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
</form>;
