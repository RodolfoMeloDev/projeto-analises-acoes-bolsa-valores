import apiLogin from "../api/login";

export function usuarioToken() {
  const today = new Date(Date.now());
  let dataValidade = localStorage.getItem("data-validade");

  // caso não tenha acessado ainda
  if (dataValidade === "null" || dataValidade === null) return "";

  dataValidade = new Date(
    dataValidade.substring(0, 4),
    dataValidade.substring(5, 7) - 1,
    dataValidade.substring(8, 10),
    dataValidade.substring(11, 13),
    dataValidade.substring(14, 16),
    dataValidade.substring(17, 19)
  );

  if (today > dataValidade) return "";
  else {
    return localStorage.getItem("nickName");
  }
}

export async function getLogin(userLogin, userPassword) {
  const user = usuarioToken();

  if (user === "") {
    try {
      const response = await apiLogin.post("", {
        login: userLogin,
        password: userPassword,
      });

      if (response.status === 200 && response.data.authenticated) {
        const login = response.data;
        const nameUser =
          login.nickName !== "" && login.nickName !== null
            ? login.nickName
            : login.name.substr(0, login.name.search(" "));

        localStorage.setItem("data-validade", login.expiration);
        localStorage.setItem("token", login.accessToken);
        localStorage.setItem("nickName", nameUser);
        localStorage.setItem("login", userLogin);

        return { logado: true, user: nameUser };
      } else {
        return { logado: false, error: response.data.message };
      }
    } catch {
      return {
        logado: false,
        error: "Deve ser o informado E-mail/Senha para realizar o Login!",
      };
    }
  } else {
    return { logado: true, user: user };
  }
}
