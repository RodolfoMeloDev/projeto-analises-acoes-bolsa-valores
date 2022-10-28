import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';

import apiLogin from '../../api/login';

import imgLogo from './logo.svg';

import { useState } from 'react';

const Menu = () => {
  const [showLogin, setShowLogin] = useState(false);
  const [showPasswordRecovery, setShowPasswordRecovery] = useState(false);
  const [user, setUser] = useState('');

  const tokenValido = () => {
    const today = new Date(Date.now());
    let dataValidade = localStorage.getItem('data-validade');    

    // caso não tenha acessado ainda
    if (dataValidade === 'null' || dataValidade === null)
      return false;

    dataValidade = new Date(dataValidade.substr(0, 4), dataValidade.substr(5, 2) -1, dataValidade.substr(8, 2), 
                            dataValidade.substr(11,2), dataValidade.substr(14,2), dataValidade.substr(17,2));

    if (today > dataValidade)
      return false;
    else
    {      
      setUser(localStorage.getItem('nickName'));
      return true;
    }
  }
  
  const [userLogado, setUserLogado] = useState(tokenValido);

  const deslogar = (e) => {
    e.preventDefault();

    localStorage.setItem('data-validade', null);
    localStorage.setItem('token', null);
    localStorage.setItem('nickName', null);    
    
    setUserLogado(!userLogado);
    setUser('');
  }

  const getLogin = async (e) => {
    e.preventDefault();

    if (!userLogado){
      const response = await apiLogin.post('login', { 
                                                      "login": "rodollopes@gmail.com"
                                                    });

      if (response.status === 200)
      {        
        const login = response.data;
        const nameUser = login.nickName !== "" && login.nickName !== null ? login.nickName : login.name.substr(0, login.name.search(" "));
  
        localStorage.setItem('data-validade', login.expiration);
        localStorage.setItem('token', login.accessToken);
        localStorage.setItem('nickName', nameUser);
  
        setUserLogado(true);
        setUser(nameUser);
        handleOpenCloseModalLogin();
      }else
      {
        setUserLogado(false);
        setUser('');
      }
    }    
  };

  const handleOpenCloseModalLogin = () => setShowLogin(!showLogin);
  const handleOpenCloseModalPasswordRecovery = () => {
    setShowPasswordRecovery(!showPasswordRecovery);
    setShowLogin(!showLogin);
  }
  const handleOpenPasswordRecovery = () => {
    setShowLogin(false);
    setShowPasswordRecovery(true);
  }

  return (
    <>
      <Navbar bg="dark" variant="dark" expand="lg">
        <Container>
          <Navbar.Brand href="#home">
            <img 
              src={imgLogo}
              width="30"
              height="30"
              className="d-inline-block align-top me-1"
              alt=""
            />
            Comparador de Analises</Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">            
              <Nav.Link href="#home">Ações</Nav.Link>
              <Nav.Link href="#link">Setores</Nav.Link>
              {userLogado ? 
              <NavDropdown title={user} id="basic-nav-dropdown">
                <NavDropdown.Item href="#action/3.1">Dashboard</NavDropdown.Item>
                <NavDropdown.Item href="#action/3.2">
                  Formas de Analise
                </NavDropdown.Item>
                <NavDropdown.Divider />
                <NavDropdown.Item href="#action/3.4" onClick={deslogar}>
                  Sair
                </NavDropdown.Item>
              </NavDropdown>
              : null
              }
            </Nav>
            {!userLogado ? <Button className="btn btn-sm btn-primary" onClick={handleOpenCloseModalLogin}>ENTRAR</Button> : null}
          </Navbar.Collapse>        
        </Container>        
      </Navbar>

      <Modal id="loginModal" show={showLogin} onHide={handleOpenCloseModalLogin}>
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
            <Form.Group className="mb-3" controlId="emailGroup">
              <Form.Label>Email</Form.Label>
              <Form.Control
                type="email"
                placeholder="email@mail.com.br"
                autoFocus
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="passwordGroup">
              <Form.Label>Senha</Form.Label>
              <Form.Control type="password" placeholder="Senha" />
            </Form.Group>
          </Form>
          <div className="d-flex justify-content-between">
            <Button 
              variant="outline-secondary"
              onClick={handleOpenPasswordRecovery}
            >
              Esqueceu sua Senha
            </Button>
            <Button className="btn btn-success" onClick={getLogin}>
              Entrar
            </Button>
          </div>
          <div className="d-grid gap-2">
            <hr/>
            <p className='mb-0 text-center'>Ainda não tem cadastro?</p>
            <Button className="btn btn-secondary">
              Faça seu Cadastro Aqui
            </Button>
          </div>
        </Modal.Body>        
      </Modal>

      <Modal id="passwordRecoveryModal" show={showPasswordRecovery} onHide={handleOpenCloseModalPasswordRecovery}>
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
            <Form.Group className="mb-3" controlId="emailRecoveryGroup">
              <Form.Label>Email</Form.Label>
              <Form.Control
                type="email"
                placeholder="email@mail.com.br"
                autoFocus
              />
            </Form.Group>            
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Form className="d-flex justify-content-between">
            <Button 
              variant="outline-secondary"
              onClick={handleOpenCloseModalPasswordRecovery}
            >
              Voltar
            </Button>
            <Button variant="warning">
              Recuperar Senha
            </Button>
          </Form>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default Menu;
