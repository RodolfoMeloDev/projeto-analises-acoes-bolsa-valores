import { useEffect, useState } from "react";
import { Badge, Button, Container, Form, OverlayTrigger, Row, Table, Toast, ToastContainer, Tooltip } from "react-bootstrap";

import "./greenblatt.css";
import "../../../utils/funcoesFormula";

import FiltroEvEbit from "../../../components/filtroEvEbit/FiltroEvEbit";
import FiltroLiquidezDiariaMinima from "../../../components/filtroLiquidezDiariaMinima/FiltroLiquidezDiariaMinima";
import FiltroMargemEbit from "../../../components/filtroMargemEbit/FiltroMargemEbit";
import FiltroPrecoLucro from "../../../components/filtroPrecoLucro/FiltroPrecoLucro";
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
  const [filters, setFilters] = useState(initialFilters);
  const [tickers, setTickres] = useState([]);
  const [tickersFiltrados, setTickersFiltrados] = useState([]);
  const [tickersGrid, setTickersGrid] = useState([]);
  const [itemInicial, setItemInicial] = useState(0);
  const [itemFinal, setItemFinal] = useState(10);
  const [show, setShow] = useState(false);
  const [campoPesquisa, setCampoPesquisa] = useState("");

  const limparFiltros = () => {
    setFilters(initialFilters);
    setTickres([]);
    setCampoPesquisa("");
  };

  const handleSubmit = async (e) => {
    const form = e.currentTarget;
    e.preventDefault();

    if (filters.fileImportId === null){
      setShow(true);
      return;
    }

    const responseFormula = await getTickersGreenblatt(filters);

    setTickres(responseFormula);
    setTickersFiltrados(tickers);

    form.reset();
  };

  useEffect(() => {
    setTickersGrid(tickersFiltrados.slice(itemInicial, itemFinal));
  }, [itemFinal, itemInicial, tickersFiltrados]);

  const handleInput = (e) => {
    const { value } = e.target;
    setCampoPesquisa(value);
  };

  useEffect(() => {
    let itensFiltrados = tickers;

    if (campoPesquisa !== "") {
      itensFiltrados = itensFiltrados.filter(
        (el) => el.ticker.toLowerCase().includes(campoPesquisa)
      );
    }
    setTickersFiltrados(itensFiltrados);

  }, [campoPesquisa, tickers])

  return (
    <div id="frmFormulaGreenblatt" className="mt-3">
      <h3>Greenblatt - Filtros:</h3>
      <Form onSubmit={handleSubmit}>
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

        <ToastContainer position="middle-center">
          <Toast onClose={() => setShow(false)} show={show} bg={"warning"} delay={5000} autohide>
            <Toast.Header>
              <strong className="me-auto">Mensagem</strong>
            </Toast.Header>
            <Toast.Body>Para realizar a busca é necessário selecionar pelo menos o filtro de arquivo importado</Toast.Body>
          </Toast>
        </ToastContainer>

      </Form>
      <div hidden={tickers.length === 0 ? true : false}>
        <h5>Resultado da Busca:</h5>
        <Form.Control
          className='mb-1'
          type="text"
          placeholder="Filtra por Ticker"
          onChange={handleInput}     
          value={campoPesquisa} 
        />        
        <Table
          className="mb-0"
          striped
          bordered
          hover
          responsive
          style={{ minWidth: "max-content" }}
        >
          <thead className="tabela-cabecalho">
            <tr>
              <th className="coluna-tabela-formula text-center">#</th>
              <th className="coluna-tabela-formula text-center">
                <div style={{ display: "flex", justifyContent: "space-evenly" }}>
                  R.J. 
                  <OverlayTrigger overlay={
                    <Tooltip id="tooltip-evEbit">Recuperação Judicial</Tooltip>}>
                    <Badge bg="dark"> ? </Badge>
                  </OverlayTrigger>
                </div>
              </th>
              <th className="coluna-tabela-formula text-center">Ticker</th>
              <th className="coluna-tabela-formula text-center">Preço</th>
              <th className="coluna-tabela-formula text-center">D.Y</th>
              <th className="coluna-tabela-formula text-center">P/L</th>
              <th className="coluna-tabela-formula text-center">Roic</th>
              <th className="coluna-tabela-formula text-center">Ev/Ebit</th>
              <th className="coluna-tabela-formula text-center">Margem Ebit</th>
              <th className="coluna-tabela-formula text-center">Lpa</th>
              <th className="coluna-tabela-formula text-center">Vpa</th>
              <th className="coluna-tabela-formula text-center">Roe</th>
              <th className="coluna-tabela-formula text-center">Dpa</th>
              <th className="coluna-tabela-formula text-center">Payout</th>
              <th className="coluna-tabela-formula text-center">Lucro CAGR</th>
              <th className="coluna-tabela-formula text-center">Méd. Cresimento</th>
              <th className="coluna-tabela-formula text-center">Expec. Crescimento</th>
              <th className="coluna-tabela-formula text-center">Liq. Med. Diária</th>
              <th className="coluna-tabela-formula">Segmento</th>
            </tr>
          </thead>
          <tbody>
            {tickersGrid.map((ticker) => {
              let mediaLiquidezDiaria = (ticker.averageDailyLiquidity / 1000.0).toFixed(2);
              let volumeFinanceiro = "K";
              
              if (mediaLiquidezDiaria > 1000 ){
                mediaLiquidezDiaria = (ticker.averageDailyLiquidity / 1000000.0).toFixed(2);
                volumeFinanceiro = "M";
              }
              
              if (mediaLiquidezDiaria > 1000 ){
                mediaLiquidezDiaria = (ticker.averageDailyLiquidity / 1000000000.0).toFixed(2);
                volumeFinanceiro = "B"
              }

              return (
                <tr key={ticker.position}>
                  <td className="text-center p-2">{ticker.position}</td>
                  <td className="text-center p-2">{ticker.judicialRecovery === false ? "N" : "S"}</td>
                  <td className="text-center p-2">{ticker.ticker}</td>
                  <td className="text-end p-2">{ticker.price.toLocaleString('pt-br',{style: 'currency', currency: 'BRL'})}</td>
                  <td className="text-end p-2">{ticker.dividendYield.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{ticker.priceByProfit.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{ticker.roic.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{ticker.evEbit.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{ticker.ebitMargin.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{ticker.lpa.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{ticker.vpa.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{ticker.roe.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{ticker.dpa === null ? "" : ticker.dpa.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{ticker.payout === null ? "" : ticker.payout.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{ticker.profitCAGR === null ? "" : ticker.profitCAGR.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{ticker.averageGrowth.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{ticker.expectedGrowth.toLocaleString('pt-br', {minimumFractionDigits: 2})}</td>
                  <td className="text-end p-2">{mediaLiquidezDiaria + " " + volumeFinanceiro}</td>
                  <td>{ticker.nameSeguiment}</td>
                </tr>
              );
            })}
          </tbody>
        </Table>

        <PaginacaoGrid
          totalRegistros={tickersFiltrados.length}
          itemInicial={setItemInicial}
          itemFinal={setItemFinal}
        />
      </div>
    </div>
  );
};

export default Greenblatt;
