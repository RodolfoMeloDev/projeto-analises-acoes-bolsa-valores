import { Badge, FormControl, FormGroup, FormLabel, OverlayTrigger, Row, Tooltip } from "react-bootstrap";

import "./filtroPrecoLucro.css";

const FiltroPrecoLucro = ({ values, setValues }) => {
  const handleInput = (e) => {
    const { id, value } = e.target;
    setValues({
      ...values,
      [id === "edtPrecoLucrotMinimo"
        ? "minimumPriceByProfit"
        : "maximumPriceByProfit"]: value === "" ? null : parseFloat(value),
    });
  };

  return (
    <FormGroup className="border rounded p-2 filtroPrecoLucro">
      <FormLabel className='d-flex justify-content-between'>
        <strong>Preço/Lucro:</strong>
        <OverlayTrigger overlay={
          <Tooltip id="tooltip-precolucro">Dá uma ideia do quanto o mercado está disposto a pagar pelos lucros da empresa.</Tooltip>}>
          <Badge bg="dark"> ? </Badge>
        </OverlayTrigger>
      </FormLabel>
      <Row className="filtroPrecoLucro-row">
        <FormControl
          id="edtPrecoLucrotMinimo"
          type="number"
          className="ms-2 filtroPrecoLucro-row-input"
          placeholder="Valor Minímo"
          value={
            values.minimumPriceByProfit === null
              ? ""
              : values.minimumPriceByProfit
          }
          onChange={handleInput}
        />
        <span className="filtroPrecoLucro-row-span">à</span>
        <FormControl
          id="edtPrecoLucroMaximo"
          type="number"
          className="ms-2 me-2 filtroPrecoLucro-row-input"
          placeholder="Valor Máximo"
          value={
            values.maximumPriceByProfit === null
              ? ""
              : values.maximumPriceByProfit
          }
          onChange={handleInput}
        />
      </Row>
    </FormGroup>
  );
};

export default FiltroPrecoLucro;
