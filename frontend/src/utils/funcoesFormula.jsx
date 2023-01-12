import apiFormulas from "../api/formulas";
import { refreshTokenExec } from "./funcoesLogin";

export async function getTickersGreenblatt(filters) {
  let retornoDados = null;

  await apiFormulas
    .post("Greenblatt", filters, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    })
    .then((response) => {
      if (response.status === 200) {
        retornoDados = {
          statusCode: response.status,
          mensagem: "Retorno de dados com sucesso!",
          dados: response.data,
        };
      } else {
        retornoDados = {
          statusCode: response.status,
          mensagem: response.data.message,
          dados: null,
        };
      }
    })
    .catch(async function (error) {
      if (error.response.status === 401) {
        const returnRefreshToken = await refreshTokenExec(
          localStorage.getItem("login"),
          localStorage.getItem("refreshToken")
        );

        let responseRefreshToken = null;
        if (returnRefreshToken.statusCode === 200) {
          responseRefreshToken = await getTickersBazin(filters);
          retornoDados = {
            statusCode: responseRefreshToken.statusCode,
            mensagem: "Retorno de dados com sucesso!",
            dados: responseRefreshToken.dados,
          };
        } else {
          retornoDados = {
            statusCode: returnRefreshToken.statusCode,
            mensagem: returnRefreshToken.message,
            dados: null,
          };
        }
      }
    });

  return retornoDados;
}

export async function getTickersGordon(filters) {
  let retornoDados = null;

  await apiFormulas
    .post("ValuetionByGordon", filters, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    })
    .then((response) => {
      if (response.status === 200) {
        retornoDados = {
          statusCode: response.status,
          mensagem: "Retorno de dados com sucesso!",
          dados: response.data,
        };
      } else {
        retornoDados = {
          statusCode: response.status,
          mensagem: response.data.message,
          dados: null,
        };
      }
    })
    .catch(async function (error) {
      if (error.response.status === 401) {
        const returnRefreshToken = await refreshTokenExec(
          localStorage.getItem("login"),
          localStorage.getItem("refreshToken")
        );

        let responseRefreshToken = null;
        if (returnRefreshToken.statusCode === 200) {
          responseRefreshToken = await getTickersBazin(filters);
          retornoDados = {
            statusCode: responseRefreshToken.statusCode,
            mensagem: "Retorno de dados com sucesso!",
            dados: responseRefreshToken.dados,
          };
        } else {
          retornoDados = {
            statusCode: returnRefreshToken.statusCode,
            mensagem: returnRefreshToken.message,
            dados: null,
          };
        }
      }
    });

  return retornoDados;
}

export async function getTickersGraham(filters) {
  let retornoDados = null;

  await apiFormulas
    .post("ValuetionByGraham", filters, {
      headers: {
        accept: "*",
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    })
    .then((response) => {
      if (response.status === 200) {
        retornoDados = {
          statusCode: response.status,
          mensagem: "Retorno de dados com sucesso!",
          dados: response.data,
        };
      } else {
        retornoDados = {
          statusCode: response.status,
          mensagem: response.data.message,
          dados: null,
        };
      }
    })
    .catch(async function (error) {
      if (error.response.status === 401) {
        const returnRefreshToken = await refreshTokenExec(
          localStorage.getItem("login"),
          localStorage.getItem("refreshToken")
        );

        let responseRefreshToken = null;
        if (returnRefreshToken.statusCode === 200) {
          responseRefreshToken = await getTickersBazin(filters);
          retornoDados = {
            statusCode: responseRefreshToken.statusCode,
            mensagem: "Retorno de dados com sucesso!",
            dados: responseRefreshToken.dados,
          };
        } else {
          retornoDados = {
            statusCode: returnRefreshToken.statusCode,
            mensagem: returnRefreshToken.message,
            dados: null,
          };
        }
      }
    });

  return retornoDados;
}

export async function getTickersBazin(filters) {
  let retornoDados = null;

  await apiFormulas
    .post("ValuetionByBazin", filters, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    })
    .then((response) => {
      if (response.status === 200) {
        retornoDados = {
          statusCode: response.status,
          mensagem: "Retorno de dados com sucesso!",
          dados: response.data,
        };
      } else {
        retornoDados = {
          statusCode: response.status,
          mensagem: response.data.message,
          dados: null,
        };
      }
    })
    .catch(async function (error) {
      if (error.response.status === 401) {
        const returnRefreshToken = await refreshTokenExec(
          localStorage.getItem("login"),
          localStorage.getItem("refreshToken")
        );

        let responseRefreshToken = null;
        if (returnRefreshToken.statusCode === 200) {
          responseRefreshToken = await getTickersBazin(filters);
          retornoDados = {
            statusCode: responseRefreshToken.statusCode,
            mensagem: "Retorno de dados com sucesso!",
            dados: responseRefreshToken.dados,
          };
        } else {
          retornoDados = {
            statusCode: returnRefreshToken.statusCode,
            mensagem: returnRefreshToken.message,
            dados: null,
          };
        }
      }
    });

  return retornoDados;
}

export async function getTickersPrecoLucro(filters) {
  let retornoDados = null;

  await apiFormulas
    .post("PriceAndProfit", filters, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    })
    .then((response) => {
      if (response.status === 200) {
        retornoDados = {
          statusCode: response.status,
          mensagem: "Retorno de dados com sucesso!",
          dados: response.data,
        };
      } else {
        retornoDados = {
          statusCode: response.status,
          mensagem: response.data.message,
          dados: null,
        };
      }
    })
    .catch(async function (error) {
      if (error.response.status === 401) {
        const returnRefreshToken = await refreshTokenExec(
          localStorage.getItem("login"),
          localStorage.getItem("refreshToken")
        );

        let responseRefreshToken = null;
        if (returnRefreshToken.statusCode === 200) {
          responseRefreshToken = await getTickersBazin(filters);
          retornoDados = {
            statusCode: responseRefreshToken.statusCode,
            mensagem: "Retorno de dados com sucesso!",
            dados: responseRefreshToken.dados,
          };
        } else {
          retornoDados = {
            statusCode: returnRefreshToken.statusCode,
            mensagem: returnRefreshToken.message,
            dados: null,
          };
        }
      }
    });

  return retornoDados;
}

export async function getTickersEvEbit(filters) {
  let retornoDados = null;

  await apiFormulas
    .post("EvEbit", filters, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    })
    .then((response) => {
      if (response.status === 200) {
        retornoDados = {
          statusCode: response.status,
          mensagem: "Retorno de dados com sucesso!",
          dados: response.data,
        };
      } else {
        retornoDados = {
          statusCode: response.status,
          mensagem: response.data.message,
          dados: null,
        };
      }
    })
    .catch(async function (error) {
      if (error.response.status === 401) {
        const returnRefreshToken = await refreshTokenExec(
          localStorage.getItem("login"),
          localStorage.getItem("refreshToken")
        );

        let responseRefreshToken = null;
        if (returnRefreshToken.statusCode === 200) {
          responseRefreshToken = await getTickersBazin(filters);
          retornoDados = {
            statusCode: responseRefreshToken.statusCode,
            mensagem: "Retorno de dados com sucesso!",
            dados: responseRefreshToken.dados,
          };
        } else {
          retornoDados = {
            statusCode: returnRefreshToken.statusCode,
            mensagem: returnRefreshToken.message,
            dados: null,
          };
        }
      }
    });

  return retornoDados;
}

export async function getTickersCompareFormulas(filters) {
  let retornoDados = null;

  await apiFormulas
    .post("TickersAnalisys", filters, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    })
    .then((response) => {
      if (response.status === 200) {
        retornoDados = {
          statusCode: response.status,
          mensagem: "Retorno de dados com sucesso!",
          dados: response.data,
        };
      } else {
        retornoDados = {
          statusCode: response.status,
          mensagem: response.data.message,
          dados: null,
        };
      }
    })
    .catch(async function (error) {
      if (error.response.status === 401) {
        const returnRefreshToken = await refreshTokenExec(
          localStorage.getItem("login"),
          localStorage.getItem("refreshToken")
        );

        let responseRefreshToken = null;
        if (returnRefreshToken.statusCode === 200) {
          responseRefreshToken = await getTickersBazin(filters);
          retornoDados = {
            statusCode: responseRefreshToken.statusCode,
            mensagem: "Retorno de dados com sucesso!",
            dados: responseRefreshToken.dados,
          };
        } else {
          retornoDados = {
            statusCode: returnRefreshToken.statusCode,
            mensagem: returnRefreshToken.message,
            dados: null,
          };
        }
      }
    });

  return retornoDados;
}

export async function getTickersCompareAllFormulas(filters) {
  let retornoDados = null;

  await apiFormulas
    .post("ListTickersAnalisys", filters, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    })
    .then((response) => {
      if (response.status === 200) {
        retornoDados = {
          statusCode: response.status,
          mensagem: "Retorno de dados com sucesso!",
          dados: response.data,
        };
      } else {
        retornoDados = {
          statusCode: response.status,
          mensagem: response.data.message,
          dados: null,
        };
      }
    })
    .catch(async function (error) {
      if (error.response.status === 401) {
        const returnRefreshToken = await refreshTokenExec(
          localStorage.getItem("login"),
          localStorage.getItem("refreshToken")
        );

        let responseRefreshToken = null;
        if (returnRefreshToken.statusCode === 200) {
          responseRefreshToken = await getTickersBazin(filters);
          retornoDados = {
            statusCode: responseRefreshToken.statusCode,
            mensagem: "Retorno de dados com sucesso!",
            dados: responseRefreshToken.dados,
          };
        } else {
          retornoDados = {
            statusCode: returnRefreshToken.statusCode,
            mensagem: returnRefreshToken.message,
            dados: null,
          };
        }
      }
    });

  return retornoDados;
}

export function retornaLiquidezMediaDiariaTratada(valor) {
  let mediaLiquidezDiaria = (valor / 1000.0).toFixed(2);
  let volumeFinanceiro = "K";

  if (mediaLiquidezDiaria > 1000) {
    mediaLiquidezDiaria = (valor / 1000000.0).toFixed(2);
    volumeFinanceiro = "M";
  }

  if (mediaLiquidezDiaria > 1000) {
    mediaLiquidezDiaria = (valor / 1000000000.0).toFixed(2);
    volumeFinanceiro = "B";
  }

  return (
    parseFloat(mediaLiquidezDiaria).toLocaleString("pt-br", {
      minimumFractionDigits: 2,
    }) +
    " " +
    volumeFinanceiro
  );
}
