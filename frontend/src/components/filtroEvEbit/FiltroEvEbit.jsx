import { FormControl, FormGroup, FormLabel, Row } from "react-bootstrap";

import "./filtroevebit.css";

const FiltroEvEbit = () => {
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
          placeholder="Valor"
        />
        <span className="filtroEvEbit-row-span">à</span>
        <FormControl
          id="edtEvEbitMaximo"
          type="number"
          className="ms-2 me-2 filtroEvEbit-row-input"
          placeholder="Valor"
        />
      </Row>
    </FormGroup>
  );
};

export default FiltroEvEbit;
