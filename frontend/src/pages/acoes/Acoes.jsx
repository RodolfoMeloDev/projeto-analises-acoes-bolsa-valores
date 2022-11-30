import { useCallback, useEffect, useState } from "react";
import { Form, Row, Table } from "react-bootstrap";

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
  const [campoPesquisa, setCampoPesquisa] = useState("");
  const [itemInicial, setItemInicial] = useState(0);
  const [itemFinal, setItemFinal] = useState(10);

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

      <Table
        className="mt-3 mb-0"
        striped
        bordered
        hover
        responsive
        style={{ minWidth: "max-content" }}
      >
        <thead>
          <tr>
            <th>Ticker</th>
            <th>Empresa</th>
            <th>Setor</th>
            <th>SubSetor</th>
            <th>Segmento</th>
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
      <PaginacaoGrid
        totalRegistros={baseTickersFiltrados.length}
        itemInicial={setItemInicial}
        itemFinal={setItemFinal}
      />
    </>
  );
};

export default Acoes;
