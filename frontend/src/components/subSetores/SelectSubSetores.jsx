import { useCallback, useEffect, useState } from "react";
import { Col, Form } from "react-bootstrap";
import apiSubSetor from "../../api/subSectors";

const SelectSubSetores = ({ idSelect, tamanhoSelect, getValue, valueLink }) => {
  const [subSetores, setSubSetores] = useState([]);
  const [disabled, setDisabled] = useState(false);

  const getSubSetores = async (setor) => {
    let response = null;    
    if (setor === undefined)
      response = await apiSubSetor.get()
    else
      response = await apiSubSetor.get("Setor/"+setor);

    if (response.status === 200) {
      return response.data;
    }
  };

  const dataSubSectors = useCallback(async () => {  
    if (valueLink === 0 || valueLink === ""){
      setSubSetores([])
    }else{
      const subSetores = await getSubSetores(valueLink);
      setSubSetores(subSetores);
    }
  }, [valueLink]);

  useEffect(() => {
    dataSubSectors().catch(console.error);
  }, [dataSubSectors]);

  useEffect(() => {
    if (valueLink === 0 || valueLink === "0")
      setDisabled(true)
    else
      setDisabled(false);
  }, [valueLink])

  return (
    <Form.Group as={Col} md={tamanhoSelect} controlId={idSelect}>
      <Form.Label><strong>SubSetores</strong></Form.Label>
      <Form.Select aria-label="Selecione um SubSetor" disabled={disabled} onChange={ getValue === undefined ? null : () => getValue(idSelect)}>        
        <option key={0} value={0}>{ valueLink === undefined ? "TODOS OS SUBSETORES" : "SELECIONE UM SETOR" }</option>
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
