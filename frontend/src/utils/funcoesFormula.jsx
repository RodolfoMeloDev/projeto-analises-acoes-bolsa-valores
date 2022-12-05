import apiFormulas from "../api/formulas";

export async function getTickersGreenblatt(filters) {
  const response = await apiFormulas.post("Greenblatt", filters, {
    headers: {
      Authorization: "Bearer " + localStorage.getItem("token"),
      "Content-Type": "application/json",
    },
  });

  if (response.status === 200) {
    return response.data;
  }

  return [];
}

export async function getTickersGordon(filters) {
  const response = await apiFormulas.post("ValuetionByGordon", filters, {
    headers: {
      Authorization: "Bearer " + localStorage.getItem("token"),
      "Content-Type": "application/json",
    },
  });

  if (response.status === 200) {
    return response.data;
  }

  return [];
}

export async function getTickersCompareFormulas(filters) {
  const response = await apiFormulas.post("TickersAnalisys", filters, {
    headers: {
      Authorization: "Bearer " + localStorage.getItem("token"),
      "Content-Type": "application/json",
    },
  });

  if (response.status === 200) {
    return response.data;
  }

  return null;
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
