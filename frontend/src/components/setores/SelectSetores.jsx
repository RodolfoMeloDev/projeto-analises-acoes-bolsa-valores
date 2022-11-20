import { useCallback, useEffect, useState } from "react";
import { Col, Form } from "react-bootstrap";
import apiSetor from "../../api/sectors";

const SelectSetores = ({ idSelect, tamanhoSelect, getValue }) => {
  const [setores, setSetores] = useState([]);

  const getSetores = async () => {
    const response = await apiSetor.get("");

    if (response.status === 200) {
      return response.data;
    }
  };

  const dataSectors = useCallback(async () => {
    const setores = await getSetores();
    setSetores(setores);
  }, []);

  useEffect(() => {
    dataSectors().catch(console.error);
  }, [dataSectors]);

  return (
    <Form.Group as={Col} md={tamanhoSelect} controlId={idSelect}>
      <Form.Label><strong>Setores</strong></Form.Label>
      <Form.Select aria-label="Selecione um Setor" onChange={ getValue === undefined ? null : () => getValue(idSelect)}>
        <option key={0} Value={0}>TODOS OS SETORES</option>
        {setores
          .sort((a, b) => a.name.localeCompare(b.name))
          .map((setor) => {
            return (
              <option key={setor.id} value={setor.id}>
                {setor.name}
              </option>
            );
          })}
      </Form.Select>
    </Form.Group>
  );
};

export default SelectSetores;
