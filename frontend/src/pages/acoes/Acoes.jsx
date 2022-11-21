import { useCallback, useEffect, useState } from "react";
import { Form, Pagination, Row, Table } from "react-bootstrap";

import apiBaseTicker from "../../api/baseTicker";

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

  const [qtdeRegistrosGrid, setQtdeRegistrosGrid] = useState(10);
  const [botoesPaginacao, setBotoesPaginacao] = useState([]);
  const [paginaAtiva, setPaginaAtiva] = useState(1);

  const [campoPesquisa, setCampoPesquisa] = useState("");

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

  useEffect(() => {
    setValueSubSetor(0);
    setBaseTickersFiltrados(
      getTickersFiltered(valueSetor, valueSubSetor, valueSegmento)
    );
  }, [valueSetor]);

  useEffect(() => {
    setValueSegmento(0);
    setBaseTickersFiltrados(
      getTickersFiltered(valueSetor, valueSubSetor, valueSegmento)
    );
  }, [valueSubSetor]);

  useEffect(() => {
    setBaseTickersFiltrados(
      getTickersFiltered(valueSetor, valueSubSetor, valueSegmento)
    );
  }, [valueSegmento]);

  const getTickersFiltered = (setor, subSetor, segmento) => {
    let listaBaseTickres = baseTickers;

    if (parseInt(segmento) > 0) {
      listaBaseTickres = listaBaseTickres.filter(
        (el) => el.segmentId === parseInt(segmento)
      );
    } else if (parseInt(subSetor) > 0) {
      listaBaseTickres = listaBaseTickres.filter(
        (el) => el.segment.subSector.id === parseInt(subSetor)
      );
    } else if (parseInt(setor) > 0) {
      listaBaseTickres = listaBaseTickres.filter(
        (el) => el.segment.subSector.sector.id === parseInt(setor)
      );
    }

    if (campoPesquisa !== "") {
      listaBaseTickres = listaBaseTickres.filter(
        (el) =>
          el.baseTicker.toLowerCase().includes(campoPesquisa) ||
          el.company.toLowerCase().includes(campoPesquisa)
      );
    }

    return listaBaseTickres;
  };

  const montaBotoes = (qtdeBotoes) => {
    let botoes = [];
    let labelBotal = 0;

    console.log(qtdeBotoes);

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

    for (let index = 0; index < qtdeBotoes; index++) {
      if (qtdeBotoes > 5) {
        if (index === 5) {
          setBotoesPaginacao(botoes);
          break;
        }

        labelBotal = index + 1 + (paginaAtiva <= 5 ? 0 : paginaAtiva - 5);
        botoes.push(
          <Pagination.Item
            style={{ width: "40px", textAlign: "center" }}
            onClick={(e) => setPage(e)}
            key={labelBotal}
            active={paginaAtiva === parseInt(labelBotal)}
          >
            {labelBotal}
          </Pagination.Item>
        );
      } else {
        botoes.push(
          <Pagination.Item
            style={{ width: "40px", textAlign: "center" }}
            onClick={(e) => setPage(e)}
            key={index + 1}
            active={paginaAtiva === index + 1}
          >
            {index + 1}
          </Pagination.Item>
        );
      }
    }

    setBotoesPaginacao(botoes);
  };

  useEffect(() => {
    montaBotoes(
      parseInt(qtdeRegistrosGrid) === 0
        ? 1
        : Math.trunc(baseTickersFiltrados.length / qtdeRegistrosGrid) +
            (baseTickersFiltrados.length % qtdeRegistrosGrid === 0 ? 0 : 1)
    );
    setPaginaAtiva(1);
    setBaseTickersGrid(
      baseTickersFiltrados
        .sort((a, b) => {
          let fa = a.baseTicker.toLowerCase(),
            fb = b.baseTicker.toLowerCase();

          if (fa < fb) {
            return -1;
          }
          if (fa > fb) {
            return 1;
          }
          return 0;
        })
        .slice(
          0,
          parseInt(qtdeRegistrosGrid) === 0
            ? baseTickersFiltrados.length
            : qtdeRegistrosGrid
        )
    );
  }, [baseTickersFiltrados, qtdeRegistrosGrid]);

  const setNextPage = () => {
    const newPage = paginaAtiva + 1;
    const qtdeBotoes =
      Math.trunc(baseTickersFiltrados.length / qtdeRegistrosGrid) +
      (baseTickersFiltrados.length % qtdeRegistrosGrid === 0 ? 0 : 1);

    if (newPage <= qtdeBotoes) {
      setPaginaAtiva(newPage);
      setBaseTickersGrid(
        baseTickersFiltrados
          .sort((a, b) => {
            let fa = a.baseTicker.toLowerCase(),
              fb = b.baseTicker.toLowerCase();

            if (fa < fb) {
              return -1;
            }
            if (fa > fb) {
              return 1;
            }
            return 0;
          })
          .slice(
            newPage * qtdeRegistrosGrid - qtdeRegistrosGrid,
            newPage * qtdeRegistrosGrid
          )
      );
    }

    if (newPage > 8) montaBotoes(qtdeBotoes);
  };

  const setPreviusPage = () => {
    const newPage = paginaAtiva - 1;
    if (newPage > 0) {
      setPaginaAtiva(newPage);
      setBaseTickersGrid(
        baseTickersFiltrados
          .sort((a, b) => {
            let fa = a.baseTicker.toLowerCase(),
              fb = b.baseTicker.toLowerCase();

            if (fa < fb) {
              return -1;
            }
            if (fa > fb) {
              return 1;
            }
            return 0;
          })
          .slice(
            newPage * qtdeRegistrosGrid - qtdeRegistrosGrid,
            newPage * qtdeRegistrosGrid
          )
      );
    }
  };

  const setFirstPage = () => {
    setPaginaAtiva(1);
    setBaseTickersGrid(
      baseTickersFiltrados
        .sort((a, b) => {
          let fa = a.baseTicker.toLowerCase(),
            fb = b.baseTicker.toLowerCase();

          if (fa < fb) {
            return -1;
          }
          if (fa > fb) {
            return 1;
          }
          return 0;
        })
        .slice(0, qtdeRegistrosGrid)
    );
  };

  const setLastPage = () => {
    const page =
      Math.trunc(baseTickersFiltrados.length / qtdeRegistrosGrid) +
      (baseTickersFiltrados.length % qtdeRegistrosGrid === 0 ? 0 : 1);
    setPaginaAtiva(page);
    setBaseTickersGrid(
      baseTickersFiltrados
        .sort((a, b) => {
          let fa = a.baseTicker.toLowerCase(),
            fb = b.baseTicker.toLowerCase();

          if (fa < fb) {
            return -1;
          }
          if (fa > fb) {
            return 1;
          }
          return 0;
        })
        .slice((page - 1) * qtdeRegistrosGrid)
    );
  };

  const setPage = (e) => {
    const page = parseInt(e.target.textContent);

    if (page !== paginaAtiva) {
      setPaginaAtiva(page);
      setBaseTickersGrid(
        baseTickersFiltrados
          .sort((a, b) => {
            let fa = a.baseTicker.toLowerCase(),
              fb = b.baseTicker.toLowerCase();

            if (fa < fb) {
              return -1;
            }
            if (fa > fb) {
              return 1;
            }
            return 0;
          })
          .slice(
            page * qtdeRegistrosGrid - qtdeRegistrosGrid,
            page * qtdeRegistrosGrid
          )
      );
    }
  };

  useEffect(() => {
    montaBotoes(
      Math.trunc(baseTickersFiltrados.length / qtdeRegistrosGrid) +
        (baseTickersFiltrados.length % qtdeRegistrosGrid === 0 ? 0 : 1)
    );
  }, [paginaAtiva]);

  const alterarQtdeRegistrosGrid = (idComponente) => {
    setQtdeRegistrosGrid(document.getElementById(idComponente).value);
  };

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
  }, [campoPesquisa]);

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

      <div className="d-flex" style={{ height: "38px" }}>
        <Form.Select
          className="me-3"
          style={{ width: "100px", textAlign: "center" }}
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
        <lable className="ms-3 pt-3 ">
          <b>Total de Registros: {baseTickersFiltrados.length}</b>
        </lable>
      </div>
    </>
  );
};

export default Acoes;
