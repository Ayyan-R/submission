import React, { useEffect, useState } from "react";

function Backend() {
  const [data, setData] = useState([]);

  useEffect(() => {
    fetch("https://localhost:7298/api/Auth/Register")
      .then((response) => response.json())
      .then((json) => setData(json))
      .catch((error) => console.error("Error:", error));
  }, []);

  return (
    <div>
      <h1>Data from ASP.NET Core API</h1>
      <ul>
        {data.map((item, index) => (
          <li key={index}>{item.summary} - {item.temperatureC}°C</li>
        ))}
      </ul>
    </div>
  );
}

export default Backend;
