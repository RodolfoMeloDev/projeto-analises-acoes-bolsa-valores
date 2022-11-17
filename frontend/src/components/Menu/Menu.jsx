import Container from "react-bootstrap/Container";
import Button from "react-bootstrap/Button";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import Modal from "react-bootstrap/Modal";
import Form from "react-bootstrap/Form";
import Toast from "react-bootstrap/Toast";

import apiLogin from "../../api/login";
import apiUsers from "../../api/users";

import imgLogo from "./logo.svg";

import { useState, useEffect } from "react";
import { ToastContainer } from "react-bootstrap";
import { NavLink, useLocation } from "react-router-dom";

const userLoginInitial = {
  login: "",
  password: "",
};

const newUserInitial = {
  name: "",
  nickname: "",
  login: "",
  password: "",
};

let messageError;

const Menu = () => {
  const getActiveRoute = useLocation().pathname ? "Active" : "";

  const [validated, setValidated] = useState(false);
  const [showToast, setShowToast] = useState(false);
  const [error, setError] = useState(false);
  const [showLogin, setShowLogin] = useState(false);
  const [showPasswordRecovery, setShowPasswordRecovery] = useState(false);
  const [showCreateUser, setShowCreateUser] = useState(false);
  const [user, setUser] = useState("");
  const [login, setUserLogin] = useState(userLoginInitial);
  const [newUser, setNewUser] = useState(newUserInitial);

  const tokenValido = () => {
    const today = new Date(Date.now());
    let dataValidade = localStorage.getItem("data-validade");

    // caso não tenha acessado ainda
    if (dataValidade === "null" || dataValidade === null) return false;

    dataValidade = new Date(
      dataValidade.substr(0, 4),
      dataValidade.substr(5, 2) - 1,
      dataValidade.substr(8, 2),
      dataValidade.substr(11, 2),
      dataValidade.substr(14, 2),
      dataValidade.substr(17, 2)
    );

    if (today > dataValidade) return false;
    else {
      setUser(localStorage.getItem("nickName"));
      return true;
    }
  };

  const [userLogado, setUserLogado] = useState(tokenValido);

  const deslogar = (e) => {
    e.preventDefault();

    localStorage.setItem("data-validade", null);
    localStorage.setItem("token", null);
    localStorage.setItem("nickName", null);

    setUserLogado(!userLogado);
    setUser("");
  };

  const getLogin = async (useModalLogin) => {
    if (!userLogado) {
      try {
        const response = await apiLogin.post("", {
          login: login.login,
          password: login.password,
        });

        if (response.status === 200 && response.data.authenticated) {
          setUserLogin(userLoginInitial);
          const login = response.data;
          const nameUser =
            login.nickName !== "" && login.nickName !== null
              ? login.nickName
              : login.name.substr(0, login.name.search(" "));

          localStorage.setItem("data-validade", login.expiration);
          localStorage.setItem("token", login.accessToken);
          localStorage.setItem("nickName", nameUser);

          setUserLogado(true);
          setUser(nameUser);
          if (useModalLogin) {
            handleOpenCloseModalLogin();
          }
        } else {
          setUserLogado(false);
          setUser("");
          messageError = response.data.message;
          setError(true);
        }
      } catch (e) {
        setUserLogado(false);
        setUser("");
        console.log(e);
        messageError =
          "Deve ser o informado E-mail/Senha para realizar o Login!";
        setError(true);
      }
    }
  };

  const handleInputLogin = (e) => {
    const { name, value } = e.target;

    setUserLogin({ ...login, [name]: value });
  };

  const handleInputNewUser = (e) => {
    const { name, value } = e.target;

    setNewUser({ ...newUser, [name]: value });

    if (name === "login" || name === "password")
      setUserLogin({ ...login, [name]: value });
  };

  const handleSubmit = async (e) => {
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

      console.log(response);

      if (response.status === 201) {
        setShowCreateUser(false);
        getLogin(false);
        setNewUser(newUserInitial);
      } else {
        console.log(response);
        messageError = response.data.message;
        setError(true);
      }
    } catch (e) {
      console.log(e);
      messageError = e.response.data
        .substr(
          e.response.data.search(":") + 1,
          e.response.data.search("\r\n") - e.response.data.search(":")
        )
        .trim();
      setError(true);
    }
  };

  const handleOpenCloseModalLogin = () => {
    setShowToast(false);
    setShowLogin(!showLogin);
    setUserLogin(userLoginInitial);
  };

  const handleOpenCloseModalPasswordRecovery = () => {
    setShowPasswordRecovery(!showPasswordRecovery);
    setShowLogin(!showLogin);
  };

  const handleOpenCloseModalCreateUser = () => {
    setShowCreateUser(!showCreateUser);
    setShowLogin(!showLogin);
    setValidated(false);
  };

  const handleOpenPasswordRecovery = () => {
    setShowLogin(false);
    setShowPasswordRecovery(true);
  };

  const handleOpenCreateUser = () => {
    setShowLogin(false);
    setShowCreateUser(true);
  };

  useEffect(() => {
    if (error) {
      setShowToast(true);
      setError(false);
    }
  }, [error]);

  return (
    <>
      <Navbar bg="dark" variant="dark" expand="lg">
        <Container>
          <Navbar.Brand as={NavLink} to="/">
            <img
              src={imgLogo}
              width="30"
              height="30"
              className="d-inline-block align-top me-1"
              alt=""
            />
            Comparador de Analises
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link className={getActiveRoute} as={NavLink} to="/acoes">
                Ações
              </Nav.Link>
              <Nav.Link className={getActiveRoute} as={NavLink} to="/setores">
                Setores
              </Nav.Link>
              {userLogado ? (
                <Nav.Link
                  className={getActiveRoute}
                  as={NavLink}
                  to="/formasAnalise"
                >
                  Formas de Analise
                </Nav.Link>
              ) : null}
            </Nav>
            {!userLogado ? (
              <Button
                className="btn btn-sm btn-primary"
                onClick={handleOpenCloseModalLogin}
              >
                ENTRAR
              </Button>
            ) : (
              <Nav>
                <NavDropdown title={user} id="basic-nav-dropdown">
                  <NavDropdown.Item
                    className={getActiveRoute}
                    as={NavLink}
                    to="/dashboard"
                  >
                    Dashboard
                  </NavDropdown.Item>
                  <NavDropdown.Item href="#action/3.2">
                    Formas de Analise
                  </NavDropdown.Item>
                  <NavDropdown.Divider />
                  <NavDropdown.Item href="#action/3.4" onClick={deslogar}>
                    Sair
                  </NavDropdown.Item>
                </NavDropdown>
              </Nav>
            )}
          </Navbar.Collapse>
        </Container>
      </Navbar>

      <Modal
        id="loginModal"
        show={showLogin}
        onHide={handleOpenCloseModalLogin}
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title>
            <img
              src={imgLogo}
              width="45"
              height="45"
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
                onClick={() => getLogin(true)}
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
            <Toast.Body>{messageError}</Toast.Body>
          </Toast>
        </ToastContainer>
      </Modal>

      <Modal
        id="passwordRecoveryModal"
        show={showPasswordRecovery}
        onHide={handleOpenCloseModalPasswordRecovery}
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title>
            <img
              src={imgLogo}
              width="45"
              height="45"
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
            <Button
              variant="outline-secondary"
              onClick={handleOpenCloseModalPasswordRecovery}
            >
              Voltar
            </Button>
            <Button variant="warning">Recuperar Senha</Button>
          </Form>
        </Modal.Body>
      </Modal>

      <Modal
        id="createUser"
        show={showCreateUser}
        onHide={handleOpenCloseModalCreateUser}
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title>
            <img
              src={imgLogo}
              width="45"
              height="45"
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
              <Button
                variant="outline-secondary"
                onClick={handleOpenCloseModalCreateUser}
              >
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
              <Toast.Body>{messageError}</Toast.Body>
            </Toast>
          </ToastContainer>
        </Modal.Body>
      </Modal>
    </>
  );
};

export default Menu;
