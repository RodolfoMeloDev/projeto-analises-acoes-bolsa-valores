import React from "react";
import { useParams } from "react-router-dom";

const PrecoLucro = () => {
  const { id } = useParams();
  return <div>PrecoLucro {id}</div>;
};

export default PrecoLucro;
