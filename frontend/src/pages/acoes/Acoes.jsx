import { Row } from "react-bootstrap";

import SelectSegmentos from "../../components/segmentos/SelectSegmentos";
import SelectSetores from "../../components/setores/SelectSetores";
import SelectSubSetores from "../../components/subSetores/SelectSubSetores";

const Acoes = () => {
  return (
    <>
      <Row className="mt-3">
        <SelectSetores idSelect="selSetor" tamanhoSelect="4" />
        <SelectSubSetores idSelect="selSubSetor" tamanhoSelect="4" />
        <SelectSegmentos idSelect="selSegmento" tamanhoSelect="4" />
      </Row>
    </>
  );
};

export default Acoes;
