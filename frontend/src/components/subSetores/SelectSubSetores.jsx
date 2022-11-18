import { useCallback, useEffect, useState } from "react";
import { Col, Form } from "react-bootstrap";
import apiSubSetor from "../../api/subSectors";

const SelectSubSetores = ({ idSelect, tamanhoSelect }) => {
  const [subSetores, setSubSetores] = useState([]);

  const getSubSetores = async () => {
    const response = await apiSubSetor.get("", {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    });

    if (response.status === 200) {
      return response.data;
    }
  };

  const dataSubSectors = useCallback(async () => {
    const subSetores = await getSubSetores();
    setSubSetores(subSetores);
  }, []);

  useEffect(() => {
    dataSubSectors().catch(console.error);
  }, [dataSubSectors]);

  return (
    <Form.Group as={Col} md={tamanhoSelect} controlId={idSelect}>
      <Form.Label>SubSetores</Form.Label>
      <Form.Select aria-label="Selecione um SubSetor">
        <option key={0} defaultValue={0}></option>
        {subSetores
          .sort((a, b) => a.name.localeCompare(b.name))
          .map((subSetor) => {
            return (
              <option key={subSetor.id} value={subSetor.id}>
                {subSetor.name}
              </option>
            );
          })}
      </Form.Select>
    </Form.Group>
  );
};

export default SelectSubSetores;
