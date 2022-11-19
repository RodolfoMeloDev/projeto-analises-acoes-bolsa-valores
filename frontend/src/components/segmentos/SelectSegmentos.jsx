import { useCallback, useEffect, useState } from "react";
import { Col, Form } from "react-bootstrap";
import apiSegmentos from "../../api/segments";

const SelectSegmentos = ({ idSelect, tamanhoSelect }) => {
  const [segmentos, setSegmentos] = useState([]);

  const getSegmentos = async () => {
    const response = await apiSegmentos.get("", {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    });

    if (response.status === 200) {
      return response.data;
    }
  };

  const dataSegments = useCallback(async () => {
    const segmentos = await getSegmentos();
    setSegmentos(segmentos);
  }, []);

  useEffect(() => {
    dataSegments().catch(console.error);
  }, [dataSegments]);

  return (
    <Form.Group as={Col} md={tamanhoSelect} controlId={idSelect}>
      <Form.Label>Segmentos</Form.Label>
      <Form.Select aria-label="Selecione um Segmento">
        <option key={0} defaultValue={0}></option>
        {segmentos
          .sort((a, b) => a.name.localeCompare(b.name))
          .map((segmento) => {
            return (
              <option key={segmento.id} value={segmento.id}>
                {segmento.name}
              </option>
            );
          })}
      </Form.Select>
    </Form.Group>
  );
};

export default SelectSegmentos;
