import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';

import { useState } from 'react';

const Menu = () => {
  
  const tokenValido = () => {
    const today = new Date(Date.now());
    
    const dataValidade = Date.parse(localStorage.getItem('data-validade'));

    if (Date.parse(today) < Date.parse(dataValidade) || isNaN(dataValidade))
      return false;
    else
      return true;
  }
  
  const [userLogado, setUserLogado] = useState(tokenValido())

  const gravaLocalStore = (e) => {
    e.preventDefault();

    const today = new Date(Date.now());

    localStorage.setItem('data-validade', today);
    localStorage.setItem('token', 'teste');

    setUserLogado(!userLogado);
  }

  const deslogar = (e) => {
    e.preventDefault();
    setUserLogado(!userLogado);
  }

  return (
    <Navbar bg="dark" variant="dark" expand="lg">
      <Container>
        <Navbar.Brand href="#home">React-Bootstrap</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link href="#home">Home</Nav.Link>
            <Nav.Link href="#link">Link</Nav.Link>
            <NavDropdown title="Dropdown" id="basic-nav-dropdown">
              <NavDropdown.Item href="#action/3.1">Action</NavDropdown.Item>
              <NavDropdown.Item href="#action/3.2">
                Another action
              </NavDropdown.Item>
              <NavDropdown.Item href="#action/3.3">Something</NavDropdown.Item>
              <NavDropdown.Divider />
              <NavDropdown.Item href="#action/3.4">
                Separated link
              </NavDropdown.Item>
            </NavDropdown>
          </Nav>
        </Navbar.Collapse>
        {userLogado ? <Button className="btn btn-primary" onClick={gravaLocalStore}>ENTRAR</Button> : <Button className="btn btn-primary" onClick={deslogar} >LOGADO</Button>}
      </Container>        
    </Navbar>
  );
}

export default Menu;
