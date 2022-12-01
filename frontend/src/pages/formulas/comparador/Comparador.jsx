import { useEffect, useState } from "react";
import {
  Button,
  Card,
  Container,
  Form,
  FormControl,
  FormGroup,
  FormLabel,
  InputGroup,
  Row,
  Toast,
  ToastContainer,
} from "react-bootstrap";
import FiltroArquivosImportados from "../../../components/filtroArquivosImportados/FiltroArquivosImportados";
import FiltroRiscoMercado from "../../../components/filtroRiscoMercado/FiltroRiscoMercado";
import FiltroTicker from "../../../components/filtroTicker/FiltroTicker";

import { getTickersCompareFormulas } from "../../../utils/funcoesFormula";

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
        <Container className="mb-1">
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
        <Card>
          <Card.Header>
            <strong>Resultado da Busca:</strong>
          </Card.Header>
          <Card.Body>
            <Card>
              <Card.Header>
                <b>Informações do Ticker: {dataTicker.ticker}</b>
              </Card.Header>
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
            </Card>
          </Card.Body>
        </Card>
      ) : null}
    </div>
  );
};

export default Comparador;
