import { useEffect, useState } from "react";

import apiFileImport from "../../api/fileImport";
import apiUser from "../../api/users";

import "./importador.css";

import {
  Button,
  Col,
  Form,
  Modal,
  Row,
  Spinner,
  Toast,
  ToastContainer,
} from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const initialFileImport = {
  UserId: 0,
  Description: "",
  DateFile: "",
  TypeFile: "",
  File: null,
};

const Importador = () => {
  const navigate = useNavigate();

  const [validated, setValidated] = useState(false);
  const [error, setError] = useState("");
  const [showToast, setShowToast] = useState(false);
  const [fileImportData, setFileImportData] = useState(initialFileImport);
  const [fileImported, setFileImported] = useState(null);
  const [importando, setImportando] = useState(false);
  const [show, setShow] = useState(false);

  const handleFileImport = (e) => {
    const { name, value } = e.target;
    setFileImportData({ ...fileImportData, [name]: value });
  };

  const handleFileImportFile = (e) => {
    setError("");
    const file = e.target.files[0];

    if (file === undefined) {
      setFileImported(null);
      return;
    }

    if (file.size / 1024 <= 2048) {
      setFileImported(e.target.files[0]);
    } else {
      setError("Não é possível importar um arquivo maior do que 2MB!");
    }
  };

  const retornaUserId = async () => {
    try {
      const response = await apiUser.get(localStorage.getItem("login"), {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("token"),
        },
      });

      if (response.status === 200) {
        return response.data.id;
      } else {
        setError(response.data.message);
        return;
      }
    } catch (e) {
      setError(
        e.response.data
          .substr(
            e.response.data.search(":") + 1,
            e.response.data.search("\r\n") - e.response.data.search(":")
          )
          .trim()
      );
    }
  };

  const handleSubmit = async (e) => {
    setError("");

    const form = e.currentTarget;
    e.preventDefault();
    if (form.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
      return;
    }

    setValidated(false);

    const formData = new FormData(form);
    formData.append("UserId", await retornaUserId());

    try {
      setImportando(true);
      const response = await apiFileImport.post("", formData, {
        headers: {
          accept: "*",
          Authorization: "Bearer " + localStorage.getItem("token"),
          "Content-Type": "multipart/form-data",
        },
      });

      if (response.status === 201) {
        setFileImportData(initialFileImport);
        setFileImported(null);
        form.reset();
        setShow(true);
      } else {
        console.log(response);
        setError(response.data.message);
      }
    } catch (e) {
      console.log(e);
      setError(
        e.response.data
          .substr(
            e.response.data.search(":") + 1,
            e.response.data.search("\r\n") - e.response.data.search(":")
          )
          .trim()
      );
    }

    setImportando(false);
  };

  const handleCloseModal = () => {
    setShow(false);
  };

  useEffect(() => {
    if (error !== "") {
      setShowToast(true);
    }
  }, [error]);

  return (
    <div className="mt-3 mb-3">
      <Form
        id="frmImportador"
        className="border rounded p-3"
        noValidate
        validated={validated}
        onSubmit={handleSubmit}
      >
        <Form.Group className="mb-3" controlId="edtDescricaoArquivo">
          <Form.Label className="text-start">Descrição Arquivo</Form.Label>
          <Form.Control
            name="Description"
            placeholder="Descrição para o arquivo importado"
            value={fileImportData.Description}
            onChange={handleFileImport}
            required
          />
          <Form.Control.Feedback type="invalid">
            O campo Descrição do arquivo é obrigatório!
          </Form.Control.Feedback>
        </Form.Group>

        <Row>
          <Form.Group
            as={Col}
            md={3}
            className="mb-3"
            controlId="selOrigemArquivo"
          >
            <Form.Label>Origem Arquivo</Form.Label>
            <Form.Select
              name="TypeFile"
              value={
                fileImportData.TypeFile === ""
                  ? ""
                  : parseInt(fileImportData.TypeFile)
              }
              onChange={handleFileImport}
              required
            >
              <option value=""></option>
              <option value={1}>Status Invest</option>
              <option value={2}>Fundamentus</option>
            </Form.Select>
            <Form.Control.Feedback type="invalid">
              Selecione a origem do arquivo
            </Form.Control.Feedback>
          </Form.Group>

          <Form.Group
            as={Col}
            md={3}
            className="mb-3"
            controlId="dtDataBaseArquivo"
          >
            <Form.Label>Data Base</Form.Label>
            <Form.Control
              name="DateFile"
              value={fileImportData.DateFile}
              onChange={handleFileImport}
              type="date"
              required
            />
            <Form.Control.Feedback type="invalid">
              Data informada inválida
            </Form.Control.Feedback>
          </Form.Group>
        </Row>

        <Form.Group className="mb-3" controlId="edtFile">
          <Form.Label>Arquivo Importação</Form.Label>
          <Form.Control
            name="File"
            onChange={handleFileImportFile}
            type="file"
            placeholder="Selecione o arquivo para importação"
            required
          />
          <Form.Control.Feedback type="invalid">
            É necessário selecionar o arquivo para importação
          </Form.Control.Feedback>
        </Form.Group>

        <Button
          variant="success"
          type="submit"
          disabled={fileImported === null}
        >
          {importando === true ? (
            <Spinner
              className="me-2"
              as="span"
              animation="border"
              size="sm"
              role="status"
              aria-hidden="true"
            />
          ) : null}
          {importando === true ? "Importando" : "Importar"}
        </Button>
        <ToastContainer position="middle-center">
          <Toast
            bg={error === "" ? "success" : "warning"}
            onClose={() => setShowToast(false)}
            show={showToast}
            delay={5000}
            autohide
          >
            <Toast.Header>
              <img
                src="holder.js/20x20?text=%20"
                className="rounded me-2"
                alt=""
              />
              <strong className="me-auto">Mensagem</strong>
            </Toast.Header>
            <Toast.Body>{error}</Toast.Body>
          </Toast>
        </ToastContainer>
      </Form>
      <Modal show={show} onHide={handleCloseModal} centered>
        <Modal.Header>
          <Modal.Title>Mensagem</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p>A importação foi realizada com sucesso!</p>
          <p>Deseja ser redirecionado para a tela de dashborad ?</p>
        </Modal.Body>
        <Modal.Footer className="d-flex justify-content-between">
          <Button variant="outline-danger" onClick={handleCloseModal}>
            Não
          </Button>
          <Button
            variant="outline-success"
            onClick={() => navigate("/dashboard")}
          >
            Sim
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default Importador;
