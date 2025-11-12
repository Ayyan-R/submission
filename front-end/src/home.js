import React from "react";
import { useState,useEffect } from "react";
import JwtDecodeComponent from "./decodedjwt";
function Home() {
    const [role, setRole] = useState("");
    const [data, setData] = useState([]);
    const [error, setError] = useState("");
  
    useEffect(() => {
      const token = localStorage.getItem("token");
      if (!token) {
        setError("No token found. Please log in again.");
        return;
      }
  
      try {
        const decoded = jwtDecode(token);
        setRole(decoded.role || decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
  
        // ✅ Fetch data based on role
        if (decoded.role === "User") {
          fetchData("https://localhost:7298/api/Users/GetAll", token);
        } else if (decoded.role === "User") {
          fetchData("https://localhost:7298/api/Profile/GetMyDetails", token);
        } else {
          setError("Unauthorized role");
        }
      } catch (err) {
        setError("Invalid token format");
      }
    }, []);
  
    const fetchData = async (url, token) => {
      try {
        const res = await fetch(url, {
          headers: { Authorization: `Bearer ${token}` },
        });
        if (!res.ok) throw new Error("Failed to fetch data");
        const result = await res.json();
        setData(result);
      } catch (err) {
        setError(err.message);
      }
    };
  
    return (
      <div style={styles.container}>
        <h2>🏠 Home Page</h2>
        {role && <p><strong>Role:</strong> {role}</p>}
  
        {error && <p style={{ color: "red" }}>{error}</p>}
  
        <div>
          {role === "Admin" && (
            <>
              <h3>👥 User List (Admin)</h3>
              <table style={styles.table}>
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Email</th>
                    <th>Role</th>
                  </tr>
                </thead>
                <tbody>
                  {data.map((u) => (
                    <tr key={u.id}>
                      <td>{u.id}</td>
                      <td>{u.email}</td>
                      <td>{u.role}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </>
          )}
  
          {role === "User" && (
            <>
              <h3>🙍‍♂️ My Profile</h3>
              <pre style={styles.profileBox}>{JSON.stringify(data, null, 2)}</pre>
            </>
          )}
        </div>
      </div>
    );
  }
  
  const styles = {
    container: {
      margin: "30px auto",
      padding: "20px",
      maxWidth: "700px",
      textAlign: "center",
      backgroundColor: "#fafafa",
      borderRadius: "12px",
      boxShadow: "0 4px 10px rgba(0,0,0,0.1)",
    },
    table: {
      width: "100%",
      borderCollapse: "collapse",
      marginTop: "10px",
    },
    profileBox: {
      textAlign: "left",
      backgroundColor: "#eee",
      padding: "10px",
      borderRadius: "8px",
    },
  };
  
  export default Home;