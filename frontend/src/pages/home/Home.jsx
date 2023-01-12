import React, { useEffect } from "react";
import { refreshTokenExec } from '../../utils/funcoesLogin';

const Home = () => {
  useEffect(() => {    
    async function atualizaRefreshToken(){
      console.log('antigo:' +  localStorage.getItem("refreshToken"));
      
      if (localStorage.getItem("login") === "null" || localStorage.getItem("login") === null)
        return;

      await refreshTokenExec(localStorage.getItem("login"), localStorage.getItem("refreshToken"))
      
      console.log('novo:' +  localStorage.getItem("refreshToken"));
    }

    atualizaRefreshToken();
  },[]);

  return (
    <>
      <br />
      <br />
      <br />
      <br />
      <br />
      <br />
      <h1>
        Aqui terá uma explicação melhor sobre o projeto e como ele funciona
      </h1>
    </>
  );
};

export default Home;
