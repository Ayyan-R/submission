import React, { useState } from 'react';
import { BrowserRouter, Routes, Route, NavLink, Navigate, useNavigate } from 'react-router-dom';
import './router.css';
import NotFound from './NotFound';
import Home from './home';
import Welcome from './components/welcome'; // 👈 added this import

const About = () => <h2 style={{ textAlign: 'center' }}>📘 About Page</h2>;
const Contact = () => <h2 style={{ textAlign: 'center' }}>📞 Contact Page</h2>;
const Logout = () => <h2 style={{ textAlign: 'center' }}>🚪 You have been logged out</h2>;

// --- Login Page ---
const LoginPage = ({ onLogin }) => {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    if (email && password) {
      onLogin();
      navigate("/home");
    }
  };

  return (
    <div className="login-wrapper">
      <div className="login-box">
        <h2>🔐 Welcome Back</h2>
        <p>Please log in to continue</p>

        <form onSubmit={handleSubmit}>
          <input
            type="email"
            placeholder="Email address"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <button type="submit">Login</button>
        </form>

        <p className="signup-text">
          Don’t have an account?{" "}
          <span className="link" onClick={() => navigate("/signup")}>Sign up</span>
        </p>
      </div>
    </div>
  );
};

// --- Signup Page ---
const SignupPage = () => {
  const navigate = useNavigate();
  const [form, setForm] = useState({
    name: '',
    email: '',
    password: '',
    confirmPassword: ''
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (form.password !== form.confirmPassword) {
      alert("Passwords do not match!");
      return;
    }
    alert("Signup successful! You can now login.");
    navigate("/login");
  };

  return (
    <div className="login-wrapper">
      <div className="login-box">
        <h2>📝 Create Account</h2>
        <p>Sign up to get started</p>

        <form onSubmit={handleSubmit}>
          <input
            type="text"
            name="name"
            placeholder="Full Name"
            value={form.name}
            onChange={handleChange}
            required
          />
          <input
            type="email"
            name="email"
            placeholder="Email address"
            value={form.email}
            onChange={handleChange}
            required
          />
          <input
            type="password"
            name="password"
            placeholder="Password"
            value={form.password}
            onChange={handleChange}
            required
          />
          <input
            type="password"
            name="confirmPassword"
            placeholder="Confirm Password"
            value={form.confirmPassword}
            onChange={handleChange}
            required
          />
          <button type="submit">Sign Up</button>
        </form>

        <p className="signup-text">
          Already have an account?{" "}
          <span className="link" onClick={() => navigate("/login")}>Login</span>
        </p>
      </div>
    </div>
  );
};

// --- Main App ---
const RouterApp = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const handleLogin = () => setIsLoggedIn(true);
  const handleLogout = () => setIsLoggedIn(false);

  return (
    <BrowserRouter>
      {isLoggedIn ? (
        <>
          <nav className="navbar">
            <div className="nav-left">
              <NavLink to="/home" className={({ isActive }) => (isActive ? "active" : "")}>Home</NavLink>
              {" | "}
              <NavLink to="/about" className={({ isActive }) => (isActive ? "active" : "")}>About</NavLink>
              {" | "}
              <NavLink to="/contact" className={({ isActive }) => (isActive ? "active" : "")}>Contact</NavLink>
            </div>
            <div className="nav-right">
              <button className="btn" onClick={handleLogout}>Logout</button>
            </div>
          </nav>

          <Routes>
            <Route path="/home" element={<Home />} />
            <Route path="/about" element={<About />} />
            <Route path="/contact" element={<Contact />} />
            <Route path="/" element={<Navigate to="/home" />} />
            <Route path="*" element={<NotFound />} />
          </Routes>
        </>
      ) : (
        <Routes>
          <Route path="/" element={<Welcome />} /> {/* 👈 Added Welcome page here */}
          <Route path="/login" element={<LoginPage onLogin={handleLogin} />} />
          <Route path="/signup" element={<SignupPage />} />
          <Route path="*" element={<Navigate to="/" />} />
        </Routes>
      )}
    </BrowserRouter>
  );
};

export default RouterApp;
