import { Badge, OverlayTrigger, Table, Tooltip } from "react-bootstrap";
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
  colunaGridFormulaPosicao,
  colunaGridFormulaPreco,
  colunaGridFormulaPrecoJusto,
  colunaGridFormulaPrecoLucro,
  colunaGridFormulaRecuperacaoJudicial,
  colunaGridFormulaRoe,
  colunaGridFormulaRoic,
  colunaGridFormulaSegmento,
  colunaGridFormulaTicker,
  colunaGridFormulaVpa,
} from "../../constantes/constantes";
import { retornaLiquidezMediaDiariaTratada } from "../../utils/funcoesFormula";

import "./tabelaFormulas.css";

const TabelaPadrao = ({ header, body, css }) => {
  const criaColunasCabecalho = () => {
    let colunasTabela = [];
    let index = 0;
    header.map((item) => {
      index++;
      return colunasTabela.push(
        <th key={index} className={"coluna-tabela-formula text-center "}>
          {item.tooltip === null ? (
            item.label
          ) : (
            <div className="tabela-cabecalho-tooltip">
              {item.label}
              <OverlayTrigger
                overlay={<Tooltip id="tooltip-evEbit">{item.tooltip}</Tooltip>}
              >
                <Badge bg="dark" className="tabela-tooltip">
                  {" "}
                  ?{" "}
                </Badge>
              </OverlayTrigger>
            </div>
          )}
        </th>
      );
    });

    return colunasTabela;
  };

  const criarLinhasGrid = () => {
    let linhasGrid = [];
    let index = 0;
    body.map((item) => {
      index++;
      return linhasGrid.push(
        <tr key={index}>{criarColunasLinhaGrid(item)}</tr>
      );
    });

    return linhasGrid;
  };

  const criarColunasLinhaGrid = (item) => {
    let colunas = [];
    let index = 0;

    header.map((coluna) => {
      index++;
      let textoColuna = "";
      let css = "text-center";

      switch (coluna.label) {
        case colunaGridFormulaPosicao:
          textoColuna = item.position;
          break;
        case colunaGridFormulaRecuperacaoJudicial:
          textoColuna = item.judicialRecovery === true ? "Sim" : "Não";
          break;
        case colunaGridFormulaTicker:
          textoColuna = item.ticker;
          break;
        case colunaGridFormulaPreco:
          textoColuna = item.price.toLocaleString("pt-br", {
            style: "currency",
            currency: "BRL",
          });
          css = "text-end";
          break;
        case colunaGridFormulaDividendYield:
          textoColuna = item.dividendYield.toLocaleString("pt-br", {
            minimumFractionDigits: 2,
          });
          css = "text-end";
          break;
        case colunaGridFormulaPrecoLucro:
          textoColuna = item.priceByProfit.toLocaleString("pt-br", {
            minimumFractionDigits: 2,
          });
          css = "text-end";
          break;
        case colunaGridFormulaRoic:
          textoColuna = item.roic.toLocaleString("pt-br", {
            minimumFractionDigits: 2,
          });
          css = "text-end";
          break;
        case colunaGridFormulaEvEbit:
          textoColuna = item.evEbit.toLocaleString("pt-br", {
            minimumFractionDigits: 2,
          });
          css = "text-end";
          break;
        case colunaGridFormulaMargemEbit:
          textoColuna = item.ebitMargin.toLocaleString("pt-br", {
            minimumFractionDigits: 2,
          });
          css = "text-end";
          break;
        case colunaGridFormulaLpa:
          textoColuna = item.lpa.toLocaleString("pt-br", {
            minimumFractionDigits: 2,
          });
          css = "text-end";
          break;
        case colunaGridFormulaVpa:
          textoColuna = item.vpa.toLocaleString("pt-br", {
            minimumFractionDigits: 2,
          });
          css = "text-end";
          break;
        case colunaGridFormulaRoe:
          textoColuna = item.roe.toLocaleString("pt-br", {
            minimumFractionDigits: 2,
          });
          css = "text-end";
          break;
        case colunaGridFormulaDpa:
          textoColuna =
            item.dpa === null
              ? ""
              : item.dpa.toLocaleString("pt-br", {
                  minimumFractionDigits: 2,
                });
          css = "text-end";
          break;
        case colunaGridFormulaPayout:
          textoColuna =
            item.payout === null
              ? ""
              : item.payout.toLocaleString("pt-br", {
                  minimumFractionDigits: 2,
                });
          css = "text-end";
          break;
        case colunaGridFormulaLucroCAGR:
          textoColuna =
            item.profitCAGR === null
              ? ""
              : item.profitCAGR.toLocaleString("pt-br", {
                  minimumFractionDigits: 2,
                });
          css = "text-end";
          break;
        case colunaGridFormulaMediaCrescimento:
          textoColuna = item.averageGrowth.toLocaleString("pt-br", {
            minimumFractionDigits: 2,
          });
          css = "text-end";
          break;
        case colunaGridFormulaExpectativaCrescimento:
          textoColuna = item.expectedGrowth.toLocaleString("pt-br", {
            minimumFractionDigits: 2,
          });
          css = "text-end";
          break;
        case colunaGridFormulaLiquidezMediaDiaria:
          textoColuna = retornaLiquidezMediaDiariaTratada(
            item.averageDailyLiquidity
          );
          css = "text-end";
          break;
        case colunaGridFormulaSegmento:
          textoColuna = item.nameSeguiment;
          css = "text-start";
          break;
        case colunaGridFormulaPrecoJusto:
          textoColuna = item.justPrice.toLocaleString("pt-br", {
            style: "currency",
            currency: "BRL",
          });
          css = "text-end";
          break;
        case colunaGridFormulaPercentualDesconto:
          textoColuna = item.discountPercentage.toLocaleString("pt-br", {
            minimumFractionDigits: 2,
          });
          css = "text-end";
          break;
        default:
          break;
      }

      return colunas.push(
        <td key={index} className={"p-2 " + css}>
          {textoColuna}
        </td>
      );
    });

    return colunas;
  };

  return (
    <Table striped bordered hover responsive className={"tabela-padrao " + css}>
      <thead className="tabela-cabecalho">
        <tr>{criaColunasCabecalho()}</tr>
      </thead>
      <tbody>{criarLinhasGrid()}</tbody>
    </Table>
  );
};

export default TabelaPadrao;
