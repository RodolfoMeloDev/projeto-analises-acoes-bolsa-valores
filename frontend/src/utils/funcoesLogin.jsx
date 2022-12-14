import apiLogin from "../api/login";

export function usuarioToken() {
  if (!validaSeTokenEstaExpirado()) return "";

  return localStorage.getItem("nickName");
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
        localStorage.setItem("refreshToken", login.refreshToken);
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

export function validaSeTokenEstaExpirado(setUserLogado) {
  const today = new Date(Date.now());
  let dataValidade = localStorage.getItem("data-validade");

  if (dataValidade === "null" || dataValidade === null) return false;

  dataValidade = new Date(
    dataValidade.substring(0, 4),
    dataValidade.substring(5, 7) - 1,
    dataValidade.substring(8, 10),
    dataValidade.substring(11, 13),
    dataValidade.substring(14, 16),
    dataValidade.substring(17, 19)
  );

  if (today > dataValidade) {
    localStorage.setItem("data-validade", null);
    localStorage.setItem("token", null);
    localStorage.setItem("refreshToken", null);
    localStorage.setItem("nickName", null);
    localStorage.setItem("login", null);
    setUserLogado("");
    return false;
  }

  return true;
}

export async function refreshTokenExec(userLogin, refreshToken) {
  try {
    const response = await apiLogin.put("RefreshToken", {
      login: userLogin,
      refreshToken: refreshToken,
    });

    if (response.status === 200) {
      localStorage.setItem("data-validade", response.data.expiration);
      localStorage.setItem("token", response.data.accessToken);
      localStorage.setItem("refreshToken", response.data.refreshToken);

      return {
        statusCode: response.status,
        message: "Token atualizado com sucesso!",
      };
    } else {
      return {
        statusCode: response.status,
        message: "Não foi possível atualizar o token!",
      };
    }
  } catch (error) {
    return {
      statusCode: error.response.status,
      message: "Não foi possível atualizar o token!",
    };
  }
}
