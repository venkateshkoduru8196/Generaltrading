import { useState } from "react";
import {
  Link,
  useNavigate,
} from "react-router-dom";

import { useAuth } from "../../context/AuthContext";

import "./Register.css";

export default function Register() {
  const navigate = useNavigate();

  const { registerUser } =
    useAuth();

  const [formData, setFormData] =
    useState({
      firstName: "",
      lastName: "",
      userName: "",
      email: "",
      phoneNumber: "",
      password: "",
      confirmPassword: "",
    });

  const [loading, setLoading] =
    useState(false);

  const [error, setError] =
    useState("");

  const [success, setSuccess] =
    useState("");

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]:
        e.target.value,
    });
  };

  const handleSubmit =
    async (e) => {

      e.preventDefault();

      setError("");
      setSuccess("");

      if (
        formData.password !==
        formData.confirmPassword
      ) {
        setError(
          "Passwords do not match."
        );
        return;
      }

      setLoading(true);

      try {

        const registerData = {
          firstName:
            formData.firstName,

          lastName:
            formData.lastName,

          userName:
            formData.userName,

          email:
            formData.email,

          phoneNumber:
            formData.phoneNumber,

          password:
            formData.password,
        };

        await registerUser(
          registerData
        );

        setSuccess(
          "Registration successful. Redirecting to login..."
        );

        setTimeout(() => {
          navigate("/login", {
            replace: true,
          });
        }, 2000);

      }
      catch (error) {

        setError(
          error?.response?.data?.message ||
          error?.response?.data ||
          "Registration failed."
        );

      }
      finally {

        setLoading(false);

      }

    };

  return (
    <div className="register-page">

      <div className="register-card">

        <div className="register-header">

          <div className="logo-circle">
            GST
          </div>

          <h2>
            Customer Registration
          </h2>

          <p>
            Create your customer account
          </p>

        </div>

        <form
          onSubmit={handleSubmit}
        >

          <div className="row">

            <div className="input-group">

              <label>
                First Name
              </label>

              <input
                type="text"
                name="firstName"
                value={
                  formData.firstName
                }
                onChange={
                  handleChange
                }
                required
              />

            </div>

            <div className="input-group">

              <label>
                Last Name
              </label>

              <input
                type="text"
                name="lastName"
                value={
                  formData.lastName
                }
                onChange={
                  handleChange
                }
                required
              />

            </div>

          </div>

          <div className="input-group">

            <label>
              Username
            </label>

            <input
              type="text"
              name="userName"
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
              Email
            </label>

            <input
              type="email"
              name="email"
              value={
                formData.email
              }
              onChange={
                handleChange
              }
              required
            />

          </div>

          <div className="input-group">

            <label>
              Phone Number
            </label>

            <input
              type="text"
              name="phoneNumber"
              value={
                formData.phoneNumber
              }
              onChange={
                handleChange
              }
              required
            />

          </div>

          <div className="row">

            <div className="input-group">

              <label>
                Password
              </label>

              <input
                type="password"
                name="password"
                value={
                  formData.password
                }
                onChange={
                  handleChange
                }
                required
              />

            </div>

            <div className="input-group">

              <label>
                Confirm Password
              </label>

              <input
                type="password"
                name="confirmPassword"
                value={
                  formData.confirmPassword
                }
                onChange={
                  handleChange
                }
                required
              />

            </div>

          </div>

          {error && (
            <div className="error">
              {error}
            </div>
          )}

          {success && (
            <div className="success">
              {success}
            </div>
          )}

          <button
            type="submit"
            className="register-btn"
            disabled={loading}
          >
            {loading
              ? "Registering..."
              : "Register"}
          </button>

        </form>

        <div className="register-footer">

          Already have an account?

          <Link to="/login">
            Login
          </Link>

        </div>

      </div>

    </div>
  );
}