import { useCallback, useEffect, useState } from 'react';
import { Pagination, Row, Table } from "react-bootstrap";

import apiBaseTicker from "../../api/baseTicker";

import SelectSegmentos from "../../components/segmentos/SelectSegmentos";
import SelectSetores from "../../components/setores/SelectSetores";
import SelectSubSetores from "../../components/subSetores/SelectSubSetores";

const Acoes = () => {
  const [valueSetor, setValueSetor] = useState(0);
  const [valueSubSetor, setValueSubSetor] = useState(0);
  const [valueSegmento, setValueSegmento] = useState(0);
  const [baseTickers, setBaseTickers] = useState([]);
  const [baseTickersFiltrados, setBaseTickersFiltrados] = useState(baseTickers);

  const [botoesPaginacao, setBotoesPaginacao] = useState([]);
  const [paginaAtiva, setPaginaAtiva] = useState(1);

  const getValueSetor = (idComponente) => {
    setValueSetor(document.getElementById(idComponente).value)
  }

  const getValueSubSetor = (idComponente) => {
    setValueSubSetor(document.getElementById(idComponente).value)
  }

  const getValueSegmento = (idComponente) => {
    setValueSegmento(document.getElementById(idComponente).value)
  }

  const getBaseTicker = async () => {
    const response = await apiBaseTicker.get("Complete");

    if (response.status === 200) {
      return response.data;
    }
  }

  const dataBaseTickers = useCallback(async () => {    
      const baseTickers = await getBaseTicker();      
      setBaseTickers(baseTickers);
      setBaseTickersFiltrados(baseTickers);
  }, []);

  useEffect(() => {
    dataBaseTickers().catch(console.error);    
  }, [dataBaseTickers]);

  useEffect(() => {
      setValueSubSetor(0);      
      if (valueSetor === 0 || valueSetor === "0")
        setBaseTickersFiltrados(baseTickers)
      else{
        const listaBaseTickres = baseTickers;           
        setBaseTickersFiltrados(listaBaseTickres.filter((el) => el.segment.subSector.sector.id === parseInt(valueSetor)));
      }   
  }, [valueSetor, baseTickers])

  useEffect(() => {
    setValueSegmento(0)  
    if (parseInt(valueSubSetor) > 0){
      const listaBaseTickres = baseTickers;           
      setBaseTickersFiltrados(listaBaseTickres.filter((el) => el.segment.subSector.id === parseInt(valueSubSetor)));
    }else{
      const listaBaseTickres = baseTickers;           
      setBaseTickersFiltrados(listaBaseTickres.filter((el) => el.segment.subSector.sector.id === parseInt(valueSetor)));
    }
  }, [valueSubSetor])  

  useEffect(() => {
    if (parseInt(valueSegmento) > 0){
      const listaBaseTickres = baseTickers;           
      setBaseTickersFiltrados(listaBaseTickres.filter((el) => el.segmentId === parseInt(valueSegmento)));
    }else{
      const listaBaseTickres = baseTickers;           
      setBaseTickersFiltrados(listaBaseTickres.filter((el) => el.segment.subSector.id === parseInt(valueSubSetor)));
    }
  }, [valueSegmento]);

  const montaBotoes = (qtdeBotoes) => {
    let botoes = [];

    for (let index = 0; index < qtdeBotoes; index++) {
      if (qtdeBotoes > 8 ){
        if (index === 8){
          setBotoesPaginacao(botoes);
          break;
        }
        else
          botoes.push(<Pagination.Item onClick={((e) => setPage(e))} key={index+1} active={paginaAtiva === index +1}>{index + 1}</Pagination.Item>)
      }else{
        botoes.push(<Pagination.Item onClick={((e) => setPage(e))} key={index+1} active={paginaAtiva === index +1}>{index + 1}</Pagination.Item>)        
      }
    }

    setBotoesPaginacao(botoes);
  }

  useEffect(() => {
    montaBotoes(Math.trunc(baseTickersFiltrados.length / 10) + ( baseTickersFiltrados.length % 10 === 0 ? 0 : 1));
    setPaginaAtiva(1);
  }, [baseTickersFiltrados])

  const setNextPage = () => {
    const qtdeBotoes = Math.trunc(baseTickersFiltrados.length / 10) + ( baseTickersFiltrados.length % 10 === 0 ? 0 : 1);
    if (paginaAtiva+1 <= qtdeBotoes )
      setPaginaAtiva(paginaAtiva+1)
  }

  const setPreviusPage = () => {
    if (paginaAtiva-1 > 0)
      setPaginaAtiva(paginaAtiva-1)
  }

  const setFirstPage = () => {
    setPaginaAtiva(1)
  }

  const setLastPage = () => {
    setPaginaAtiva(Math.trunc(baseTickersFiltrados.length / 10) + ( baseTickersFiltrados.length % 10 === 0 ? 0 : 1))
  }

  const setPage = (e) => {
    setPaginaAtiva(parseInt(e.target.text))
}

  useEffect(() => {
    montaBotoes(Math.trunc(baseTickersFiltrados.length / 10) + ( baseTickersFiltrados.length % 10 === 0 ? 0 : 1));
  }, [paginaAtiva])

  return (
    <>
      <div id="filtros" className="mt-3">
        <h3>Filtros:</h3>
        <Row >
          <SelectSetores idSelect="selSetor" tamanhoSelect="4" getValue={getValueSetor} />
          <SelectSubSetores idSelect="selSubSetor" tamanhoSelect="4" getValue={getValueSubSetor} valueLink={valueSetor} />
          <SelectSegmentos idSelect="selSegmento" tamanhoSelect="4" getValue={getValueSegmento} valueLink={valueSubSetor} />
        </Row>        
      </div>
      <Table className='mt-3' striped bordered hover responsive>
        <thead>
          <tr>
            <th style={{width: "5%"}}>Ticker</th>
            <th style={{width: "25%"}}>Empresa</th>
            <th style={{width: "25%"}}>Setor</th>
            <th style={{width: "25%"}}>SubSetor</th>
            <th style={{width: "25%"}}>Segmento</th>
          </tr>
        </thead>
        <tbody>
          {baseTickersFiltrados
            .sort((a, b) => {
              let fa = a.baseTicker.toLowerCase(),
                  fb = b.baseTicker.toLowerCase();
          
              if (fa < fb) {
                  return -1;
              }
              if (fa > fb) {
                  return 1;
              }
              return 0;
            })
            .map((ticker) => {
              return(
                <tr key={ticker.id}>
                  <td>{ticker.baseTicker}</td>
                  <td>{ticker.company}</td>
                  <td>{ticker.segment.subSector.sector.name}</td>
                  <td>{ticker.segment.subSector.name}</td>
                  <td>{ticker.segment.name}</td>
                </tr>
              );
            }) 
          }
        </tbody>
      </Table>
      <Pagination>
        <Pagination.First onClick={setFirstPage}/>
        <Pagination.Prev onClick={setPreviusPage}/>
          {botoesPaginacao}
        <Pagination.Next onClick={setNextPage} />
        <Pagination.Last onClick={setLastPage}/>
      </Pagination>
    </>
  );
};

export default Acoes;
