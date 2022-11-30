import { useState } from "react";
import { Button, Container, Form, Row, Table } from "react-bootstrap";
import { useParams } from "react-router-dom";

import "./greenblatt.css";

import FiltroEvEbit from "../../../components/filtroEvEbit/FiltroEvEbit";
import FiltroLiquidezDiariaMinima from "../../../components/filtroLiquidezDiariaMinima/FiltroLiquidezDiariaMinima";
import FiltroMargemEbit from "../../../components/filtroMargemEbit/FiltroMargemEbit";
import FiltroPrecoLucro from "../../../components/filtroPrecoLucro/FiltroPrecoLucro";
import FiltroRiscoMercado from "../../../components/filtroRiscoMercado/FiltroRiscoMercado";
import FiltroSwitches from "../../../components/filtroSwitches/FiltroSwitches";
import PaginacaoGrid from "../../../components/paginacaoGrid/PaginacaoGrid";
import FiltroArquivosImportados from "../../../components/filtroArquivosImportados/FiltroArquivosImportados";

const initialFilters = {
  fileImportId: "",
  description: "",
  minimunEvEbit: "",
  maximumEvEbit: "",
  minimumPriceByProfit: "",
  maximumPriceByProfit: "",
  minimumEbitMargem: "",
  maximumEbitMargem: "",
  minimumLiquidity: "",
  marketRisk: "",
  removeItemsJudicialRecovery: true,
  removeLowerLiquidity: true,
  removeItemsWithZeroValue: true,
  removeItemsWithNegativeValue: true,
};

const Greenblatt = () => {
  const { id } = useParams();

  const [filters, setFilters] = useState(initialFilters);

  const limparFiltros = () => {
    setFilters(initialFilters);
  };

  const handleSubmit = async (e) => {
    const form = e.currentTarget;
    e.preventDefault();

    form.reset();
  };

  return (
    <div id="frmFormulaGreenblatt" className="mt-3">
      <h3>Greenblatt - Filtros: {id}</h3>
      <Form noValidate onSubmit={handleSubmit}>
        <Container className="mb-1">
          <Row>
            <FiltroArquivosImportados values={filters} setValues={setFilters} />
          </Row>
          <Row className="justify-content-between">
            <FiltroEvEbit values={filters} setValues={setFilters} />
            <FiltroPrecoLucro values={filters} setValues={setFilters} />
            <FiltroMargemEbit values={filters} setValues={setFilters} />
          </Row>
        </Container>
        <Container className="mb-1">
          <Row id="linha-filtro-2">
            <FiltroLiquidezDiariaMinima
              values={filters}
              setValues={setFilters}
            />
            <FiltroRiscoMercado values={filters} setValues={setFilters} />
          </Row>
        </Container>
        <Container className="border rounded mb-1">
          <FiltroSwitches values={filters} setValues={setFilters} />
        </Container>

        <div id="botoesPesquisa" className="mb-3">
          <Button className="me-2" variant="success" type="submit">
            Buscar
          </Button>
          <Button variant="outline-danger" onClick={limparFiltros}>
            Limpar
          </Button>
        </div>
      </Form>

      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>#</th>
            <th>Ticker</th>
            <th>Preço</th>
            <th>D.Y</th>
            <th>P/L</th>
            <th>Roic</th>
            <th>Ev/Ebit</th>
            <th>Margem Ebit</th>
            <th>LPA</th>
            <th>VPA</th>
            <th>DPA</th>
            <th>ROE</th>
            <th>Payout</th>
            <th>Lucro CAGR</th>
            <th>Méd. Cresimento</th>
            <th>Expec. Crescimento</th>
            <th>Liq. Med. Diária</th>
            <th>Segmento</th>
          </tr>
        </thead>
      </Table>

      <PaginacaoGrid
        totalRegistros={100}
        itemInicial={() => {}}
        itemFinal={() => {}}
      />
    </div>
  );
};

export default Greenblatt;
