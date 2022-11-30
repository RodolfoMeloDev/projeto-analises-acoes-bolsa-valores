import { Button, Container, Row, } from "react-bootstrap";
import { useParams } from "react-router-dom";

import "./greenblatt.css";

import FiltroEvEbit from "../../../components/filtroEvEbit/FiltroEvEbit";
import FiltroLiquidezDiariaMinima from "../../../components/filtroLiquidezDiariaMinima/FiltroLiquidezDiariaMinima";
import FiltroMargemEbit from "../../../components/filtroMargemEbit/FiltroMargemEbit";
import FiltroPrecoLucro from "../../../components/filtroPrecoLucro/FiltroPrecoLucro";
import FiltroRiscoMercado from "../../../components/filtroRiscoMercado/FiltroRiscoMercado";
import FiltroSwitches from '../../../components/filtroSwitches/FiltroSwitches';
import PaginacaoGrid from "../../../components/paginacaoGrid/PaginacaoGrid";

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
        <Row id="linha-filtro-2">
          <FiltroLiquidezDiariaMinima />
          <FiltroRiscoMercado />
        </Row>
      </Container>
      <Container className="border rounded mb-1">
        <FiltroSwitches/>        
      </Container>
      <div id="botoesPesquisa" className='mb-3'>
        <Button className="me-2" variant="success">
          Buscar
        </Button>
        <Button variant="outline-secondary">Limpar</Button>
      </div>

      <PaginacaoGrid totalRegistros={100} itemInicial={() => {}} itemFinal={() => {}}/>
    </div>
  );
};

export default Greenblatt;
