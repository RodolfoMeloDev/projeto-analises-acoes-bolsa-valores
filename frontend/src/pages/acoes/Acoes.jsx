import { useCallback, useEffect, useState } from "react";
import { Form, Pagination, Row, Table } from "react-bootstrap";

import apiBaseTicker from "../../api/baseTicker";
import PaginacaoGrid from "../../components/paginacaoGrid/PaginacaoGrid";

import SelectSegmentos from "../../components/segmentos/SelectSegmentos";
import SelectSetores from "../../components/setores/SelectSetores";
import SelectSubSetores from "../../components/subSetores/SelectSubSetores";

const Acoes = () => {
  const [valueSetor, setValueSetor] = useState(0);
  const [valueSubSetor, setValueSubSetor] = useState(0);
  const [valueSegmento, setValueSegmento] = useState(0);
  const [baseTickers, setBaseTickers] = useState([]);
  const [baseTickersFiltrados, setBaseTickersFiltrados] = useState(baseTickers);
  const [baseTickersGrid, setBaseTickersGrid] = useState(baseTickersFiltrados);

  const [qtdeBotoesPaginacao, setQtdeBotoesPaginacao] = useState(1);
  const [qtdeRegistrosGrid, setQtdeRegistrosGrid] = useState(10);
  const [botoesPaginacao, setBotoesPaginacao] = useState([]);
  const [paginaAtiva, setPaginaAtiva] = useState(1);

  const [campoPesquisa, setCampoPesquisa] = useState("");

  //* ************************************** */
  //Metódos utilizados para filtros do grid
  //* ************************************** */
  const handleInputLogin = (e) => {
    const { value } = e.target;
    setCampoPesquisa(value);
  };

  const getValueSetor = (idComponente) => {
    setValueSetor(document.getElementById(idComponente).value);
  };

  const getValueSubSetor = (idComponente) => {
    setValueSubSetor(document.getElementById(idComponente).value);
  };

  const getValueSegmento = (idComponente) => {
    setValueSegmento(document.getElementById(idComponente).value);
  };

  const alterarQtdeRegistrosGrid = (idComponente) => {
    setQtdeRegistrosGrid(parseInt(document.getElementById(idComponente).value));
  };

  //* ************************************** */
  //Metódos utilizados para criar o componente de paginação
  //* ************************************** */
  const setPage = useCallback(
    (e) => {
      const page = parseInt(e.target.textContent);

      if (page !== paginaAtiva) {
        setPaginaAtiva(page);
      }
    },
    [paginaAtiva]
  );

  const montaBotoes = useCallback(
    (qtdeBotoes) => {
      let botoes = [];
      let labelBotal = 0;
      let maisQueCincoBotoes = qtdeBotoes > 5;
      let botaoAtivo = false;

      if (qtdeBotoes === 1) {
        botoes.push(
          <Pagination.Item
            style={{ width: "40px", textAlign: "center" }}
            onClick={(e) => setPage(e)}
            key={qtdeBotoes}
            active={true}
          >
            {qtdeBotoes}
          </Pagination.Item>
        );
        setBotoesPaginacao(botoes);
        return;
      }

      for (let index = 1; index <= qtdeBotoes; index++) {
        if (maisQueCincoBotoes) {
          if (index === 6) {
            setBotoesPaginacao(botoes);
            break;
          }

          labelBotal =
            paginaAtiva < 4
              ? index
              : qtdeBotoes - paginaAtiva > 1
              ? paginaAtiva - 3 + index
              : qtdeBotoes - 5 + index;

          botaoAtivo =
            ((index <= 3 && qtdeBotoes - paginaAtiva >= 2) ||
              (index > 3 && qtdeBotoes - paginaAtiva < 2)) &&
            paginaAtiva === parseInt(labelBotal);

          botoes.push(
            <Pagination.Item
              style={{ width: "40px", textAlign: "center" }}
              onClick={(e) => setPage(e)}
              key={labelBotal}
              active={botaoAtivo}
            >
              {labelBotal}
            </Pagination.Item>
          );
        } else {
          botoes.push(
            <Pagination.Item
              style={{ width: "40px", textAlign: "center" }}
              onClick={(e) => setPage(e)}
              key={index}
              active={paginaAtiva === index}
            >
              {index}
            </Pagination.Item>
          );
        }
      }

      setBotoesPaginacao(botoes);
    },
    [paginaAtiva, setPage]
  );

  const retornarQtdeBotoes = useCallback(() => {
    return qtdeRegistrosGrid === 0
      ? 1
      : Math.trunc(baseTickersFiltrados.length / qtdeRegistrosGrid) +
          (baseTickersFiltrados.length % qtdeRegistrosGrid === 0 ? 0 : 1);
  }, [baseTickersFiltrados, qtdeRegistrosGrid]);

  //* ************************************** */
  //Metódos utilizados para Páginação do grid
  //* ************************************** */
  const setNextPage = () => {
    const newPage = paginaAtiva + 1;
    const qtdeBotoes = retornarQtdeBotoes();

    if (newPage <= qtdeBotoes) {
      setPaginaAtiva(newPage);
    }

    if (newPage > 8) montaBotoes(qtdeBotoes);
  };

  const setPreviusPage = () => {
    const newPage = paginaAtiva - 1;
    if (newPage > 0) {
      setPaginaAtiva(newPage);
    }
  };

  const setFirstPage = () => {
    setPaginaAtiva(1);
  };

  const setLastPage = () => {
    const page = retornarQtdeBotoes();
    setPaginaAtiva(page);
  };

  //* ************************************** */
  //Metódos para buscar os dados e ordenar o grid
  //* ************************************** */
  const retornarItensOrdenadosePaginado = (
    itens,
    registroInicial,
    registroFinal
  ) => {
    return itens
      .sort((a, b) => {
        let itemA = a.baseTicker.toLowerCase(),
          itemB = b.baseTicker.toLowerCase();

        if (itemA < itemB) return -1;

        if (itemA > itemB) return 1;

        return 0;
      })
      .slice(registroInicial, registroFinal);
  };

  const getBaseTicker = async () => {
    const response = await apiBaseTicker.get("Complete");

    if (response.status === 200) {
      return response.data;
    }
  };

  const dataBaseTickers = useCallback(async () => {
    const baseTickers = await getBaseTicker();
    setBaseTickers(baseTickers);
    setBaseTickersFiltrados(baseTickers);
  }, []);

  useEffect(() => {
    dataBaseTickers();
  }, [dataBaseTickers]);

  //* ************************************** */
  // Efeitos comportamentais da tela
  //* ************************************** */

  useEffect(() => {
    setValueSubSetor(0);
  }, [valueSetor]);

  useEffect(() => {
    setValueSegmento(0);
  }, [valueSubSetor]);

  useEffect(() => {
    setQtdeBotoesPaginacao(
      parseInt(qtdeRegistrosGrid) === 0 ? 1 : retornarQtdeBotoes()
    );
    setPaginaAtiva(1);
  }, [baseTickersFiltrados, qtdeRegistrosGrid, retornarQtdeBotoes]);

  useEffect(() => {
    montaBotoes(qtdeBotoesPaginacao);
  }, [montaBotoes, qtdeBotoesPaginacao]);

  useEffect(() => {
    const registroInicial = paginaAtiva * qtdeRegistrosGrid - qtdeRegistrosGrid,
      registroFinal =
        paginaAtiva *
        (qtdeRegistrosGrid === 0
          ? baseTickersFiltrados.length
          : qtdeRegistrosGrid);

    montaBotoes(retornarQtdeBotoes());
    setBaseTickersGrid(
      retornarItensOrdenadosePaginado(
        baseTickersFiltrados,
        registroInicial,
        registroFinal
      )
    );
  }, [
    baseTickersFiltrados,
    montaBotoes,
    paginaAtiva,
    qtdeRegistrosGrid,
    retornarQtdeBotoes,
  ]);

  useEffect(() => {
    let itensFiltrados = baseTickers;

    if (campoPesquisa !== "") {
      itensFiltrados = itensFiltrados.filter(
        (el) =>
          el.baseTicker.toLowerCase().includes(campoPesquisa) ||
          el.company.toLowerCase().includes(campoPesquisa)
      );
    }

    if (parseInt(valueSegmento) > 0)
      itensFiltrados = itensFiltrados.filter(
        (el) => el.segmentId === parseInt(valueSegmento)
      );
    else if (parseInt(valueSubSetor) > 0)
      itensFiltrados = itensFiltrados.filter(
        (el) => el.segment.subSector.id === parseInt(valueSubSetor)
      );
    else if (parseInt(valueSetor) > 0)
      itensFiltrados = itensFiltrados.filter(
        (el) => el.segment.subSector.sector.id === parseInt(valueSetor)
      );

    setBaseTickersFiltrados(itensFiltrados);
  }, [campoPesquisa, valueSegmento, valueSubSetor, valueSetor, baseTickers]);

  const [itemInicial, setItemInicial] = useState(0);
  const [itemFinal, setItemFinal] = useState(10);

  useEffect(() => {
    setBaseTickersGrid(
      retornarItensOrdenadosePaginado(
        baseTickersFiltrados,
        itemInicial,
        itemFinal
      )
    );
  }, [baseTickersFiltrados, itemFinal, itemInicial]);

  return (
    <>
      <div id="filtros" className="mt-3">
        <h3>Filtros:</h3>
        <Row>
          <SelectSetores
            idSelect="selSetor"
            tamanhoSelect="4"
            getValue={getValueSetor}
          />
          <SelectSubSetores
            idSelect="selSubSetor"
            tamanhoSelect="4"
            getValue={getValueSubSetor}
            valueLink={valueSetor}
          />
          <SelectSegmentos
            idSelect="selSegmento"
            tamanhoSelect="4"
            getValue={getValueSegmento}
            valueLink={valueSubSetor}
          />
        </Row>
        <Row>
          <Form.Group className="mt-3" controlId="edtBuscaDados">
            <Form.Label>
              <strong>Busca por Ticker/Empresa</strong>
            </Form.Label>
            <Form.Control
              type="text"
              placeholder="Filtra os dados das colunas"
              onChange={handleInputLogin}
            />
          </Form.Group>
        </Row>
      </div>
      <Table className="mt-3" striped bordered hover responsive>
        <thead>
          <tr>
            <th style={{ width: "5%" }}>Ticker</th>
            <th style={{ width: "25%" }}>Empresa</th>
            <th style={{ width: "25%" }}>Setor</th>
            <th style={{ width: "25%" }}>SubSetor</th>
            <th style={{ width: "25%" }}>Segmento</th>
          </tr>
        </thead>
        <tbody>
          {baseTickersGrid.map((ticker) => {
            return (
              <tr key={ticker.id}>
                <td>{ticker.baseTicker}</td>
                <td>{ticker.company}</td>
                <td>{ticker.segment.subSector.sector.name}</td>
                <td>{ticker.segment.subSector.name}</td>
                <td>{ticker.segment.name}</td>
              </tr>
            );
          })}
        </tbody>
      </Table>

      {/* <div className="d-flex" style={{ height: "38px" }}>
        <Form.Select
          className="me-3"
          style={{ width: "110px", textAlign: "center" }}
          id="qtdeRegistros"
          aria-label="Selecione a quantidade de registros para mostrar no Grid"
          onChange={() => alterarQtdeRegistrosGrid("qtdeRegistros")}
        >
          <option value={10}>10</option>
          <option value={20}>20</option>
          <option value={0}>TODOS</option>
        </Form.Select>
        <Pagination>
          <Pagination.First
            style={{ width: "40px", textAlign: "center" }}
            onClick={setFirstPage}
          />
          <Pagination.Prev
            style={{ width: "40px", textAlign: "center" }}
            onClick={setPreviusPage}
          />
          {botoesPaginacao}
          <Pagination.Next
            style={{ width: "40px", textAlign: "center" }}
            onClick={setNextPage}
          />
          <Pagination.Last
            style={{ width: "40px", textAlign: "center" }}
            onClick={setLastPage}
          />
        </Pagination>
        <label className="ms-3 pt-3 ">
          <b>Total de Registros: {baseTickersFiltrados.length}</b>
        </label>
      </div> */}
      <PaginacaoGrid
        totalRegistros={baseTickersFiltrados.length}
        itemInicial={setItemInicial}
        itemFinal={setItemFinal}
      />
    </>
  );
};

export default Acoes;
