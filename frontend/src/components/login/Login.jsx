import React from "react";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import imgLogo from "../../../src/logo.svg";
import { getLogin } from "../../utils/funcoesLogin";

import { Button, Form, Modal, Toast, ToastContainer } from "react-bootstrap";

import RecuperarSenha from "../recuperarSenha/RecuperarSenha";
import Usuario from "../usuario/Usuario";

const Login = ({ open, openCloseModalLogin, userLogin }) => {
  const navigate = useNavigate();

  const userLoginInitial = {
    login: "",
    password: "",
  };

  const [showToast, setShowToast] = useState(false);
  const [login, setUserLogin] = useState(userLoginInitial);
  const [error, setError] = useState("");

  const handleOpenCloseModalLogin = () => {
    setShowToast(false);
    openCloseModalLogin(false);
  };

  const handleInputLogin = (e) => {
    const { name, value } = e.target;

    setUserLogin({ ...login, [name]: value });
  };

  const handleOpenPasswordRecovery = () => {
    openCloseModalLogin(false);
    setShowModalRecuperarSenha(true);
  };

  const handleOpenCreateUser = () => {
    openCloseModalLogin(false);
    setShowModalUsuario(true);
  };

  const handleClickLogin = async () => {
    const dadosLogin = await getLogin(login.login, login.password);

    if (dadosLogin.logado) {
      userLogin(dadosLogin.user);
      openCloseModalLogin(false);
      navigate("/dashboard");
    } else {
      userLogin("");
      setError(dadosLogin.error);
    }
  };

  useEffect(() => {
    if (error !== "") {
      setShowToast(true);
    }
  }, [error]);

  const [showModalRecuperarSenha, setShowModalRecuperarSenha] = useState(false);

  const closeModalRecuperarSenha = (value) => {
    setShowModalRecuperarSenha(value);
  };

  const [showModalUsuario, setShowModalUsuario] = useState(false);

  const closeModalUsuario = (value) => {
    setShowModalUsuario(value);
  };

  return (
    <>
      <Modal
        id="loginModal"
        show={open}
        onHide={handleOpenCloseModalLogin}
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title>
            <img
              src={imgLogo}
              width="35"
              height="35"
              className="d-inline-block align-top me-2"
              alt="Login"
            />
            Login
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
                onChange={handleInputLogin}
                required
                autoFocus
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="password">
              <Form.Label>Senha</Form.Label>
              <Form.Control
                name="password"
                type="password"
                placeholder="Senha"
                onChange={handleInputLogin}
                required
              />
            </Form.Group>
            <div className="d-flex justify-content-between">
              <Button
                variant="outline-secondary"
                onClick={handleOpenPasswordRecovery}
              >
                Esqueceu sua Senha
              </Button>
              <Button
                className="btn btn-success"
                // onClick={() => getLogin(true)}
                onClick={handleClickLogin}
              >
                Entrar
              </Button>
            </div>
          </Form>
          <div className="d-grid gap-2">
            <hr />
            <p className="mb-0 text-center">Ainda não tem cadastro?</p>
            <Button
              className="btn btn-secondary"
              onClick={handleOpenCreateUser}
            >
              Faça seu Cadastro Aqui
            </Button>
          </div>
        </Modal.Body>
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
      </Modal>

      <RecuperarSenha
        open={showModalRecuperarSenha}
        closeModal={closeModalRecuperarSenha}
        backModalLogin={openCloseModalLogin}
      />

      <Usuario
        open={showModalUsuario}
        closeModal={closeModalUsuario}
        backModalLogin={openCloseModalLogin}
        userLogin={userLogin}
      />
    </>
  );
};

export default Login;
