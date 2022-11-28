import React from "react";
import { useParams } from "react-router-dom";

const Greenblatt = () => {
  const { id } = useParams();
  return <div>Greenblatt {id}</div>;
};

export default Greenblatt;
