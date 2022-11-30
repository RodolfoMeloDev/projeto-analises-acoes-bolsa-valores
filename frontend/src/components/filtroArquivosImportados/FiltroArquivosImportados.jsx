import { useCallback, useEffect, useState } from "react";
import {
  Button,
  Container,
  FormControl,
  FormLabel,
  InputGroup,
  Modal,
  Table,
} from "react-bootstrap";

import PaginacaoGrid from "../paginacaoGrid/PaginacaoGrid";
import { retornarDadosUsuarioLogado } from "../../utils/funcoesUsuario";

import apiFilesImport from "../../api/fileImport";
import "./filtroArquivosImportados.css";
import "../../utils/funcoesUsuario";

const FiltroArquivosImportados = ({ values, setValues }) => {
  const [show, setShow] = useState(false);
  const [filesImported, setFilesImported] = useState([]);
  const [filesImportedGrid, setFilesImportedGrid] = useState(filesImported);
  const [itemInicial, setItemInicial] = useState(0);
  const [itemFinal, setItemFinal] = useState(10);

  const handleOpenClose = () => {
    setValues({
      ...values,
      fileImportId: "",
      description: "",
    });
    setShow(!show);
  };

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
  }, []);

  useEffect(() => {
    dataFilesImported();
  }, [dataFilesImported]);

  useEffect(() => {
    setFilesImportedGrid(filesImported.slice(itemInicial, itemFinal));
  }, [filesImported, itemFinal, itemInicial]);

  const getDataItemRowTable = (e) => {
    const fileImportId = e.target.getAttribute("data-item");
    const description = e.target.getAttribute("data-title");

    setValues({
      ...values,
      fileImportId: fileImportId,
      description: description,
    });
    setShow(!show);
  };

  return (
    <>
      <Container className="border rounded p-2 mb-3">
        <FormLabel>
          <strong>Arquivos Importados:</strong>
        </FormLabel>
        <InputGroup>
          <Button
            variant="outline-secondary"
            id="btnPesquisarArquivoImportado"
            onClick={handleOpenClose}
          >
            Pesquisar
          </Button>
          <FormControl
            id="campo-codigo"
            disabled={true}
            aria-label="Código do Arquivo Importado"
            placeholder="Id"
            value={values.fileImportId}
          />
          <FormControl
            id="campo-descricao"
            disabled={true}
            aria-label="Descrição do Arquivo Importado"
            placeholder="Descrição do Arquivo Importado"
            value={values.description}
          />
        </InputGroup>
      </Container>
      <Modal
        show={show}
        onHide={handleOpenClose}
        backdrop="static"
        keyboard={false}
        centered
        scrollable={true}
        size={"lg"}
      >
        <Modal.Header closeButton>
          <Modal.Title>Arquivos Importados</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Table striped bordered hover>
            <thead>
              <tr>
                <th className="text-center" style={{ minWidth: "120px" }}>
                  Data Arquivo
                </th>
                <th style={{ width: "100%" }}>Descrição</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              {filesImportedGrid.map((file) => {
                const dateFile = new Date(file.dateFile);
                return (
                  <tr key={file.id}>
                    <td className="text-center">
                      {dateFile.toLocaleDateString()}
                    </td>
                    <td>{file.description}</td>
                    <td>
                      <Button
                        data-item={file.id}
                        data-title={file.description}
                        size="sm"
                        variant="outline-success"
                        onClick={getDataItemRowTable}
                      >
                        Selecionar
                      </Button>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </Table>
          <PaginacaoGrid
            totalRegistros={filesImported.length}
            itemInicial={setItemInicial}
            itemFinal={setItemFinal}
            idAlternativo="selQuantidadeRegistrosModal"
          />
        </Modal.Body>
        <Modal.Footer>
          <Button variant="danger" onClick={handleOpenClose}>
            Fechar
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

export default FiltroArquivosImportados;
