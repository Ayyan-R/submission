import React, { useState } from "react";
import { useNavigate } from "react-router-dom";


function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [roles] = useState(["User"]);
  const [token, setToken] = useState("");
  const [users, setUsers] = useState([]);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
 

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    setUsers([]);
    setLoading(true);

    try {
      // 1️⃣ Login API
      const response = await fetch("https://localhost:7298/api/Auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password, roles }),
      });

      if (!response.ok) throw new Error("Invalid credentials");

      const data = await response.json();
      if (!data.token) throw new Error("Token not received from API");

      setToken(data.token);
      localStorage.setItem("token", data.token);
      

      // 2️⃣ Get User Data using Token
      const userRes = await fetch("https://localhost:7298/api/Users", {
        headers: { Authorization: `Bearer ${data.token}` },
      });

      if (!userRes.ok) throw new Error("Failed to fetch users");

      const userData = await userRes.json();
      setUsers(userData);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={styles.container}>
      <h2 style={styles.title}>🔐 Login</h2>
      <form onSubmit={handleSubmit} style={styles.form}>
        <input
          type="email"
          placeholder="Email"
          style={styles.input}
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <input
          type="password"
          placeholder="Password"
          style={styles.input}
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
        <button type="submit" style={styles.button} disabled={loading}>
          {loading ? "Logging in..." : "Login"}
        </button>
      </form>

      {error && <p style={styles.error}>{error}</p>}

      {users.length > 0 && (
        <div style={styles.tableContainer}>
          <h3 style={styles.subtitle}>👥 User List (Fetched from API)</h3>
          <table style={styles.table}>
            <thead>
              <tr>
                <th style={styles.th}>ID</th>
                <th style={styles.th}>Name</th>
                <th style={styles.th}>Email</th>
                <th style={styles.th}>Role</th>
              </tr>
            </thead>
            <tbody>
              {users.map((u) => (
                <tr key={u.id}>
                  <td style={styles.td}>{u.id}</td>
                  <td style={styles.td}>{u.name || "N/A"}</td>
                  <td style={styles.td}>{u.email}</td>
                  <td style={styles.td}>{u.role || "User"}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}

const styles = {
  container: {
    margin: "40px auto",
    maxWidth: "500px",
    padding: "25px",
    border: "1px solid #ccc",
    borderRadius: "12px",
    boxShadow: "0 4px 10px rgba(0,0,0,0.1)",
    fontFamily: "Arial, sans-serif",
    backgroundColor: "#fafafa",
  },
  title: {
    textAlign: "center",
    color: "#333",
  },
  form: {
    display: "flex",
    flexDirection: "column",
    gap: "12px",
  },
  input: {
    padding: "10px",
    fontSize: "15px",
    border: "1px solid #aaa",
    borderRadius: "8px",
  },
  button: {
    padding: "10px",
    fontSize: "16px",
    backgroundColor: "#007bff",
    color: "white",
    border: "none",
    borderRadius: "8px",
    cursor: "pointer",
  },
  error: {
    color: "red",
    textAlign: "center",
    marginTop: "10px",
  },
  tableContainer: {
    marginTop: "30px",
  },
  subtitle: {
    textAlign: "center",
    color: "#333",
  },
  table: {
    width: "100%",
    borderCollapse: "collapse",
    marginTop: "10px",
  },
  th: {
    border: "1px solid #ddd",
    padding: "8px",
    backgroundColor: "#007bff",
    color: "white",
    textAlign: "left",
  },
  td: {
    border: "1px solid #ddd",
    padding: "8px",
  },
};

export default Login;
