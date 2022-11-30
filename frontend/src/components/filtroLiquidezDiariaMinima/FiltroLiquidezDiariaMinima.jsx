import { FormControl, FormGroup, FormLabel } from "react-bootstrap";

import "./filtroLiquidezDiariaMinima.css";

const FiltroLiquidezDiariaMinima = () => {
  return (
    <FormGroup className="border rounded p-2 filtro-liquidez-diaria">
      <FormLabel>
        <strong>Liquidez Diária Miníma:</strong>
      </FormLabel>
      <FormControl
        id="edtLiquidezDiariaMinima"
        type="number"
        placeholder="Liquidez Miníma"
      />
    </FormGroup>
  );
};

export default FiltroLiquidezDiariaMinima;
