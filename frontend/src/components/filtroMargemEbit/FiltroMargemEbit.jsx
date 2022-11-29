import { FormControl, FormGroup, FormLabel, Row } from "react-bootstrap";

import "./filtroMargemEbit.css";

const FiltroMargemEbit = () => {
  return (
    <FormGroup className="border rounded p-2 filtro">
      <FormLabel>
        <strong>Margem Ebit:</strong>
      </FormLabel>
      <Row className="filtro-row">
        <FormControl
          id="edtMargemEbitMinimo"
          type="number"
          className="ms-2 filtro-row-input"
          placeholder="Valor"
        />
        <span className="filtro-row-span">à</span>
        <FormControl
          id="edtMargemEbitMaximo"
          type="number"
          className="ms-2 me-2 filtro-row-input"
          placeholder="Valor"
        />
      </Row>
    </FormGroup>
  );
};

export default FiltroMargemEbit;
