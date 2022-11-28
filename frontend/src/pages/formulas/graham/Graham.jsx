import React from "react";
import { useParams } from "react-router-dom";

const Graham = () => {
  const { id } = useParams();
  return <div>Graham {id}</div>;
};

export default Graham;
