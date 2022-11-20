import { useCallback, useEffect, useState } from "react";
import { Col, Form } from "react-bootstrap";
import apiSegmentos from "../../api/segments";

const SelectSegmentos = ({ idSelect, tamanhoSelect, getValue, valueLink }) => {
  const [segmentos, setSegmentos] = useState([]);
  const [disabled, setDisabled] = useState(false);

  const getSegmentos = async (segmento) => {
    let response = null;
    if (segmento === undefined)
      response = await apiSegmentos.get("")
    else
      response = await apiSegmentos.get("SubSetor/"+segmento);

    if (response.status === 200) {
      return response.data;
    }
  };

  const dataSegments = useCallback(async () => {
    if (valueLink === 0 || valueLink === ""){
      setSegmentos([])
    }else{
      const segmentos = await getSegmentos(valueLink);
      setSegmentos(segmentos);
    }
  }, [valueLink]);

  useEffect(() => {
    dataSegments().catch(console.error);
  }, [dataSegments]);

  useEffect(() => {
    if (valueLink === 0 || valueLink === "0")
      setDisabled(true)
    else
      setDisabled(false);
  }, [valueLink])

  return (
    <Form.Group as={Col} md={tamanhoSelect} controlId={idSelect}>
      <Form.Label><strong>Segmentos</strong></Form.Label>
      <Form.Select aria-label="Selecione um Segmento" disabled={disabled} onChange={ getValue === undefined ? null : () => getValue(idSelect) } >
        <option key={0} value={0}>{ valueLink === undefined ? "TODOS OS SEGMENTOS" : "SELECIONE UM SUBSETOR" }</option>
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
