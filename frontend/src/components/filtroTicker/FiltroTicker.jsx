import { useCallback, useEffect, useState } from "react";
import { FormGroup, FormLabel, FormSelect } from "react-bootstrap";

import apiTickers from "../../api/tickers";
import "./filtroticker.css";

const FiltroTicker = ({ values, setValues }) => {
  const [tickers, setTickers] = useState([]);

  const getTickers = async () => {
    const response = await apiTickers.get("Complete", {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    });

    if (response.status === 200) {
      return response.data;
    }
  };

  const dataSectors = useCallback(async () => {
    const setores = await getTickers();
    setTickers(setores);
  }, []);

  useEffect(() => {
    dataSectors().catch(console.error);
  }, [dataSectors]);

  const handleSelect = (e) => {
    const { value } = e.target;
    setValues({
      ...values,
      ticker: value,
    });
  };

  return (
    <FormGroup
      controlId="selFiltroTicker"
      className="border rounded p-2 filtro-ticker"
    >
      <FormLabel className="d-flex justify-content-between">
        <strong>Ticker:</strong>
      </FormLabel>
      <FormSelect
        aria-label="Selecione um Ticker"
        onChange={handleSelect}
        value={values.ticker}
      >
        <option key={0} value={""}>
          TODOS OS TICKERS
        </option>
        {tickers.map((ticker) => {
          return (
            <option key={ticker.id} value={ticker.ticker}>
              {ticker.ticker} - {ticker.baseTicker.company}
            </option>
          );
        })}
      </FormSelect>
    </FormGroup>
  );
};

export default FiltroTicker;
