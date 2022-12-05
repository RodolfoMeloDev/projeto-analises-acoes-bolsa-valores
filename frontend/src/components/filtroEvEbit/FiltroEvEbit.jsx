import {
  Badge,
  FormControl,
  FormGroup,
  FormLabel,
  OverlayTrigger,
  Row,
  Tooltip,
} from "react-bootstrap";

import "./filtroevebit.css";

const FiltroEvEbit = ({ values, setValues }) => {
  const handleInput = (e) => {
    const { id, value } = e.target;
    setValues({
      ...values,
      [id === "edtEvEbitMinimo" ? "minimunEvEbit" : "maximumEvEbit"]:
        value === "" ? null : parseFloat(value),
    });
  };

  return (
    <FormGroup className="border rounded p-2 filtroEvEbit">
      <FormLabel className="d-flex justify-content-between">
        <strong>Ev/Ebit:</strong>
        <OverlayTrigger
          overlay={
            <Tooltip id="tooltip-evEbit">
              O EV (Enterprise Value ou Valor da Firma), indica quanto custaria
              para comprar todos os ativos da companhia, descontando o caixa.
              Este indicador mostra quanto tempo levaria para o valor calculado
              no EBIT pagar o investimento feito para compra-la.
            </Tooltip>
          }
        >
          <Badge bg="dark" style={{ height: "20px" }}>
            {" "}
            ?{" "}
          </Badge>
        </OverlayTrigger>
      </FormLabel>
      <Row className="filtroEvEbit-row">
        <FormControl
          id="edtEvEbitMinimo"
          type="number"
          className="ms-2 filtroEvEbit-row-input"
          placeholder="Valor Minímo"
          value={values.minimunEvEbit === null ? "" : values.minimunEvEbit}
          onChange={handleInput}
        />
        <span className="filtroEvEbit-row-span">à</span>
        <FormControl
          id="edtEvEbitMaximo"
          type="number"
          className="ms-2 me-2 filtroEvEbit-row-input"
          placeholder="Valor Máximo"
          value={values.maximumEvEbit === null ? "" : values.maximumEvEbit}
          onChange={handleInput}
        />
      </Row>
    </FormGroup>
  );
};

export default FiltroEvEbit;
