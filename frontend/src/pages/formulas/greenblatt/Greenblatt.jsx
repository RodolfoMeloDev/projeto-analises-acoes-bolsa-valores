import { useEffect, useState } from "react";
import { Button, Container, Form, Row, Table } from "react-bootstrap";
import { useParams } from "react-router-dom";

import "./greenblatt.css";
import "../../../utils/funcoesFormula";

import FiltroEvEbit from "../../../components/filtroEvEbit/FiltroEvEbit";
import FiltroLiquidezDiariaMinima from "../../../components/filtroLiquidezDiariaMinima/FiltroLiquidezDiariaMinima";
import FiltroMargemEbit from "../../../components/filtroMargemEbit/FiltroMargemEbit";
import FiltroPrecoLucro from "../../../components/filtroPrecoLucro/FiltroPrecoLucro";
import FiltroRiscoMercado from "../../../components/filtroRiscoMercado/FiltroRiscoMercado";
import FiltroSwitches from "../../../components/filtroSwitches/FiltroSwitches";
import PaginacaoGrid from "../../../components/paginacaoGrid/PaginacaoGrid";
import FiltroArquivosImportados from "../../../components/filtroArquivosImportados/FiltroArquivosImportados";
import { getTickersGreenblatt } from "../../../utils/funcoesFormula";

const initialFilters = {
  fileImportId: null,
  description: "",
  minimunEvEbit: null,
  maximumEvEbit: null,
  minimumPriceByProfit: null,
  maximumPriceByProfit: null,
  minimumEbitMargem: null,
  maximumEbitMargem: null,
  minimumLiquidity: null,
  marketRisk: null,
  removeItemsJudicialRecovery: true,
  removeLowerLiquidity: true,
  removeItemsWithZeroValue: true,
  removeItemsWithNegativeValue: true,
};

const Greenblatt = () => {
  const { id } = useParams();

  const [filters, setFilters] = useState(initialFilters);
  const [tickers, setTickres] = useState([]);
  const [tickersGrid, setTickersGrid] = useState([]);
  const [itemInicial, setItemInicial] = useState(0);
  const [itemFinal, setItemFinal] = useState(10);

  const limparFiltros = () => {
    setFilters(initialFilters);
  };

  const handleSubmit = async (e) => {
    const form = e.currentTarget;
    e.preventDefault();

    const responseFormula = await getTickersGreenblatt(filters);

    setTickres(responseFormula);
    setTickersGrid(tickers);

    form.reset();
  };

  useEffect(() => {
    setTickersGrid(tickers.slice(itemInicial, itemFinal));
  }, [itemFinal, itemInicial, tickers]);

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
      <div hidden={tickersGrid.length === 0 ? true : false}>
        <Table
          className="mb-0"
          striped
          bordered
          hover
          responsive
          style={{ minWidth: "max-content" }}
        >
          <thead>
            <tr>
              <th>#</th>
              <th>R.J.</th>
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
          <tbody>
            {tickersGrid.map((ticker) => {
              return (
                <tr>
                  <td>{ticker.position}</td>
                  <td>{ticker.judicialRecovery === false ? "N" : "S"}</td>
                  <td>{ticker.ticker}</td>
                  <td>{ticker.price}</td>
                  <td>{ticker.dividendYield}</td>
                  <td>{ticker.priceByProfit}</td>
                  <td>{ticker.roic}</td>
                  <td>{ticker.evEbit}</td>
                  <td>{ticker.ebitMargin}</td>
                  <td>{ticker.lpa}</td>
                  <td>{ticker.vpa}</td>
                  <td>{ticker.dpa}</td>
                  <td>{ticker.roe}</td>
                  <td>{ticker.payout}</td>
                  <td>{ticker.profitCAGR}</td>
                  <td>{ticker.averageGrowth}</td>
                  <td>{ticker.expectedGrowth}</td>
                  <td>{ticker.averageDailyLiquidity}</td>
                  <td>{ticker.nameSeguiment}</td>
                </tr>
              );
            })}
          </tbody>
        </Table>

        <PaginacaoGrid
          totalRegistros={tickers.length}
          itemInicial={setItemInicial}
          itemFinal={setItemFinal}
        />
      </div>
    </div>
  );
};

export default Greenblatt;
