import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import imgLogo from "../../../src/logo.svg";

import apiUsers from "../../api/users";

import { getLogin } from "../../utils/funcoesLogin";

import { Button, Form, Modal, Toast, ToastContainer } from "react-bootstrap";

const newUserInitial = {
  name: "",
  nickname: "",
  login: "",
  password: "",
};

const Usuario = ({ open, closeModal, backModalLogin, userLogin }) => {
  const navigate = useNavigate();

  const [newUser, setNewUser] = useState(newUserInitial);
  const [validated, setValidated] = useState(false);
  const [error, setError] = useState("");
  const [showToast, setShowToast] = useState(false);

  useEffect(() => {
    if (error !== "") {
      setShowToast(true);
    }
  }, [error]);

  const handleCloseModal = () => {
    setValidated(false);
    closeModal(false);
  };

  const handleBackModalLogin = () => {
    setValidated(false);
    backModalLogin(true);
    handleCloseModal();
  };

  const handleInputNewUser = (e) => {
    const { name, value } = e.target;

    setNewUser({ ...newUser, [name]: value });
  };

  const handleSubmit = async (e) => {
    setError("");

    const form = e.currentTarget;
    e.preventDefault();
    if (form.checkValidity() === false) {
      e.stopPropagation();
    }

    setValidated(true);

    try {
      const response = await apiUsers.post("", {
        name: newUser.name,
        nickname: newUser.nickname,
        login: newUser.login,
        password: newUser.password,
      });

      if (response.status === 201) {
        const dadosLogin = await getLogin(newUser.login, newUser.password);
        userLogin(dadosLogin.user);
        setNewUser(newUserInitial);
        handleCloseModal();
        navigate("/dashboard");
      } else {
        setError(response.data.message);
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

  return (
    <Modal id="createUser" show={open} onHide={handleCloseModal} centered>
      <Modal.Header closeButton>
        <Modal.Title>
          <img
            src={imgLogo}
            width="35"
            height="35"
            className="d-inline-block align-top me-2"
            alt="Novo Usuário"
          />
          Novo Usuário
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form
          id="formNewUser"
          noValidate
          validated={validated}
          onSubmit={handleSubmit}
        >
          <Form.Group className="mb-3" controlId="nameUser">
            <Form.Label>Nome</Form.Label>
            <Form.Control
              name="name"
              type="text"
              placeholder="Informe seu Nome"
              onChange={handleInputNewUser}
              required
            />
            <Form.Control.Feedback type="invalid">
              O campo Nome é obrigatório!
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group className="mb-3" controlId="nicknameUser">
            <Form.Label>Apelido</Form.Label>
            <Form.Control
              name="nickname"
              type="text"
              placeholder="Como quer ser chamado"
              onChange={handleInputNewUser}
            />
            <Form.Control.Feedback type="valid"></Form.Control.Feedback>
          </Form.Group>
          <hr />
          <Form.Group className="mb-3" controlId="loginUser">
            <Form.Label>Email</Form.Label>
            <Form.Control
              name="login"
              type="email"
              placeholder="email@mail.com.br"
              onChange={handleInputNewUser}
              required
            />
            <Form.Control.Feedback type="invalid">
              O campo E-mail é obrigatório!
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group className="mb-3" controlId="passwordUser">
            <Form.Label>Senha</Form.Label>
            <Form.Control
              name="password"
              type="password"
              placeholder="Senha"
              onChange={handleInputNewUser}
              required
            />
            <Form.Control.Feedback type="invalid">
              O campo Senha é obrigatório!
            </Form.Control.Feedback>
          </Form.Group>
          <hr />
          <div className="d-flex justify-content-between">
            <Button variant="outline-secondary" onClick={handleBackModalLogin}>
              Voltar
            </Button>
            <Button variant="success" type="submit">
              Cadastrar
            </Button>
          </div>
        </Form>
        <ToastContainer position="middle-center">
          <Toast
            bg="warning"
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
      </Modal.Body>
    </Modal>
  );
};

export default Usuario;
