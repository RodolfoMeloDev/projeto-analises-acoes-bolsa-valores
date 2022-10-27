import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';

import apiLogin from '../../api/login';

import imgLogo from './logo.svg';

import { useState } from 'react';

const Menu = () => {
  
  const [user, setUser] = useState('');

  const tokenValido = () => {
    const today = new Date(Date.now());
    let dataValidade = localStorage.getItem('data-validade');    

    // caso não tenha acessado ainda
    if (dataValidade === 'null')
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
                                                      login: "rodollopes@gmail.com"
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
      }else
      {
        setUserLogado(false);
        setUser('');
      }
    }    
  };

  return (
    <Navbar bg="dark" variant="dark" expand="lg">
      <Container>
        <Navbar.Brand href="#home">
          <img src={imgLogo}
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
          {!userLogado ? <Button className="btn btn-sm btn-primary" onClick={getLogin}>ENTRAR</Button> : null}
        </Navbar.Collapse>        
      </Container>        
    </Navbar>
  );
}

export default Menu;
