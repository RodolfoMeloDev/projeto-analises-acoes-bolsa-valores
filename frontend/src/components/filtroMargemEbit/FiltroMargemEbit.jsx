import { FormControl, FormGroup, FormLabel, Row } from "react-bootstrap";

import "./filtroMargemEbit.css";

const FiltroMargemEbit = () => {
  return (
    <FormGroup className="border rounded p-2 filtroMargemEbit">
      <FormLabel>
        <strong>Margem Ebit:</strong>
      </FormLabel>
      <Row className="filtroMargemEbit-row">
        <FormControl
          id="edtMargemEbitMinimo"
          type="number"
          className="ms-2 filtroMargemEbit-row-input"
          placeholder="Valor"
        />
        <span className="filtroMargemEbit-row-span">à</span>
        <FormControl
          id="edtMargemEbitMaximo"
          type="number"
          className="ms-2 me-2 filtroMargemEbit-row-input"
          placeholder="Valor"
        />
      </Row>
    </FormGroup>
  );
};

export default FiltroMargemEbit;
