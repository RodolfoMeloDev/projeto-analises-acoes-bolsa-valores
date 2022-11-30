import { FormControl, FormGroup, FormLabel } from "react-bootstrap";

import "./filtroRiscoMercado.css";

const FiltroRiscoMercado = ({ values, setValues }) => {
  const handleInput = (e) => {
    const { value } = e.target;
    setValues({
      ...values,
      marketRisk: value,
    });
  };

  return (
    <FormGroup className="border rounded p-2 filtro-risco-mercado">
      <FormLabel>
        <strong>Risco de Mercado:</strong>
      </FormLabel>
      <FormControl
        id="edtRiscoMercado"
        type="number"
        placeholder="Percentual Risco Mercado"
        value={values.marketRisk}
        onChange={handleInput}
      />
    </FormGroup>
  );
};

export default FiltroRiscoMercado;
