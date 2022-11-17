import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";

import Home from "./pages/home/Home";
import Acoes from "./pages/acoes/Acoes";
import Setores from "./pages/setores/Setores";
import FormasAnalise from "./pages/formasAnalise/FormasAnalise";
import Dashboard from "./pages/dashboard/Dashboard";

import { Route, Routes } from "react-router-dom";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/acoes" element={<Acoes />} />
      <Route path="/setores" element={<Setores />} />
      <Route path="/formasAnalise" element={<FormasAnalise />} />
      <Route path="/dashboard" element={<Dashboard />} />
    </Routes>
  );
}

export default App;
