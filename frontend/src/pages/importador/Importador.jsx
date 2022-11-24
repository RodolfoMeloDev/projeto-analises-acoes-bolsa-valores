import { Button, Col, Form, Row } from 'react-bootstrap';

const Importador = () => {
  return (
    <div className='mt-3 d-flex justify-content-center' >
      <Form className='border rounded p-3'>
        <Form.Group className="mt-3 mb-3" controlId="edtDescricaoArquivo">
          <Form.Label className="text-start">Descrição Arquivo</Form.Label>
          <Form.Control placeholder="Informe um identificador para o arquivo importado" />
        </Form.Group>

        <Row className="mb-3">
          <Form.Group controlId="selTipoArquivo">
            <Form.Label>Origem Arquivo</Form.Label>
            <Form.Select>
              <option defaultValue={0}>Escolha a fonte do arquivo</option>
              <option value={1}>Status Invest</option>
              <option value={2}>Fundamentus</option>
            </Form.Select>
          </Form.Group>

          <Form.Group controlId="edtDataBase">
            <Form.Label>Data Base</Form.Label>
            <Form.Control type="date"/>
          </Form.Group>
        </Row>

        <Form.Group className="mt-3 mb-3" controlId="edtArquivo">
          <Form.Label>Arquivo Importação</Form.Label>
          <Form.Control type="file" placeholder="Selecione o arquivo para importação" />
        </Form.Group>

        <Button variant="success" type="submit">
          Importar
        </Button>
      </Form>
    </div>
  )
}

export default Importador;