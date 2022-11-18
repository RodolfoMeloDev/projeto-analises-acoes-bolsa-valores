import React from "react";

import imgLogo from "../../../src/logo.svg";

import { Button, Form, Modal } from "react-bootstrap";

const RecuperarSenha = ({ open, closeModal, backModalLogin }) => {
  const handleCloseModal = () => {
    closeModal(false);
  };

  const handleBackModalLogin = () => {
    backModalLogin(true);
    handleCloseModal();
  };

  return (
    <Modal
      id="passwordRecoveryModal"
      show={open}
      onHide={handleCloseModal}
      centered
    >
      <Modal.Header closeButton>
        <Modal.Title>
          <img
            src={imgLogo}
            width="35"
            height="35"
            className="d-inline-block align-top me-2"
            alt="Recuperar Senha"
          />
          Recuperar Senha
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group className="mb-3" controlId="login">
            <Form.Label>Email</Form.Label>
            <Form.Control
              name="login"
              type="email"
              placeholder="email@mail.com.br"
              required
              autoFocus
            />
          </Form.Group>
        </Form>
        <hr />
        <Form className="d-flex justify-content-between">
          <Button variant="outline-secondary" onClick={handleBackModalLogin}>
            Voltar
          </Button>
          <Button variant="warning">Recuperar Senha</Button>
        </Form>
      </Modal.Body>
    </Modal>
  );
};

export default RecuperarSenha;
