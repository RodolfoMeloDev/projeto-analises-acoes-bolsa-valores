import { Badge, FormControl, FormGroup, FormLabel, OverlayTrigger, Row, Tooltip } from "react-bootstrap";

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
      <FormLabel className='d-flex justify-content-between'>
        <strong>Margem Ebit:</strong>
        <OverlayTrigger overlay={
          <Tooltip id="tooltip-margemEbit">Útil para comparar a lucratividade operacional de empresas do mesmo segmento, além de contribuir para avaliar o crescimento da eficiência produtiva de um negócio ao longo do tempo.
          </Tooltip>}>
          <Badge bg="dark"> ? </Badge>
        </OverlayTrigger>
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
