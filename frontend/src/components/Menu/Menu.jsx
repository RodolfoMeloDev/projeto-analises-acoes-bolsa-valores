import Container from "react-bootstrap/Container";
import Button from "react-bootstrap/Button";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";

import imgLogo from "../../../src/logo.svg";

import { useState } from "react";
import { NavLink, useLocation } from "react-router-dom";
import Login from "../login/Login";

const Menu = ({ user, logout, setUserLogado }) => {
  const getActiveRoute = useLocation().pathname ? "Active" : "";
  const [showLogin, setShowLogin] = useState(false);

  const deslogar = (e) => {
    e.preventDefault();

    logout();
  };

  const handleOpenCloseModalLogin = () => {
    openCloseModalLogin(true);
  };

  const openCloseModalLogin = (value) => {
    setShowLogin(value);
  };

  const setUserLogin = (value) => {
    setUserLogado(value);
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
                Ações B3
              </Nav.Link>
              {user !== "" ? (
                <>
                  <Nav.Link
                    className={getActiveRoute}
                    as={NavLink}
                    to="/importador"
                  >
                    Importador
                  </Nav.Link>
                  <NavDropdown align="end" title="Formulas" id="menFormulas">
                    <NavDropdown.Item as={NavLink} to="/formula/comparador">
                      Comparador Individual
                    </NavDropdown.Item>
                    <NavDropdown.Item
                      as={NavLink}
                      to="/formula/comparadorGeral"
                    >
                      Comparador Geral
                    </NavDropdown.Item>
                    <NavDropdown.Divider />
                    <NavDropdown.Item as={NavLink} to="/formula/evEbit/">
                      Ev/Ebit
                    </NavDropdown.Item>
                    <NavDropdown.Item as={NavLink} to="/formula/pl/">
                      Preço/Lucro
                    </NavDropdown.Item>
                    <NavDropdown.Divider />
                    <NavDropdown.Item as={NavLink} to="/formula/bazin/">
                      Bazin
                    </NavDropdown.Item>
                    <NavDropdown.Item as={NavLink} to="/formula/graham/">
                      Graham
                    </NavDropdown.Item>
                    <NavDropdown.Item as={NavLink} to="/formula/greenblatt/">
                      Greenblatt
                    </NavDropdown.Item>
                    <NavDropdown.Item as={NavLink} to="/formula/gordon/">
                      Gordon
                    </NavDropdown.Item>
                  </NavDropdown>
                </>
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
                <NavDropdown align="end" title={user} id="menLogado">
                  <NavDropdown.Item as={NavLink} to="/dashboard">
                    Dashboard
                  </NavDropdown.Item>
                  <NavDropdown.Item as={NavLink} to="/importador">
                    Importador
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
