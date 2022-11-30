import { FormControl, FormGroup, FormLabel } from "react-bootstrap";

import "./filtroLiquidezDiariaMinima.css";

const FiltroLiquidezDiariaMinima = ({ values, setValues }) => {
  const handleInput = (e) => {
    const { value } = e.target;
    setValues({
      ...values,
      minimumLiquidity: value,
    });
  };

  return (
    <FormGroup className="border rounded p-2 filtro-liquidez-diaria">
      <FormLabel>
        <strong>Liquidez Diária Miníma:</strong>
      </FormLabel>
      <FormControl
        id="edtLiquidezDiariaMinima"
        type="number"
        placeholder="Liquidez Miníma"
        value={values.minimumLiquidity}
        onChange={handleInput}
      />
    </FormGroup>
  );
};

export default FiltroLiquidezDiariaMinima;
