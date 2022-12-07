import { useCallback, useEffect, useState } from "react";
import { Button, Card, Table } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

import apiFilesImport from "../../api/fileImport";
import "../../utils/funcoesUsuario";

import { retornarDadosUsuarioLogado } from "../../utils/funcoesUsuario";

import "./dashborad.css";

const formulas = [
  "Comparador Individual",
  "Comparador Geral",
  "Ev/Ebit",
  "Preço/Lucro",
  "Bazin",
  "Graham",
  "Greenblatt",
  "Gordon",
];

const linkPage = [
  "formula/comparador/",
  "formula/comparadorGeral/",
  "formula/evEbit/",
  "formula/pl/",
  "formula/bazin/",
  "formula/graham/",
  "formula/greenblatt/",
  "formula/gordon/",
];

const Dashboard = () => {
  const navigate = useNavigate();

  const [filesImported, setFilesImported] = useState([]);
  const [filesGrid, setFilesGrid] = useState([]);

  const getFilesImport = async () => {
    const user = await retornarDadosUsuarioLogado();

    const response = await apiFilesImport.get("User/" + user.data.id, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    });

    if (response.status === 200) {
      return response.data;
    }

    return [];
  };

  const dataFilesImported = useCallback(async () => {
    const files = await getFilesImport();
    setFilesImported(files);
    setFilesGrid(files.slice(0, 3));
  }, []);

  useEffect(() => {
    dataFilesImported();
  }, [dataFilesImported]);

  const montaCards = () => {
    let cards = [];

    for (let index = 0; index < formulas.length; index++) {
      cards.push(
        <Card key={index} className="card-formula">
          <Card.Header className="card-header-page">
            {formulas[index]}
          </Card.Header>
          <Card.Body className="card-formula-body">
            <Button
              variant="outline-success"
              onClick={() => navigate("/" + linkPage[index])}
            >
              Acessar
            </Button>
          </Card.Body>
        </Card>
      );
    }

    return cards;
  };

  return (
    <>
      <div className="d-flex justify-content-between dados-importacao">
        <Card className="card-contador">
          <Card.Header className="card-header-page">
            Arquivos Importados:
          </Card.Header>
          <Card.Body className="card-contador-texto">
            {filesImported.length}
          </Card.Body>
        </Card>
        <Card className="card-importacoes">
          <Card.Header className="card-header-page">
            Últimas importações:
          </Card.Header>
          <Card.Body>
            <Table className="mb-0" striped bordered hover responsive>
              <thead>
                <tr>
                  <th className="text-center coluna-grid">Data Arquivo</th>
                  <th className="text-center coluna-grid">Origem</th>
                  <th>Descrição</th>
                  <th className="text-center coluna-grid">Data Importação</th>
                </tr>
              </thead>
              <tbody>
                {filesGrid.map((file) => {
                  const dateFile = new Date(file.dateFile);
                  const dateImported = new Date(file.dateCreated);
                  return (
                    <tr key={file.id}>
                      <td className="text-center">
                        {dateFile.toLocaleDateString()}
                      </td>
                      <td className="text-center">
                        {file.nameTypeFile.replace("_", " ")}
                      </td>
                      <td>{file.description}</td>
                      <td className="text-center">
                        {dateImported.toLocaleDateString()}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </Table>
          </Card.Body>
        </Card>
      </div>
      <div className="mt-3">
        <h2>Métodos de Analise:</h2>
        <div id="cardsFormulas" className="d-flex flex-wrap formulas">
          {montaCards()}
        </div>
      </div>
    </>
  );
};

export default Dashboard;
