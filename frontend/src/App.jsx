import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";

import Home from "./pages/home/Home";
import Acoes from "./pages/acoes/Acoes";
import Dashboard from "./pages/dashboard/Dashboard";
import Importador from "./pages/importador/Importador";
import Comparador from "./pages/formulas/comparador/Comparador";
import ComparadorGeral from "./pages/formulas/comparadorGeral/ComparadorGeral";
import Greenblatt from "./pages/formulas/greenblatt/Greenblatt";
import Precolucro from "./pages/formulas/precolucro/PrecoLucro";
import Evebit from "./pages/formulas/evebit/Evbit";
import Bazin from "./pages/formulas/bazin/Bazin";
import Graham from "./pages/formulas/graham/Graham";
import Gordon from "./pages/formulas/gordon/Gordon";

import { Navigate, Route, Routes } from "react-router-dom";
import PrivateRoute from "./components/privateRoute/PrivateRoute";
import Menu from "./components/menu/Menu";
import { useEffect, useState } from "react";
import { usuarioToken } from './utils/funcoesLogin';

function App() {
  const [userLogado, setUserLogado] = useState("");

  const logout = () => {
    localStorage.setItem("data-validade", null);
    localStorage.setItem("token", null);
    localStorage.setItem("refreshToken", null);
    localStorage.setItem("nickName", null);
    localStorage.setItem("login", null);
    setUserLogado("");
    Navigate("/");
  };

  useEffect(() => {    
    setUserLogado(usuarioToken());
  },[]);
  
  return (
    <>
      <Menu user={userLogado} logout={logout} setUserLogado={setUserLogado} />
      <div className="container">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/acoes" element={<Acoes />} />

          <Route
            path="/dashboard"
            element={<PrivateRoute setUserLogado={setUserLogado} />}
          >
            <Route path="/dashboard" element={<Dashboard logout={logout} />} />
          </Route>

          <Route
            path="/importador"
            element={<PrivateRoute setUserLogado={setUserLogado} />}
          >
            <Route path="/importador" element={<Importador logout={logout} />} />
          </Route>

          <Route
            path="/formula/comparador"
            element={<PrivateRoute setUserLogado={setUserLogado} />}
          >
            <Route path="/formula/comparador" element={<Comparador logout={logout} />} />
          </Route>

          <Route
            path="/formula/comparadorGeral"
            element={<PrivateRoute setUserLogado={setUserLogado} />}
          >
            <Route
              path="/formula/comparadorGeral"
              element={<ComparadorGeral logout={logout} />}
            />
          </Route>

          <Route
            path="/formula/greenblatt"
            element={<PrivateRoute setUserLogado={setUserLogado} />}
          >
            <Route path="/formula/greenblatt" element={<Greenblatt logout={logout} />} />
          </Route>

          <Route
            path="/formula/pl"
            element={<PrivateRoute setUserLogado={setUserLogado} />}
          >
            <Route path="/formula/pl" element={<Precolucro logout={logout} />} />
          </Route>

          <Route
            path="/formula/evEbit"
            element={<PrivateRoute setUserLogado={setUserLogado} />}
          >
            <Route path="/formula/evEbit" element={<Evebit logout={logout} />} />
          </Route>

          <Route
            path="/formula/bazin"
            element={<PrivateRoute setUserLogado={setUserLogado} />}
          >
            <Route path="/formula/bazin" element={<Bazin logout={logout} />} />
          </Route>

          <Route
            path="/formula/graham"
            element={<PrivateRoute setUserLogado={setUserLogado} />}
          >
            <Route path="/formula/graham" element={<Graham logout={logout} />} />
          </Route>

          <Route
            path="/formula/gordon"
            element={<PrivateRoute setUserLogado={setUserLogado} />}
          >
            <Route path="/formula/gordon" element={<Gordon logout={logout} />} />
          </Route>
        </Routes>
      </div>
    </>
  );
}

export default App;
