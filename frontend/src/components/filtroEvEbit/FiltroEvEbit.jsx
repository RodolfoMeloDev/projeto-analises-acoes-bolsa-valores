import { FormControl, FormGroup, FormLabel, Row } from "react-bootstrap";

import "./filtroevebit.css";

const FiltroEvEbit = () => {
  return (
    <FormGroup className="border rounded p-2 filtro">
      <FormLabel>
        <strong>Ev/Ebit:</strong>
      </FormLabel>
      <Row className="filtro-row">
        <FormControl
          id="edtEvEbitMinimo"
          type="number"
          className="ms-2 filtro-row-input"
          placeholder="Valor"
        />
        <span className="filtro-row-span">à</span>
        <FormControl
          id="edtEvEbitMaximo"
          type="number"
          className="ms-2 me-2 filtro-row-input"
          placeholder="Valor"
        />
      </Row>
    </FormGroup>
  );
};

export default FiltroEvEbit;
