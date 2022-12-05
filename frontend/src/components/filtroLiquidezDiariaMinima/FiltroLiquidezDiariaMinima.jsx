import {
  Badge,
  FormControl,
  FormGroup,
  FormLabel,
  OverlayTrigger,
  Tooltip,
} from "react-bootstrap";

import "./filtroLiquidezDiariaMinima.css";

const FiltroLiquidezDiariaMinima = ({ values, setValues }) => {
  const handleInput = (e) => {
    const { value } = e.target;
    setValues({
      ...values,
      minimumLiquidity: value === "" ? null : parseFloat(value),
    });
  };

  return (
    <FormGroup className="border rounded p-2 filtro-liquidez-diaria">
      <FormLabel className="d-flex justify-content-between">
        <strong>Liquidez Diária Miníma:</strong>
        <OverlayTrigger
          overlay={
            <Tooltip id="tooltip-liquidez-diaria-minima">
              Média dos últimos 30 dias
            </Tooltip>
          }
        >
          <Badge bg="dark" style={{ height: "20px" }}>
            {" "}
            ?{" "}
          </Badge>
        </OverlayTrigger>
      </FormLabel>
      <FormControl
        id="edtLiquidezDiariaMinima"
        type="number"
        placeholder="Liquidez Miníma"
        value={values.minimumLiquidity === null ? "" : values.minimumLiquidity}
        onChange={handleInput}
      />
    </FormGroup>
  );
};

export default FiltroLiquidezDiariaMinima;
