import React from "react";
import { useParams } from "react-router-dom";

const Gordon = () => {
  const { id } = useParams();
  return <div>Gordon {id}</div>;
};

export default Gordon;
