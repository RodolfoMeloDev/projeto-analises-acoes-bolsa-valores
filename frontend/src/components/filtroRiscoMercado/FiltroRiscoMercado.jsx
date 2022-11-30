import { FormControl, FormGroup, FormLabel } from "react-bootstrap";

import "./filtroRiscoMercado.css";

const FiltroRiscoMercado = () => {
  return (
    <FormGroup className="border rounded p-2 filtro-risco-mercado">
      <FormLabel>
        <strong>Risco de Mercado:</strong>
      </FormLabel>
      <FormControl
        id="edtRiscoMercado"
        type="number"
        placeholder="Percentual Risco Mercado"
      />
    </FormGroup>
  );
};

export default FiltroRiscoMercado;
