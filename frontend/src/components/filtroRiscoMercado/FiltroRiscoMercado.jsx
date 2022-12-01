import { Badge, FormControl, FormGroup, FormLabel, OverlayTrigger, Tooltip } from "react-bootstrap";

import "./filtroRiscoMercado.css";

const FiltroRiscoMercado = ({ values, setValues }) => {
  const handleInput = (e) => {
    const { value } = e.target;
    setValues({
      ...values,
      marketRisk: value === "" ? null : parseFloat(value),
    });
  };

  return (
    <FormGroup className="border rounded p-2 filtro-risco-mercado">
      <FormLabel className='d-flex justify-content-between'>
        <strong>Risco de Mercado:</strong>
        <OverlayTrigger overlay={
          <Tooltip id="tooltip-riscomercado" className='text-start'>
            O risco de mercado abrange, basicamente, eventuais situações que podem fazer com que as suas aplicações resultem em prejuízos ou, simplesmente, não proporcionem o retorno esperado inicialmente.
            <br/>
            <br/>
            <small>Site: https://ceqef.fgv.br/bancos-de-dados</small>
          </Tooltip>}>
          <Badge bg="dark"> ? </Badge>
        </OverlayTrigger>
      </FormLabel>
      <FormControl
        id="edtRiscoMercado"
        type="number"
        placeholder="Percentual Risco Mercado"
        value={values.marketRisk === null ? "" : values.marketRisk}
        onChange={handleInput}
      />
    </FormGroup>
  );
};

export default FiltroRiscoMercado;
