import React from "react";
import {
  Button,
  Container,
  FormCheck,
  FormControl,
  FormGroup,
  FormLabel,
  Row,
} from "react-bootstrap";
import { useParams } from "react-router-dom";
import FiltroEvEbit from "../../../components/filtroEvEbit/FiltroEvEbit";
import FiltroLiquidezDiariaMinima from "../../../components/filtroLiquidezDiariaMinima/FiltroLiquidezDiariaMinima";
import FiltroMargemEbit from "../../../components/filtroMargemEbit/FiltroMargemEbit";
import FiltroPrecoLucro from "../../../components/filtroPrecoLucro/FiltroPrecoLucro";
import FiltroRiscoMercado from "../../../components/filtroRiscoMercado/FiltroRiscoMercado";

const Greenblatt = () => {
  const { id } = useParams();

  return (
    <div id="frmFormulaGreenblatt" className="mt-3">
      <h3>Filtros:</h3>
      <Container className="mb-1">
        <Row className="justify-content-between">
          <FiltroEvEbit />
          <FiltroPrecoLucro />
          <FiltroMargemEbit />
        </Row>
      </Container>
      <Container className="mb-1">
        <Row className="gap-1">
          <FiltroLiquidezDiariaMinima />
          <FiltroRiscoMercado />
        </Row>
      </Container>
      <Container className="border rounded mb-1">
        <Row className="p-2 justify-content-between">
          <FormCheck
            inline
            className="me-0"
            defaultChecked={true}
            label="Remover Ação em Recuperação Judicial"
            type="switch"
            id="swRecuperacaoJudicial"
            style={{ width: "33%" }}
          />
          <FormCheck
            inline
            className="me-0"
            defaultChecked={true}
            label="Remover Ação com Valor Zero"
            type="switch"
            id="swRemoverValorZero"
            style={{ width: "33%" }}
          />
          <FormCheck
            inline
            className="me-0"
            defaultChecked={true}
            label="Remover Ação com Valor Negativo"
            type="switch"
            id="swRemoverValorNegativo"
            style={{ width: "33%" }}
          />
          <FormCheck
            inline
            className="me-0"
            defaultChecked={true}
            label="Remover Itens de Menor Liquidez"
            type="switch"
            id="swRemoverMenorLiquidez"
            style={{ width: "33%" }}
          />
        </Row>
      </Container>
      <div id="botoesPesquisa">
        <Button className="me-2" variant="success">
          Buscar
        </Button>
        <Button variant="outline-secondary">Limpar</Button>
      </div>
    </div>
  );
};

export default Greenblatt;
