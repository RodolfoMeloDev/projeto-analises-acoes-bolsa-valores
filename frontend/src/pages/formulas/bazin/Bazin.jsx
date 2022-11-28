import React from "react";
import { useParams } from "react-router-dom";

const Bazin = () => {
  const { id } = useParams();
  return <div>Bazin {id}</div>;
};

export default Bazin;
