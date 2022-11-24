import "./PaginacaoGrid.css";
import { Form, Pagination } from "react-bootstrap";
import { useCallback, useState } from "react";
import { useEffect } from "react";

const PaginacaoGrid = ({ totalRegistros, itemInicial, itemFinal }) => {
  const botoesNoComponente = 5;
  const [paginaAtual, setPaginaAtual] = useState(1);
  const [quantidadeRegistrosGrid, setQuantidadeRegistrosGrid] = useState(10);

  const retornarQuantidadePaginas = useCallback(() => {
    return quantidadeRegistrosGrid === 0
      ? 1
      : Math.trunc(totalRegistros / quantidadeRegistrosGrid) +
          (totalRegistros % quantidadeRegistrosGrid === 0 ? 0 : 1);
  }, [quantidadeRegistrosGrid, totalRegistros]);

  const [totalPaginas, setTotalPaginas] = useState(retornarQuantidadePaginas());

  const mudarQuantidadeRegistrosGrid = () => {
    setQuantidadeRegistrosGrid(
      parseInt(document.getElementById("selQuantidadeRegistros").value)
    );
  };

  useEffect(() => {
    setTotalPaginas(retornarQuantidadePaginas());
    setPaginaAtual(1);
    itemInicial(0);
    itemFinal(
      quantidadeRegistrosGrid === 0 ? totalRegistros : quantidadeRegistrosGrid
    );
  }, [
    itemFinal,
    itemInicial,
    quantidadeRegistrosGrid,
    retornarQuantidadePaginas,
    totalRegistros,
  ]);

  const selectPage = (idComponente) => {
    let page;
    console.log(idComponente);

    switch (idComponente) {
      case "btnFirstPage":
        page = 1;
        break;
      case "btnPreviustPage":
        if (paginaAtual - 1 === 0) return;
        page = paginaAtual - 1;
        break;
      case "btnNextPage":
        if (paginaAtual + 1 > totalPaginas) return;
        page = paginaAtual + 1;
        break;
      case "btnLastPage":
        page = totalPaginas;
        break;
      default:
        page = parseInt(idComponente.substring(7));
        break;
    }

    if (page !== paginaAtual) {
      setPaginaAtual(page);
      itemInicial(page * quantidadeRegistrosGrid - quantidadeRegistrosGrid);
      itemFinal(
        page *
          (quantidadeRegistrosGrid === 0
            ? totalRegistros
            : quantidadeRegistrosGrid)
      );
    }
  };

  const montaBotoes = () => {
    let botoes = [];
    let labelBotal = 0;
    let maisQueCincoBotoes = totalPaginas > botoesNoComponente;
    let botaoAtivo = false;

    if (totalPaginas === 1) {
      botoes.push(
        <Pagination.Item
          id={"btnPage" + totalPaginas}
          className="botao"
          onClick={() => selectPage("btnPage" + totalPaginas)}
          key={totalPaginas}
          active={true}
        >
          {totalPaginas}
        </Pagination.Item>
      );
      return botoes;
    }

    for (let index = 1; index <= totalPaginas; index++) {
      if (maisQueCincoBotoes) {
        if (index === 6) {
          return botoes;
        }

        labelBotal =
          paginaAtual < 4
            ? index
            : totalPaginas - paginaAtual > 1
            ? paginaAtual - 3 + index
            : totalPaginas - 5 + index;

        botaoAtivo =
          ((index <= 3 && totalPaginas - paginaAtual >= 2) ||
            (index > 3 && totalPaginas - paginaAtual < 2)) &&
          paginaAtual === parseInt(labelBotal);

        let idComponente = "btnPage" + labelBotal;
        botoes.push(
          <Pagination.Item
            id={idComponente}
            className="botao"
            onClick={() => selectPage(idComponente)}
            key={labelBotal}
            active={botaoAtivo}
          >
            {labelBotal}
          </Pagination.Item>
        );
      } else {
        botoes.push(
          <Pagination.Item
            id={"btnPage" + index}
            className="botao"
            onClick={() => selectPage("btnPage" + index)}
            key={index}
            active={paginaAtual === index}
          >
            {index}
          </Pagination.Item>
        );
      }
    }

    return botoes;
  };

  const botoesPaginacao = montaBotoes();

  return (
    <div className="d-flex altura-div-paginacao">
      <Form.Select
        className="me-3 tamanho-seletor-quantidade-registros"
        id="selQuantidadeRegistros"
        aria-label="Selecione a quantidade de registros para mostrar no Grid"
        onChange={() => mudarQuantidadeRegistrosGrid()}
      >
        <option value={10}>10</option>
        <option value={20}>20</option>
        <option value={0}>TODOS</option>
      </Form.Select>
      <Pagination>
        <Pagination.First
          id="btnFirstPage"
          className="botao"
          onClick={() => selectPage("btnFirstPage")}
        />
        <Pagination.Prev
          id="btnPreviustPage"
          className="botao"
          onClick={() => selectPage("btnPreviustPage")}
        />
        {botoesPaginacao}
        <Pagination.Next
          id="btnNextPage"
          className="botao"
          onClick={() => selectPage("btnNextPage")}
        />
        <Pagination.Last
          id="btnLastPage"
          className="botao"
          onClick={() => selectPage("btnLastPage")}
        />
      </Pagination>
      <label className="ms-3 label-registros">Total de Registros:</label>
      <label className="ms-1 total-registros"> {totalRegistros}</label>
    </div>
  );
};

export default PaginacaoGrid;
