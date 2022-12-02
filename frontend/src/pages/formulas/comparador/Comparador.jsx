import { useState } from "react";
import {
  Button,
  Card,
  Container,
  Form,
  FormControl,
  FormGroup,
  FormLabel,
  Row,
  Toast,
  ToastContainer,
} from "react-bootstrap";
import FiltroArquivosImportados from "../../../components/filtroArquivosImportados/FiltroArquivosImportados";
import FiltroRiscoMercado from "../../../components/filtroRiscoMercado/FiltroRiscoMercado";
import FiltroTicker from "../../../components/filtroTicker/FiltroTicker";

import { getTickersCompareFormulas, retornaLiquidezMediaDiariaTratada } from "../../../utils/funcoesFormula";

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

  const limparFiltros = () => {
    setFilters(initialFilters);
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
    console.log(dataTicker);
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
          <Card>
            <Card.Header>
              <strong>Resultado da Busca:</strong>
            </Card.Header>
            <Card.Body>
              <Card className='mb-3'>
                <Card.Header>
                  <b>Informações do Ticker: {dataTicker.ticker} {dataTicker.judicialRecovery === true ? " - Em Recuperação Judicial" : ""}</b>
                </Card.Header>
                <div className='d-flex flex-wrap'>
                  <FormGroup
                    controlId="edtTickerPreco"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Preço</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.price.toLocaleString("pt-br", {
                        style: "currency",
                        currency: "BRL",
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerDY"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>D.Y.</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.dividendYield.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerPL"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Preço/Lucro</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.priceByProfit.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerLpa"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Lpa</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.lpa.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>                
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerVpa"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Vpa</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.vpa.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerVpa"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Dpa</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.dpa === null ? "" : dataTicker.dpa.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerPayout"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Payout</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.payout === null ? "" : dataTicker.payout.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerRoe"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Roe</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.roe.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerRoic"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Roic</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.roic.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerEvEbit"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Ev/Ebit</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.evEbit.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerMargemEbit"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Margem Ebit</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.ebitMargin.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerLucroCAGR"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Lucro CAGR</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.profitCAGR === null ? "" : dataTicker.profitCAGR.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerCrescimentoExperado"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Crescimento Experado</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.expectedGrowth.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerCrescimentoMedio"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Crescimento Médio</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.averageGrowth.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerCrescimentoExperado"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Crescimento Experado</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={dataTicker.expectedGrowth.toLocaleString("pt-br", {
                        minimumFractionDigits: 2,
                      })}
                      disabled
                    ></FormControl>
                  </FormGroup>

                  <FormGroup
                    controlId="edtTickerLiquidezMediaDiaria"
                    className="border rounded m-2 p-2"
                  >
                    <FormLabel>
                      <strong>Liq. Méd. Diária</strong>
                    </FormLabel>
                    <FormControl
                      type="text"
                      value={retornaLiquidezMediaDiariaTratada(dataTicker.averageDailyLiquidity)}
                      disabled
                    ></FormControl>
                  </FormGroup>
                </div>
              </Card>
              <Card>
                <Card.Header>
                  <strong>Resultados das Formulas</strong>
                </Card.Header>
                <div className='d-flex flex-wrap p-2'>
                  <Card>
                    <Card.Header>
                      <strong>Greenblatt</strong>
                    </Card.Header>
                    <Card.Body>
                      <FormGroup controlId='edtTickerPosicaoGreenblatt'>
                        <FormLabel>
                          <strong>Posição</strong>
                        </FormLabel>
                        <FormControl
                          type="text"
                          value={dataTicker.positionGreenBlatt === null ? "" : dataTicker.positionGreenBlatt}
                          disabled
                        ></FormControl>
                      </FormGroup>
                    </Card.Body>
                  </Card>

                  <Card>
                    <Card.Header>
                      <strong>Ev/Ebit</strong>
                    </Card.Header>
                    <Card.Body>
                      <FormGroup controlId='edtTickerPosicaoEvEbit'>
                        <FormLabel>
                          <strong>Posição</strong>
                        </FormLabel>
                        <FormControl
                          type="text"
                          value={dataTicker.positionEvEbit === null ? "" : dataTicker.positionEvEbit}
                          disabled
                        ></FormControl>
                      </FormGroup>
                    </Card.Body>
                  </Card>

                  <Card>
                    <Card.Header>
                      <strong>Preço/Lucro</strong>
                    </Card.Header>
                    <Card.Body>
                      <FormGroup controlId='edtTickerPosicaoPrecoLucro'>
                        <FormLabel>
                          <strong>Posição</strong>
                        </FormLabel>
                        <FormControl
                          type="text"
                          value={dataTicker.positionPriceAndProfit === null ? "" : dataTicker.positionPriceAndProfit}
                          disabled
                        ></FormControl>
                      </FormGroup>
                    </Card.Body>
                  </Card>
                  
                  <Card>
                    <Card.Header>
                      <strong>Bazin</strong>
                    </Card.Header>
                    <Card.Body>
                      <FormGroup controlId='edtTickerPrecoJustoBazin'>
                        <FormLabel>
                          <strong>Preço Justo</strong>
                        </FormLabel>
                        <FormControl
                          type="text"
                          value={dataTicker.justPriceBazin === null ? "" : dataTicker.justPriceBazin.toLocaleString("pt-br", {
                            style: "currency",
                            currency: "BRL",
                            })}
                          disabled
                        ></FormControl>
                      </FormGroup>

                      <FormGroup controlId='edtTickerPercentualDescontoBazin'>
                        <FormLabel>
                          <strong>% Desconto</strong>
                        </FormLabel>
                        <FormControl
                          type="text"
                          value={dataTicker.discountPercentageBazin === null ? "" : dataTicker.discountPercentageBazin.toLocaleString("pt-br", {
                            minimumFractionDigits: 2,
                            })}
                          disabled
                        ></FormControl>
                      </FormGroup>
                    </Card.Body>
                  </Card>

                  <Card>
                    <Card.Header>
                      <strong>Graham</strong>
                    </Card.Header>
                    <Card.Body>
                      <FormGroup controlId='edtTickerPrecoJustoGraham'>
                        <FormLabel>
                          <strong>Preço Justo</strong>
                        </FormLabel>
                        <FormControl
                          type="text"
                          value={dataTicker.justPriceGraham === null ? "" : dataTicker.justPriceGraham.toLocaleString("pt-br", {
                            style: "currency",
                            currency: "BRL",
                            })}
                          disabled
                        ></FormControl>
                      </FormGroup>

                      <FormGroup controlId='edtTickerPercentualDescontoGraham'>
                        <FormLabel>
                          <strong>% Desconto</strong>
                        </FormLabel>
                        <FormControl
                          type="text"
                          value={dataTicker.discountPercentageGraham === null ? "" : dataTicker.discountPercentageGraham.toLocaleString("pt-br", {
                            minimumFractionDigits: 2,
                            })}
                          disabled
                        ></FormControl>
                      </FormGroup>
                    </Card.Body>
                  </Card>

                  <Card>
                    <Card.Header>
                      <strong>Gordon</strong>
                    </Card.Header>
                    <Card.Body>
                      <FormGroup controlId='edtTickerPrecoJustoGordon'>
                        <FormLabel>
                          <strong>Preço Justo</strong>
                        </FormLabel>
                        <FormControl
                          type="text"
                          value={dataTicker.justPriceGordon === null ? "" : dataTicker.justPriceGordon.toLocaleString("pt-br",{
                            style: "currency",
                            currency: "BRL"
                          })}
                          disabled
                        ></FormControl>
                      </FormGroup>

                      <FormGroup controlId='edtTickerPercentualDescontoGordon'>
                        <FormLabel>
                          <strong>% Desconto</strong>
                        </FormLabel>
                        <FormControl
                          type="text"
                          value={dataTicker.discountPercentageGordon === null ? "" : dataTicker.discountPercentageGordon.toLocaleString("pt-br", {
                            minimumFractionDigits: 2,
                            })}
                          disabled
                        ></FormControl>
                      </FormGroup>
                    </Card.Body>
                  </Card>

                </div>
              </Card>
            </Card.Body>
          </Card>          
        </>

      ) : null}
    </div>
  );
};

export default Comparador;
