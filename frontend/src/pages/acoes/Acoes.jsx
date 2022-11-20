import { useCallback, useEffect, useState } from 'react';
import { Row, Table } from "react-bootstrap";

import apiBaseTicker from "../../api/baseTicker";

import SelectSegmentos from "../../components/segmentos/SelectSegmentos";
import SelectSetores from "../../components/setores/SelectSetores";
import SelectSubSetores from "../../components/subSetores/SelectSubSetores";

const Acoes = () => {
  const [valueSetor, setValueSetor] = useState(0);
  const [valueSubSetor, setValueSubSetor] = useState(0);
  const [valueSegmento, setValueSegmento] = useState(0);
  const [baseTicker, setBaseTicker] = useState([]);

  const getValueSetor = (idComponente) => {
    setValueSetor(document.getElementById(idComponente).value)
  }

  const getValueSubSetor = (idComponente) => {
    setValueSubSetor(document.getElementById(idComponente).value)
  }

  const getValueSegmento = (idComponente) => {
    setValueSegmento(document.getElementById(idComponente).value)
  }

  const getBaseTicker = async (setor, subSetor, segmento) => {
    let response = null;

    if (segmento !== 0 && segmento !== "0"){
      response = await apiBaseTicker.get("BySegment/"+segmento);
    }else if (subSetor !== 0 && subSetor !== "0"){
      response = await apiBaseTicker.get("BySubSector/"+subSetor);
    }else if (setor !== 0 && setor !== "0"){
      response = await apiBaseTicker.get("BySector/"+setor);
    }else{
      response = await apiBaseTicker.get("");
    }

    if (response.status === 200) {
      return response.data;
    }
  }

  const dataBaseTickers = useCallback(async () => {    
      const baseTickers = await getBaseTicker(valueSetor, valueSubSetor, valueSegmento);
      setBaseTicker(baseTickers);
  }, [valueSetor, valueSubSetor, valueSegmento]);

  useEffect(() => {
    dataBaseTickers().catch(console.error);
  }, [dataBaseTickers]);

  useEffect(() => {
      setValueSubSetor(0)    
  }, [valueSetor])

  useEffect(() => {
    setValueSegmento(0)  
  }, [valueSubSetor])  

  return (
    <>
      <div id="filtros" className="mt-3">
        <h3>Filtros:</h3>
        <Row >
          <SelectSetores idSelect="selSetor" tamanhoSelect="4" getValue={getValueSetor} />
          <SelectSubSetores idSelect="selSubSetor" tamanhoSelect="4" getValue={getValueSubSetor} valueLink={valueSetor} />
          <SelectSegmentos idSelect="selSegmento" tamanhoSelect="4" getValue={getValueSegmento} valueLink={valueSubSetor} />
        </Row>
        <Table className='mt-3' striped bordered hover responsive>
          <thead>
            <tr>
              <th style={{width: "20%"}}>Ticker</th>
              <th>Empresa</th>
            </tr>
          </thead>
          <tbody>
            {baseTicker
              .sort((a,b) => a.baseTicker.localeCompare(b))
              .map((ticker) => {
                return(
                  <tr>
                    <td>{ticker.baseTicker}</td>
                    <td>{ticker.company}</td>
                  </tr>
                );
              }) 
            }
            <tr>
              <td>Mark</td>
              <td>Otto</td>
            </tr>
            <tr>
              <td>Jacob</td>
              <td>Thornton</td>
            </tr>
          </tbody>
        </Table>
      </div>
    </>
  );
};

export default Acoes;
