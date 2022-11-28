import React from "react";
import { useParams } from "react-router-dom";

const Evbit = () => {
  const { id } = useParams();
  return <div>Evbit {id}</div>;
};

export default Evbit;
