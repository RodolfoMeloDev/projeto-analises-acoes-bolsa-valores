import { FormControl, FormGroup, FormLabel, Row } from "react-bootstrap";

import "./filtroPrecoLucro.css";

const FiltroPrecoLucro = ({ values, setValues }) => {
  const handleInput = (e) => {
    const { id, value } = e.target;
    setValues({
      ...values,
      [id === "edtPrecoLucrotMinimo"
        ? "minimumPriceByProfit"
        : "maximumPriceByProfit"]: value,
    });
  };

  return (
    <FormGroup className="border rounded p-2 filtroPrecoLucro">
      <FormLabel>
        <strong>Preço/Lucro:</strong>
      </FormLabel>
      <Row className="filtroPrecoLucro-row">
        <FormControl
          id="edtPrecoLucrotMinimo"
          type="number"
          className="ms-2 filtroPrecoLucro-row-input"
          placeholder="Valor Minímo"
          value={values.minimumPriceByProfit}
          onChange={handleInput}
        />
        <span className="filtroPrecoLucro-row-span">à</span>
        <FormControl
          id="edtPrecoLucroMaximo"
          type="number"
          className="ms-2 me-2 filtroPrecoLucro-row-input"
          placeholder="Valor Máximo"
          value={values.maximumPriceByProfit}
          onChange={handleInput}
        />
      </Row>
    </FormGroup>
  );
};

export default FiltroPrecoLucro;
