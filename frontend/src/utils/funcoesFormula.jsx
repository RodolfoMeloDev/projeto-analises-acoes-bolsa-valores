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
