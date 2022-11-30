import { FormControl, FormGroup, FormLabel, Row } from "react-bootstrap";

import "./filtroMargemEbit.css";

const FiltroMargemEbit = ({ values, setValues }) => {
  const handleInput = (e) => {
    const { id, value } = e.target;
    setValues({
      ...values,
      [id === "edtMargemEbitMinimo"
        ? "minimumEbitMargem"
        : "maximumEbitMargem"]: value === "" ? null : parseFloat(value),
    });
  };

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
          placeholder="Valor Minímo"
          value={
            values.minimumEbitMargem === null ? "" : values.minimumEbitMargem
          }
          onChange={handleInput}
        />
        <span className="filtroMargemEbit-row-span">à</span>
        <FormControl
          id="edtMargemEbitMaximo"
          type="number"
          className="ms-2 me-2 filtroMargemEbit-row-input"
          placeholder="Valor Máximo"
          value={
            values.maximumEbitMargem === null ? "" : values.maximumEbitMargem
          }
          onChange={handleInput}
        />
      </Row>
    </FormGroup>
  );
};

export default FiltroMargemEbit;
