import { FormCheck, Row } from "react-bootstrap";

import "./filtroSwitches.css";

const FiltroSwitches = ({ values, setValues }) => {
  const handleChecked = (e) => {
    const { id, checked } = e.target;
    setValues({
      ...values,
      [id === "swRecuperacaoJudicial"
        ? "removeItemsJudicialRecovery"
        : id === "swRemoverValorZero"
        ? "removeItemsWithZeroValue"
        : id === "swRemoverValorNegativo"
        ? "removeItemsWithNegativeValue"
        : "removeLowerLiquidity"]: checked,
    });
  };

  return (
    <Row className="p-2 justify-content-between">
      <FormCheck
        inline
        className="me-0 formcheck"
        label="Remover Ação em Recuperação Judicial"
        type="switch"
        id="swRecuperacaoJudicial"
        checked={values.removeItemsJudicialRecovery}
        onChange={handleChecked}
      />
      <FormCheck
        inline
        className="me-0 formcheck"
        label="Remover Ação com Valor Zero"
        type="switch"
        id="swRemoverValorZero"
        checked={values.removeItemsWithZeroValue}
        onChange={handleChecked}
      />
      <FormCheck
        inline
        className="me-0 formcheck"
        label="Remover Ação com Valor Negativo"
        type="switch"
        id="swRemoverValorNegativo"
        checked={values.removeItemsWithNegativeValue}
        onChange={handleChecked}
      />
      <FormCheck
        inline
        className="me-0 formcheck"
        label="Remover Itens de Menor Liquidez"
        type="switch"
        id="swRemoverMenorLiquidez"
        checked={values.removeLowerLiquidity}
        onChange={handleChecked}
      />
    </Row>
  );
};

export default FiltroSwitches;
