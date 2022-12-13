import { Navigate, Outlet } from "react-router-dom";

import { validaSeTokenEstaExpirado } from "../../utils/funcoesLogin";

const PrivateRoute = ({ setUserLogado }) => {
  return validaSeTokenEstaExpirado(setUserLogado) ? (
    <Outlet />
  ) : (
    <Navigate to="/" />
  );
};

export default PrivateRoute;
