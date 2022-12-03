import { useState } from "react";
import {
  Accordion,
  Badge,
  Button,
  Card,
  Container,
  Form,
  FormControl,
  FormGroup,
  FormLabel,
  OverlayTrigger,
  Row,
  Toast,
  ToastContainer,
  Tooltip,
} from "react-bootstrap";
import FiltroArquivosImportados from "../../../components/filtroArquivosImportados/FiltroArquivosImportados";
import FiltroRiscoMercado from "../../../components/filtroRiscoMercado/FiltroRiscoMercado";
import FiltroTicker from "../../../components/filtroTicker/FiltroTicker";

import {
  getTickersCompareFormulas,
  retornaLiquidezMediaDiariaTratada,
} from "../../../utils/funcoesFormula";

import "./comparador.css";

import imgLogo from "../../../../src/logo.svg";

const initialFilters = {
  fileImportId: null,
  description: "",
  ticker: "",
  marketRisk: null,
};

const Comparador = () => {
  const [filters, setFilters] = useState(initialFilters);
  const [show, setShow] = useState(false);
  const [dataTicker, setDataTicker] = useState(null);
  const [cardsFormula, setCardsFormula] = useState(null);
  const [cardsInfoAdicionais, setCardsInfoAdicionais] = useState(null);
  const [showAccordion, setShowAccordion] = useState(0);

  const limparFiltros = () => {
    setFilters(initialFilters);
    setCardsFormula(null);
  };

  const preencheCards = (tickers) => {
    setCardsFormula([
      {
        titulo: "Ticker: " + tickers.ticker,
        campos: [
          {
            id: "preco",
            label: "Preço",
            valor: tickers.price.toLocaleString("pt-br", {
              style: "currency",
              currency: "BRL",
            }),
          },
          {
            id: "recuperacaoJudicial",
            label: "Rec. Judicial",
            valor: tickers.judicialRecovery === true ? "SIM" : "NÃO",
          },
        ],
      },
      {
        titulo: "Greenblatt",
        campos: [
          {
            id: "posicaoGreenblatt",
            label: "Posição",
            valor:
              tickers.positionGreenBlatt === null
                ? ""
                : tickers.positionGreenBlatt,
          },
        ],
      },
      {
        titulo: "Ev/Ebit",
        campos: [
          {
            id: "posicaoEvEbit",
            label: "Posição",
            valor:
              tickers.positionEvEbit === null ? "" : tickers.positionEvEbit,
          },
        ],
      },
      {
        titulo: "Preço/Lucro",
        campos: [
          {
            id: "posicaoPrecoLucro",
            label: "Posição",
            valor:
              tickers.positionPriceAndProfit === null
                ? ""
                : tickers.positionPriceAndProfit,
          },
        ],
      },
      {
        titulo: "Bazin",
        campos: [
          {
            id: "posicaoPrecoJustoBazin",
            label: "Preço Justo",
            valor:
              tickers.justPriceBazin === null
                ? ""
                : tickers.justPriceBazin.toLocaleString("pt-br", {
                    style: "currency",
                    currency: "BRL",
                  }),
          },
          {
            id: "descontoBazin",
            label: "% Desconto",
            valor:
              tickers.discountPercentageBazin === null
                ? ""
                : tickers.discountPercentageBazin.toLocaleString("pt-br", {
                    minimumFractionDigits: 2,
                  }),
          },
        ],
      },
      {
        titulo: "Graham",
        campos: [
          {
            id: "posicaoPrecoJustoGraham",
            label: "Preço Justo",
            valor:
              tickers.justPriceGraham === null
                ? ""
                : tickers.justPriceGraham.toLocaleString("pt-br", {
                    style: "currency",
                    currency: "BRL",
                  }),
          },
          {
            id: "descontoGraham",
            label: "% Desconto",
            valor:
              tickers.discountPercentageGraham === null
                ? ""
                : tickers.discountPercentageGraham.toLocaleString("pt-br", {
                    minimumFractionDigits: 2,
                  }),
          },
        ],
      },
      {
        titulo: "Gordon",
        campos: [
          {
            id: "posicaoPrecoJustoGordon",
            label: "Preço Justo",
            valor:
              tickers.justPriceGordon === null
                ? ""
                : tickers.justPriceGordon.toLocaleString("pt-br", {
                    style: "currency",
                    currency: "BRL",
                  }),
          },
          {
            id: "descontoGordon",
            label: "% Desconto",
            valor:
              tickers.discountPercentageGordon === null
                ? ""
                : tickers.discountPercentageGordon.toLocaleString("pt-br", {
                    minimumFractionDigits: 2,
                  }),
          },
        ],
      },
    ]);

    setCardsInfoAdicionais([
      {
        id: "infoDY",
        label: "D.Y",
        valor: tickers.dividendYield,
        tooltip:
          "Indicador utilizado para relacionar os proventos pagos por uma companhia e o preço atual de suas ações.",
      },
      {
        id: "infoPrecoLucro",
        label: "Preço/Lucro",
        valor: tickers.priceByProfit,
        tooltip:
          "Dá uma ideia do quanto o mercado está disposto a pagar pelos lucros da empresa. FORMULA: Preço Atual / LPA (Lucro por Ação).",
      },
      {
        id: "infoEvEbit",
        label: "Ev/Ebit",
        valor: tickers.evEbit,
        tooltip:
          "O EV (Enterprise Value ou Valor da Firma), indica quanto custaria para comprar todos os ativos da companhia, descontando o caixa. Este indicador mostra quanto tempo levaria para o valor calculado no EBIT pagar o investimento feito para compra-la.",
      },
      {
        id: "infoMargemEbit",
        label: "Margem Ebit",
        valor: tickers.ebitMargin,
        tooltip:
          "Útil para comparar a lucratividade operacional de empresas do mesmo segmento, além de contribuir para avaliar o crescimento da eficiência produtiva de um negócio ao longo do tempo. FORMULA: (Ebit/Receita Líquida) x 100",
      },
      {
        id: "infoLpa",
        label: "Lpa",
        valor: tickers.lpa,
        tooltip: "Indica o lucro líquido por ação.",
      },
      {
        id: "infoVpa",
        label: "Vpa",
        valor: tickers.vpa,
        tooltip: "Indica o valor patrimonial por ação.",
      },
      {
        id: "infoDpa",
        label: "Dpa",
        valor:
          tickers.dpa === null
            ? ""
            : tickers.dpa.toLocaleString("pt-br", {
                minimumFractionDigits: 2,
              }),
        tooltip:
          "Dividendo por Ação. FORMULA: (soma de dividendos ao longo de um período / Ações em circulação para o período)",
      },
      {
        id: "infoPayout",
        label: "Payout",
        valor:
          tickers.payout === null
            ? ""
            : tickers.payout.toLocaleString("pt-br", {
                minimumFractionDigits: 2,
              }),
        tooltip:
          "O índice se refere à porcentagem de lucro líquido distribuído aos acionistas, seja via dividendos ou juros sobre capital próprio.",
      },
      {
        id: "infoRoe",
        label: "Roe",
        valor: tickers.roe,
        tooltip:
          "Mede a capacidade de agregar valor de uma empresa a partir de seus próprios recursos e do dinheiro de investidores. FORMULA: (Lucro Líquido / Patrimônio Líquido) x 100",
      },
      {
        id: "infoRoic",
        label: "Roic",
        valor: tickers.roic,
        tooltip:
          "Mede a rentabilidade de dinheiro o que uma empresa é capaz de gerar em razão de todo o capital investido, incluindo os aportes por meio de dívidas. FORMULA: (Ebit-Impostos) / (Patrimônio Líquido + Endividamento)",
      },
      {
        id: "infoLucroCAGR",
        label: "Lucro CAGR",
        valor:
          tickers.profitCAGR === null
            ? ""
            : tickers.profitCAGR.toLocaleString("pt-br", {
                minimumFractionDigits: 2,
              }),
        tooltip: "",
      },
      {
        id: "infoCrescimentoExperado",
        label: "Cres. Experado",
        valor: tickers.expectedGrowth,
        tooltip: "",
      },
      {
        id: "infoCrescimentoMedio",
        label: "Cres. Méd. 5A",
        valor: tickers.averageGrowth,
        tooltip:
          "O CAGR (Compound Annual Growth Rate), ou taxa de crescimento anual composta, é a taxa de retorno necessária para um investimento crescer de seu saldo inicial para o seu saldo final.",
      },
      {
        id: "infoLiquidezMediaDiaria",
        label: "Liq. Diária",
        valor: retornaLiquidezMediaDiariaTratada(tickers.averageDailyLiquidity),
        tooltip: "Média dos últimos 30 dias.",
      },
    ]);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (
      filters.fileImportId === null ||
      filters.marketRisk === null ||
      filters.ticker === ""
    ) {
      setShow(true);
      return;
    }
    const response = await getTickersCompareFormulas(filters);
    setDataTicker(response);
    preencheCards(response);
  };

  const montaCardsResultadoBusca = () => {
    let cardsMontados = [];

    for (let index = 0; index < cardsFormula.length; index++) {
      cardsMontados.push(
        <Card key={index} className="card-formulas">
          <Card.Header>
            <strong>{cardsFormula[index].titulo}</strong>
          </Card.Header>
          <Card.Body>
            {montaCamposCardsResultadoBusca(cardsFormula[index].campos)}
          </Card.Body>
        </Card>
      );
    }

    return cardsMontados;
  };

  const montaCamposCardsResultadoBusca = (campos) => {
    let camposMontados = [];

    for (let index = 0; index < campos.length; index++) {
      camposMontados.push(
        <FormGroup
          key={index}
          controlId={"edt" + campos[index].id}
          className={campos.length !== index + 1 ? "mb-2" : ""}
        >
          <FormLabel>
            <strong>{campos[index].label}</strong>
          </FormLabel>
          <FormControl
            type="text"
            value={campos[index].valor}
            size="sm"
            disabled
          ></FormControl>
        </FormGroup>
      );
    }

    return camposMontados;
  };

  const montaCardsInformacoesAdicionais = () => {
    let cardsMontados = [];

    for (let index = 0; index < cardsInfoAdicionais.length; index++) {
      cardsMontados.push(
        <FormGroup
          key={index}
          controlId={"edt" + cardsInfoAdicionais[index].id}
          className="border rounded p-3 card-formulas"
        >
          <FormLabel className="d-flex justify-content-between">
            <strong>{cardsInfoAdicionais[index].label}</strong>
            {cardsInfoAdicionais[index].tooltip === "" ? null : (
              <OverlayTrigger
                overlay={
                  <Tooltip id={"tooltip-" + cardsInfoAdicionais[index].id}>
                    {cardsInfoAdicionais[index].tooltip}
                  </Tooltip>
                }
              >
                <Badge bg="dark" style={{ height: "20px" }}>
                  {" "}
                  ?{" "}
                </Badge>
              </OverlayTrigger>
            )}
          </FormLabel>
          <FormControl
            type="text"
            value={cardsInfoAdicionais[index].valor}
            size="sm"
            disabled
          ></FormControl>
        </FormGroup>
      );
    }

    return cardsMontados;
  };

  return (
    <div id="frmComparadorFormula" className="mt-3">
      <h3>Comparador de Formulas</h3>
      <Form className="border rounded p-3 mb-3" onSubmit={handleSubmit}>
        <h5>Filtros:</h5>
        <Container className="mb-3">
          <Row>
            <FiltroArquivosImportados values={filters} setValues={setFilters} />
          </Row>
          <Row className="gap-1">
            <FiltroTicker values={filters} setValues={setFilters} />
            <FiltroRiscoMercado values={filters} setValues={setFilters} />
          </Row>
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
              {filters.fileImportId === null
                ? "Para realizar a busca é necessário selecionar pelo menos o filtro de arquivo importado!"
                : filters.ticker === ""
                ? "Selecione o Ticker que deseja realizar a analise!"
                : "Deve ser informado o valor de Risco de Mercado!"}
            </Toast.Body>
          </Toast>
        </ToastContainer>
      </Form>
      {dataTicker !== null ? (
        <>
          <Card className="mb-3">
            <Card.Header>
              <strong>Resultados das Formulas</strong>
            </Card.Header>
            <div className="d-flex flex-wrap p-2 gap-2">
              {montaCardsResultadoBusca()}
            </div>
          </Card>

          <Accordion className="mb-3" defaultActiveKey={0}>
            <Card>
              <Card.Header
                onClick={() => {
                  setShowAccordion(showAccordion === 0 ? 1 : 0);
                }}
                img
              >
                <div className="d-flex justify-content-between">
                  <strong>Informações Adicionais</strong>
                  <img
                    src={imgLogo}
                    width="25"
                    height="25"
                    className={
                      "d-inline-block align-top me-2" + showAccordion === 0
                        ? ""
                        : "teste-imagem"
                    }
                    alt="Login"
                  />
                </div>
              </Card.Header>
              <Accordion.Collapse eventKey={showAccordion}>
                <div className="d-flex flex-wrap gap-2 p-2">
                  {montaCardsInformacoesAdicionais()}
                </div>
              </Accordion.Collapse>
            </Card>
          </Accordion>
        </>
      ) : null}
    </div>
  );
};

export default Comparador;
