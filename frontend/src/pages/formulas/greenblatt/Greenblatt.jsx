import React from "react";
import {
  Button,
  Container,
  FormCheck,
  FormControl,
  FormGroup,
  FormLabel,
  Row,
} from "react-bootstrap";
import { useParams } from "react-router-dom";

const Greenblatt = () => {
  const { id } = useParams();

  return (
    <div id="frmFormulaGreenblatt" className="mt-3">
      <h3>Filtros:</h3>
      <Container className="mb-1">
        <Row>
          <FormGroup
            className="border rounded p-2 me-1"
            style={{ width: "33%" }}
          >
            <FormLabel>
              <strong>Ev/Ebit:</strong>
            </FormLabel>
            <Row className="d-flex justify-content-between">
              <FormControl
                id="edtEvEbitMinimo"
                type="number"
                className="ms-2 me-2"
                placeholder="Valor"
                style={{ width: "43%" }}
              />
              <FormLabel style={{ display: "contents" }}>à</FormLabel>
              <FormControl
                id="edtEvEbitMaximo"
                type="number"
                className="ms-2 me-2"
                placeholder="Valor"
                style={{ width: "43%" }}
              />
            </Row>
          </FormGroup>

          <FormGroup
            className="border rounded p-2 me-1"
            style={{ width: "33%" }}
          >
            <FormLabel>
              <strong>P/L:</strong>
            </FormLabel>
            <Row className="d-flex justify-content-between">
              <FormControl
                id="edtPLMinimo"
                type="number"
                className="ms-2 me-2"
                placeholder="Valor"
                style={{ width: "43%" }}
              />
              <FormLabel style={{ display: "contents" }}>à</FormLabel>
              <FormControl
                id="edtPLMaximo"
                type="number"
                className="ms-2 me-2"
                placeholder="Valor"
                style={{ width: "43%" }}
              />
            </Row>
          </FormGroup>

          <FormGroup className="border rounded p-2" style={{ width: "33%" }}>
            <FormLabel>
              <strong>Margem Ebit:</strong>
            </FormLabel>
            <Row className="d-flex justify-content-between">
              <FormControl
                id="edtMargemEbitMinimo"
                type="number"
                className="ms-2 me-2"
                placeholder="Valor"
                style={{ width: "43%" }}
              />
              <FormLabel style={{ display: "contents" }}>à</FormLabel>
              <FormControl
                id="edtMargemEbitMaximo"
                type="number"
                className="ms-2 me-2"
                placeholder="Valor"
                style={{ width: "43%" }}
              />
            </Row>
          </FormGroup>
        </Row>
      </Container>
      <Container className="mb-1">
        <Row>
          <FormGroup
            controlId="edtLiquidezDiariaMinima"
            className="border rounded p-2 me-1"
            style={{ width: "33%" }}
          >
            <FormLabel>
              <strong>Liquidez Diária Miníma:</strong>
            </FormLabel>
            <FormControl type="number" placeholder="Liquidez Miníma" />
          </FormGroup>
          <FormGroup
            controlId="edtRiscoMercado"
            className="border rounded p-2"
            style={{ width: "33%" }}
          >
            <FormLabel>
              <strong>% Risco de Mercado:</strong>
            </FormLabel>
            <FormControl type="number" placeholder="Valor" />
          </FormGroup>
        </Row>
      </Container>
      <Container className="border rounded mb-1">
        <Row className="p-2 justify-content-between">
          <FormCheck
            inline
            className="me-0"
            defaultChecked={true}
            label="Remover Ação em Recuperação Judicial"
            type="switch"
            id="swRecuperacaoJudicial"
            style={{ width: "33%" }}
          />
          <FormCheck
            inline
            className="me-0"
            defaultChecked={true}
            label="Remover Ação com Valor Zero"
            type="switch"
            id="swRemoverValorZero"
            style={{ width: "33%" }}
          />
          <FormCheck
            inline
            className="me-0"
            defaultChecked={true}
            label="Remover Ação com Valor Negativo"
            type="switch"
            id="swRemoverValorNegativo"
            style={{ width: "33%" }}
          />
          <FormCheck
            inline
            className="me-0"
            defaultChecked={true}
            label="Remover Itens de Menor Liquidez"
            type="switch"
            id="swRemoverMenorLiquidez"
            style={{ width: "33%" }}
          />
        </Row>
      </Container>
      <div id="botoesPesquisa">
        <Button className="me-2" variant="success">
          Buscar
        </Button>
        <Button variant="outline-secondary">Limpar</Button>
      </div>
    </div>
  );
};

export default Greenblatt;
