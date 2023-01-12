import { useEffect, useState } from "react";
import {
  Button,
  Container,
  Form,
  Row,
  Toast,
  ToastContainer,
} from "react-bootstrap";

import "../../../utils/funcoesFormula";

import FiltroEvEbit from "../../../components/filtroEvEbit/FiltroEvEbit";
import FiltroLiquidezDiariaMinima from "../../../components/filtroLiquidezDiariaMinima/FiltroLiquidezDiariaMinima";
import FiltroMargemEbit from "../../../components/filtroMargemEbit/FiltroMargemEbit";
import FiltroPrecoLucro from "../../../components/filtroPrecoLucro/FiltroPrecoLucro";
import FiltroSwitches from "../../../components/filtroSwitches/FiltroSwitches";
import PaginacaoGrid from "../../../components/paginacaoGrid/PaginacaoGrid";
import FiltroArquivosImportados from "../../../components/filtroArquivosImportados/FiltroArquivosImportados";
import { getTickersGraham } from "../../../utils/funcoesFormula";
import TabelaPadrao from "../../../components/tabelaPadrao/TabelaFormulas";
import {
  colunaGridFormulaDividendYield,
  colunaGridFormulaDpa,
  colunaGridFormulaEvEbit,
  colunaGridFormulaExpectativaCrescimento,
  colunaGridFormulaLiquidezMediaDiaria,
  colunaGridFormulaLpa,
  colunaGridFormulaLucroCAGR,
  colunaGridFormulaMargemEbit,
  colunaGridFormulaMediaCrescimento,
  colunaGridFormulaPayout,
  colunaGridFormulaPercentualDesconto,
  colunaGridFormulaPreco,
  colunaGridFormulaPrecoJusto,
  colunaGridFormulaPrecoLucro,
  colunaGridFormulaRecuperacaoJudicial,
  colunaGridFormulaRoe,
  colunaGridFormulaRoic,
  colunaGridFormulaSegmento,
  colunaGridFormulaTicker,
  colunaGridFormulaVpa,
  tooltipTextoDividendYield,
  tooltipTextoDpa,
  tooltipTextoEbitMargem,
  tooltipTextoEvEbit,
  tooltipTextoLiquidezMediaDiaria,
  tooltipTextoLpa,
  tooltipTextoMediaCrescimento,
  tooltipTextoPayout,
  tooltipTextoPrecoLucro,
  tooltipTextoRecuperacaoJudicial,
  tooltipTextoRoe,
  tooltipTextoRoic,
  tooltipTextoVpa,
} from "../../../constantes/constantes";
import { refreshTokenExec } from '../../../utils/funcoesLogin';

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

const cabecalhoTabela = [
  {
    label: colunaGridFormulaRecuperacaoJudicial,
    tooltip: tooltipTextoRecuperacaoJudicial,
  },
  {
    label: colunaGridFormulaTicker,
    tooltip: null,
  },
  {
    label: colunaGridFormulaPreco,
    tooltip: null,
  },
  {
    label: colunaGridFormulaPrecoJusto,
    tooltip: null,
  },
  {
    label: colunaGridFormulaPercentualDesconto,
    tooltip: null,
  },
  {
    label: colunaGridFormulaDividendYield,
    tooltip: tooltipTextoDividendYield,
  },
  {
    label: colunaGridFormulaPrecoLucro,
    tooltip: tooltipTextoPrecoLucro,
  },
  {
    label: colunaGridFormulaRoic,
    tooltip: tooltipTextoRoic,
  },
  {
    label: colunaGridFormulaEvEbit,
    tooltip: tooltipTextoEvEbit,
  },
  {
    label: colunaGridFormulaMargemEbit,
    tooltip: tooltipTextoEbitMargem,
  },
  {
    label: colunaGridFormulaLpa,
    tooltip: tooltipTextoLpa,
  },
  {
    label: colunaGridFormulaVpa,
    tooltip: tooltipTextoVpa,
  },
  {
    label: colunaGridFormulaRoe,
    tooltip: tooltipTextoRoe,
  },
  {
    label: colunaGridFormulaDpa,
    tooltip: tooltipTextoDpa,
  },
  {
    label: colunaGridFormulaPayout,
    tooltip: tooltipTextoPayout,
  },
  {
    label: colunaGridFormulaLucroCAGR,
    tooltip: null,
  },
  {
    label: colunaGridFormulaMediaCrescimento,
    tooltip: tooltipTextoMediaCrescimento,
  },
  {
    label: colunaGridFormulaExpectativaCrescimento,
    tooltip: null,
  },
  {
    label: colunaGridFormulaLiquidezMediaDiaria,
    tooltip: tooltipTextoLiquidezMediaDiaria,
  },
  {
    label: colunaGridFormulaSegmento,
    tooltip: null,
  },
];

const Graham = ({ logout }) => {
  const [filters, setFilters] = useState(initialFilters);
  const [tickers, setTickres] = useState([]);
  const [tickersFiltrados, setTickersFiltrados] = useState([]);
  const [tickersGrid, setTickersGrid] = useState([]);
  const [itemInicial, setItemInicial] = useState(0);
  const [itemFinal, setItemFinal] = useState(10);
  const [show, setShow] = useState(false);
  const [campoPesquisa, setCampoPesquisa] = useState("");

  useEffect(() => {    
    async function atualizaRefreshToken(){
      if (localStorage.getItem("login") === "null" || localStorage.getItem("login") === null)
        return;

      await refreshTokenExec(localStorage.getItem("login"), localStorage.getItem("refreshToken"));
    }

    atualizaRefreshToken();
  },[]);

  const limparFiltros = () => {
    setFilters(initialFilters);
    setTickres([]);
    setCampoPesquisa("");
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (filters.fileImportId === null) {
      setShow(true);
      return;
    }

    const responseFormula = await getTickersGraham(filters);

    if (responseFormula.statusCode === 200) {
      setTickres(responseFormula.dados);
      setTickersFiltrados(tickersFiltrados);
    } else {
      setTickres([]);
      setTickersFiltrados([]);
      logout();
    }
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
      itensFiltrados = itensFiltrados.filter((el) =>
        el.ticker.toLowerCase().includes(campoPesquisa)
      );
    }
    setTickersFiltrados(itensFiltrados);
  }, [campoPesquisa, tickers]);

  return (
    <div id="frmFormulaGraham" className="mt-3">
      <h3>Formula de Graham</h3>
      <Form className="border rounded p-3 mb-3" onSubmit={handleSubmit}>
        <h5>Filtros:</h5>
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
        <Container className="border rounded mb-3">
          <FiltroSwitches values={filters} setValues={setFilters} />
        </Container>

        <div id="botoesPesquisa">
          <Button className="me-2" variant="success" type="submit">
            Buscar
          </Button>
          <Button variant="outline-danger" onClick={limparFiltros}>
            Limpar
          </Button>
        </div>

        <ToastContainer position="middle-center">
          <Toast
            onClose={() => setShow(false)}
            show={show}
            bg={"warning"}
            delay={5000}
            autohide
          >
            <Toast.Header>
              <strong className="me-auto">Mensagem</strong>
            </Toast.Header>
            <Toast.Body>
              Para realizar a busca é necessário selecionar pelo menos o filtro
              de arquivo importado
            </Toast.Body>
          </Toast>
        </ToastContainer>
      </Form>
      <div hidden={tickers.length === 0 ? true : false}>
        <h5>Resultado da Busca:</h5>
        <Form.Control
          className="mb-1"
          type="text"
          placeholder="Filtra por Ticker"
          onChange={handleInput}
          value={campoPesquisa}
        />
        <TabelaPadrao
          header={cabecalhoTabela}
          body={tickersGrid}
          css={"mb-0"}
        />

        <PaginacaoGrid
          totalRegistros={tickersFiltrados.length}
          itemInicial={setItemInicial}
          itemFinal={setItemFinal}
        />
      </div>
    </div>
  );
};

export default Graham;
