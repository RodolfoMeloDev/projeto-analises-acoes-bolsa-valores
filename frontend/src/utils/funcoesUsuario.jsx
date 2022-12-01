import apiUsuario from "../api/users";

export async function retornarDadosUsuarioLogado() {
  return await apiUsuario.get(localStorage.getItem("login"), {
    headers: {
      Authorization: "Bearer " + localStorage.getItem("token"),
    },
  });
}
