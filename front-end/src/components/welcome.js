import React from "react";
import { useNavigate } from "react-router-dom";

function Welcome() {
  const navigate = useNavigate();

  const handleLoginClick = () => {
    navigate("/login");
  };

  return (
    <div style={styles.container}>
      <h1 style={styles.title}>👋 Welcome to Bank Management Portal</h1>
      <p style={styles.subtitle}>Securely manage your account and users.</p>
      <button style={styles.button} onClick={handleLoginClick}>
        Go to Login
      </button>
    </div>
  );
}

const styles = {
  container: {
    height: "100vh",
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "center",
    background: "linear-gradient(135deg, #007bff, #6f42c1)",
    color: "white",
    fontFamily: "Arial, sans-serif",
  },
  title: {
    fontSize: "2rem",
    marginBottom: "10px",
  },
  subtitle: {
    fontSize: "1.1rem",
    marginBottom: "30px",
    opacity: 0.9,
  },
  button: {
    padding: "12px 30px",
    fontSize: "16px",
    backgroundColor: "white",
    color: "#007bff",
    border: "none",
    borderRadius: "8px",
    cursor: "pointer",
    fontWeight: "bold",
  },
};

export default Welcome;
