import { FormCheck, Row } from "react-bootstrap";

import "./filtroSwitches.css";

const FiltroSwitches = () => {
  return (
    <Row className="p-2 justify-content-between">
			<FormCheck
				inline
				className="me-0 formcheck"
				defaultChecked={true}
				label="Remover Ação em Recuperação Judicial"
				type="switch"
				id="swRecuperacaoJudicial"
			/>
			<FormCheck
				inline
				className="me-0 formcheck"
				defaultChecked={true}
				label="Remover Ação com Valor Zero"
				type="switch"
				id="swRemoverValorZero"
			/>
			<FormCheck
				inline
				className="me-0 formcheck"
				defaultChecked={true}
				label="Remover Ação com Valor Negativo"
				type="switch"
				id="swRemoverValorNegativo"
			/>
			<FormCheck
				inline
				className="me-0 formcheck"
				defaultChecked={true}
				label="Remover Itens de Menor Liquidez"
				type="switch"
				id="swRemoverMenorLiquidez"
			/>
		</Row>
  );
};

export default FiltroSwitches;
