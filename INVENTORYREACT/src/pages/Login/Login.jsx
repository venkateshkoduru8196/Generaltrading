import { useState } from "react";
import {
  Link,
  Navigate,
  useNavigate,
} from "react-router-dom";

import { useAuth } from "../../context/AuthContext";

import "./Login.css";

export default function Login() {
  const navigate = useNavigate();

  const {
    loginUser,
    isAuthenticated,
  } = useAuth();

  if (isAuthenticated) {
    return (
      <Navigate
        to="/"
        replace
      />
    );
  }

  const [formData, setFormData] =
    useState({
      userName: "",
      password: "",
    });

  const [loading, setLoading] =
    useState(false);

  const [error, setError] =
    useState("");

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]:
        e.target.value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    setError("");
    setLoading(true);

    try {

      await loginUser(formData);

      navigate("/", {
        replace: true,
      });

    } catch (error) {

      setError(
        error?.response?.data?.message ||
        "Invalid username or password."
      );

    } finally {

      setLoading(false);

    }
  };

  return (
    <div className="login-page">

      {/* Left Panel */}

      <div className="login-left">

        <div className="login-overlay">

          <div className="login-brand">

            <div className="brand-logo">
              GST
            </div>

            <h1>
              Advanced GST ERP
            </h1>

            <p>
              Billing, Inventory,
              Accounting and GST
              Management System
            </p>

          </div>

          <div className="features">

            <div>
              ✓ Inventory Management
            </div>

            <div>
              ✓ Purchase & Sales
            </div>

            <div>
              ✓ GST Billing
            </div>

            <div>
              ✓ Financial Reports
            </div>

            <div>
              ✓ Role Based Security
            </div>

          </div>

        </div>

      </div>

      {/* Right Panel */}

      <div className="login-right">

        <div className="login-card">

          <h2>
            Welcome Back
          </h2>

          <p className="login-subtitle">
            Sign in to continue
          </p>

          <form
            onSubmit={handleSubmit}
          >

            <div className="input-group">

              <label>
                Username
              </label>

              <input
                type="text"
                name="userName"
                placeholder="Enter Username"
                value={
                  formData.userName
                }
                onChange={
                  handleChange
                }
                required
              />

            </div>

            <div className="input-group">

              <label>
                Password
              </label>

              <input
                type="password"
                name="password"
                placeholder="Enter Password"
                value={
                  formData.password
                }
                onChange={
                  handleChange
                }
                required
              />

            </div>

            {error && (

              <div className="login-error">

                {error}

              </div>

            )}

            <button
              className="login-btn"
              type="submit"
              disabled={loading}
            >

              {loading
                ? "Signing In..."
                : "Login"}

            </button>

          </form>

          <div className="login-footer">

            Don't have an account?

            <Link
              to="/register"
            >
              Create Account
            </Link>

          </div>

        </div>

      </div>

    </div>
  );
}