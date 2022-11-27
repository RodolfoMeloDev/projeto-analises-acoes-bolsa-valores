import { Button, Card, Table } from "react-bootstrap";

import "./dashborad.css";

const formulas = ["Comparativo de Formulas", "Greenblatt", "Preço/Lucro", "Ev/Ebit", "Bazin", "Graham", "Bordon"]

const montaCards = () => {
  let cards = [];

  for (let index = 0; index < formulas.length; index++) {
    cards.push(
      <Card key={index} className='card-formula m-1'>
        <Card.Header>{formulas[index]}</Card.Header>
        <Card.Body>fsdfds</Card.Body>
      </Card>
    );
  }

  return cards;
}

const Dashboard = () => {
  return (
    <>
      <div className='d-flex ms-1 me-1'>
        <Card className="mt-3 me-3" style={{width: "19rem"}}>
          <Card.Body>
            <Card.Title>Arquivos Importados:</Card.Title>
            <Card.Text className="contador text-center">
              999
            </Card.Text>        
          </Card.Body>
        </Card>
        <Card className="mt-3" style={{width: "100%"}}>
          <Card.Body>
            <Card.Title>Últimas importações:</Card.Title>
              <Table className="mb-0" striped bordered hover responsive>
                <thead>
                  <tr>
                    <th className="text-center coluna-grid" >Data</th>
                    <th>Descrição</th>
                    <th className="text-center coluna-grid">GreenBlatt</th>
                    <th className="text-center coluna-grid">P/L</th>
                    <th className="text-center coluna-grid">Ev/Ebit</th>
                    <th className="text-center coluna-grid">Bazin</th>
                    <th className="text-center coluna-grid">Graham</th>
                    <th className="text-center coluna-grid">Gordon</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td className="text-center">10/11/2022</td>
                    <td>Teste 1</td>
                    <td className="text-center"><Button variant="outline-success">GreenBlatt</Button></td>
                    <td className="text-center"><Button variant="outline-success">P/L</Button></td>
                    <td className="text-center"><Button variant="outline-success">Ev/Ebit</Button></td>
                    <td className="text-center"><Button variant="outline-success">Bazin</Button></td>
                    <td className="text-center"><Button variant="outline-success">Graham</Button></td>
                    <td className="text-center"><Button variant="outline-success">Gordon</Button></td>
                  </tr>
                  <tr>
                    <td className="text-center">10/11/2022</td>
                    <td>Teste 1</td>
                    <td className="text-center"><Button variant="outline-success">GreenBlatt</Button></td>
                    <td className="text-center"><Button variant="outline-success">P/L</Button></td>
                    <td className="text-center"><Button variant="outline-success">Ev/Ebit</Button></td>
                    <td className="text-center"><Button variant="outline-success">Bazin</Button></td>
                    <td className="text-center"><Button variant="outline-success">Graham</Button></td>
                    <td className="text-center"><Button variant="outline-success">Gordon</Button></td>
                  </tr>
                  <tr>
                    <td className="text-center">10/11/2022</td>
                    <td>Teste 1</td>
                    <td className="text-center"><Button variant="outline-success">GreenBlatt</Button></td>
                    <td className="text-center"><Button variant="outline-success">P/L</Button></td>
                    <td className="text-center"><Button variant="outline-success">Ev/Ebit</Button></td>
                    <td className="text-center"><Button variant="outline-success">Bazin</Button></td>
                    <td className="text-center"><Button variant="outline-success">Graham</Button></td>
                    <td className="text-center"><Button variant="outline-success">Gordon</Button></td>
                  </tr>
                </tbody>
            </Table>
          </Card.Body>
        </Card>        
      </div>
      <div className="d-flex flex-wrap mt-3">
        {montaCards()}
      </div>
    </>
  );
};

export default Dashboard;
