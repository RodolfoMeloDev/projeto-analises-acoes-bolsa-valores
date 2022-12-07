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

import { Route, Routes } from "react-router-dom";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/acoes" element={<Acoes />} />
      <Route path="/dashboard" element={<Dashboard />} />
      <Route path="/importador" element={<Importador />} />
      <Route path="/formula/comparador" element={<Comparador />} />
      <Route path="/formula/comparadorGeral" element={<ComparadorGeral />} />
      <Route path="/formula/greenblatt" element={<Greenblatt />} />
      <Route path="/formula/pl" element={<Precolucro />} />
      <Route path="/formula/evEbit" element={<Evebit />} />
      <Route path="/formula/bazin" element={<Bazin />} />
      <Route path="/formula/graham" element={<Graham />} />
      <Route path="/formula/gordon" element={<Gordon />} />
    </Routes>
  );
}

export default App;
