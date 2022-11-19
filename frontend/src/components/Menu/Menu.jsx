import Container from "react-bootstrap/Container";
import Button from "react-bootstrap/Button";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";

import imgLogo from "../../../src/logo.svg";

import { usuarioToken } from "../../utils/funcoesLogin";

import { useState } from "react";
import { NavLink, useLocation } from "react-router-dom";
import Login from "../login/Login";

import { useNavigate } from "react-router-dom";

const Menu = () => {
  const navigate = useNavigate();
  const getActiveRoute = useLocation().pathname ? "Active" : "";
  const [showLogin, setShowLogin] = useState(false);

  const [user, setUser] = useState(usuarioToken);

  const deslogar = (e) => {
    e.preventDefault();

    localStorage.setItem("data-validade", null);
    localStorage.setItem("token", null);
    localStorage.setItem("nickName", null);
    setUser("");
    navigate("/");
  };

  const handleOpenCloseModalLogin = () => {
    openCloseModalLogin(true);
  };

  const openCloseModalLogin = (value) => {
    setShowLogin(value);
  };

  const setUserLogin = (value) => {
    setUser(value);
  };

  return (
    <>
      <Navbar bg="dark" variant="dark" expand="lg">
        <Container>
          <Navbar.Brand as={NavLink} to="/">
            <img
              src={imgLogo}
              width="28"
              height="28"
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
              {user !== "" ? (
                <Nav.Link
                  className={getActiveRoute}
                  as={NavLink}
                  to="/formasAnalise"
                >
                  Formas de Analise
                </Nav.Link>
              ) : null}
            </Nav>
            {user === "" ? (
              <Button
                className="btn btn-sm btn-primary"
                onClick={handleOpenCloseModalLogin}
              >
                ENTRAR
              </Button>
            ) : (
              <Nav>
                <NavDropdown title={user} id="basic-nav-dropdown">
                  <NavDropdown.Item as={NavLink} to="/dashboard">
                    Dashboard
                  </NavDropdown.Item>
                  <NavDropdown.Item as={NavLink} to="/formasAnalise">
                    Formas de Analise
                  </NavDropdown.Item>
                  <NavDropdown.Divider />
                  <NavDropdown.Item onClick={deslogar}>Sair</NavDropdown.Item>
                </NavDropdown>
              </Nav>
            )}
          </Navbar.Collapse>
        </Container>
      </Navbar>

      <Login
        open={showLogin}
        openCloseModalLogin={openCloseModalLogin}
        userLogin={setUserLogin}
      />
    </>
  );
};

export default Menu;
