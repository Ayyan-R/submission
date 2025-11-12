import React, { useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";

function JwtDecodeComponent() {
  const [decoded, setDecoded] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      try {
        const decodedToken = jwtDecode(token);
        setDecoded(decodedToken);
      } catch (err) {
        console.error("Invalid token:", err);
      }
    }
  }, []);

  if (!decoded) {
    return <p style={{ textAlign: "center" }}>No valid token found 🔒</p>;
  }

  return (
    <div style={styles.container}>
      <h2 style={styles.title}>🔍 JWT Token Details</h2>
      <table style={styles.table}>
        <tbody>
          {Object.entries(decoded).map(([key, value]) => (
            <tr key={key}>
              <td style={styles.key}>{key}</td>
              <td style={styles.value}>{String(value)}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

const styles = {
  container: {
    margin: "40px auto",
    maxWidth: "600px",
    background: "#f8f9fa",
    padding: "20px",
    borderRadius: "10px",
    boxShadow: "0 4px 10px rgba(0,0,0,0.1)",
    fontFamily: "Arial, sans-serif",
  },
  title: {
    textAlign: "center",
    color: "#333",
    marginBottom: "20px",
  },
  table: {
    width: "100%",
    borderCollapse: "collapse",
  },
  key: {
    fontWeight: "bold",
    padding: "8px",
    backgroundColor: "#007bff",
    color: "white",
    width: "40%",
  },
  value: {
    padding: "8px",
    backgroundColor: "#ffffff",
  },
};

export default JwtDecodeComponent;
