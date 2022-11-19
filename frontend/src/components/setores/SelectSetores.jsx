import { useCallback, useEffect, useState } from "react";
import { Col, Form } from "react-bootstrap";
import apiSetor from "../../api/sectors";

const SelectSetores = ({ idSelect, tamanhoSelect }) => {
  const [setores, setSetores] = useState([]);

  const getSetores = async () => {
    const response = await apiSetor.get("", {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    });

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
      <Form.Label>Setores</Form.Label>
      <Form.Select aria-label="Selecione um Setor">
        <option key={0} defaultValue={0}></option>
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
