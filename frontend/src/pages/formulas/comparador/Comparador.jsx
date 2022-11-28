import React from "react";
import { useParams } from "react-router-dom";

const Comparador = () => {
  let { id } = useParams();

  return <div>Comparador {id}</div>;
};

export default Comparador;
