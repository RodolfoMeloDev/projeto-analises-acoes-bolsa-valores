import { FormControl, FormGroup, FormLabel, Row } from "react-bootstrap";

import "./filtroPrecoLucro.css";

const FiltroPrecoLucro = () => {
  return (
    <FormGroup className="border rounded p-2 filtro">
      <FormLabel>
        <strong>Preço/Lucro:</strong>
      </FormLabel>
      <Row className="filtro-row">
        <FormControl
          id="edtPrecoLucrotMinimo"
          type="number"
          className="ms-2 filtro-row-input"
          placeholder="Valor"
        />
        <span className="filtro-row-span">à</span>
        <FormControl
          id="edtPrecoLucroMaximo"
          type="number"
          className="ms-2 me-2 filtro-row-input"
          placeholder="Valor"
        />
      </Row>
    </FormGroup>
  );
};

export default FiltroPrecoLucro;
