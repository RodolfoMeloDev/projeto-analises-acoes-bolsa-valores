import { FormControl, FormGroup, FormLabel, Row } from "react-bootstrap";

import "./filtroevebit.css";

const FiltroEvEbit = ({ values, setValues }) => {
  const handleInput = (e) => {
    const { id, value } = e.target;
    setValues({
      ...values,
      [id === "edtEvEbitMinimo" ? "minimunEvEbit" : "maximumEvEbit"]:
        value === "" ? null : parseFloat(value),
    });
  };

  return (
    <FormGroup className="border rounded p-2 filtroEvEbit">
      <FormLabel>
        <strong>Ev/Ebit:</strong>
      </FormLabel>
      <Row className="filtroEvEbit-row">
        <FormControl
          id="edtEvEbitMinimo"
          type="number"
          className="ms-2 filtroEvEbit-row-input"
          placeholder="Valor Minímo"
          value={values.minimunEvEbit === null ? "" : values.minimunEvEbit}
          onChange={handleInput}
        />
        <span className="filtroEvEbit-row-span">à</span>
        <FormControl
          id="edtEvEbitMaximo"
          type="number"
          className="ms-2 me-2 filtroEvEbit-row-input"
          placeholder="Valor Máximo"
          value={values.maximumEvEbit === null ? "" : values.maximumEvEbit}
          onChange={handleInput}
        />
      </Row>
    </FormGroup>
  );
};

export default FiltroEvEbit;
