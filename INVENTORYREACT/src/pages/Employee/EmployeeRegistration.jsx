import { useState } from "react";
import { Link } from "react-router-dom";

import { createEmployee } from "../../services/employeeService";

import "./EmployeeRegistration.css";

/*
===========================================================
EMPLOYEE REGISTRATION
===========================================================

Purpose:
Create a new Employee account.

Backend:
POST /api/Auth/create-employee

Backend automatically sets:
Role = "Employee"

Backend automatically assigns:
CompanyId = logged-in user's CompanyId

Frontend sends:
- FullName
- UserName
- Email
- PhoneNumber
- Password
- ConfirmPassword
- EditPassword
- ConfirmEditPassword
===========================================================
*/

const INITIAL_FORM = {
  fullName: "",
  userName: "",
  email: "",
  phoneNumber: "",
  password: "",
  confirmPassword: "",
  editPassword: "",
  confirmEditPassword: "",
};

export default function EmployeeRegistration() {
  const [formData, setFormData] =
    useState(INITIAL_FORM);

  const [loading, setLoading] =
    useState(false);

  const [error, setError] =
    useState("");

  const [success, setSuccess] =
    useState("");

  const [showPassword, setShowPassword] =
    useState(false);

  const [showConfirmPassword, setShowConfirmPassword] =
    useState(false);

  const [showEditPassword, setShowEditPassword] =
    useState(false);

  const [showConfirmEditPassword, setShowConfirmEditPassword] =
    useState(false);

  /*
  ===========================================================
  HANDLE INPUT
  ===========================================================
  */

  const handleChange = (e) => {
    const { name, value } = e.target;

    setFormData((previous) => ({
      ...previous,
      [name]: value,
    }));

    if (error) {
      setError("");
    }

    if (success) {
      setSuccess("");
    }
  };

  /*
  ===========================================================
  VALIDATION
  ===========================================================
  */

  const validateForm = () => {
    const fullName =
      formData.fullName.trim();

    const userName =
      formData.userName.trim();

    const email =
      formData.email.trim();

    const phoneNumber =
      formData.phoneNumber.trim();

    if (!fullName) {
      return "Full Name is required.";
    }

    if (fullName.length < 3) {
      return "Full Name must contain at least 3 characters.";
    }

    if (!userName) {
      return "Username is required.";
    }

    if (userName.length < 3) {
      return "Username must contain at least 3 characters.";
    }

    if (!email) {
      return "Email is required.";
    }

    if (!isValidEmail(email)) {
      return "Please enter a valid email address.";
    }

    if (!phoneNumber) {
      return "Phone Number is required.";
    }

    if (!isValidPhone(phoneNumber)) {
      return "Please enter a valid phone number.";
    }

    if (!formData.password) {
      return "Password is required.";
    }

    if (formData.password.length < 6) {
      return "Password must contain at least 6 characters.";
    }

    if (!formData.confirmPassword) {
      return "Confirm Password is required.";
    }

    if (
      formData.password !==
      formData.confirmPassword
    ) {
      return "Password and Confirm Password do not match.";
    }

    if (!formData.editPassword) {
      return "Edit Password is required.";
    }

    if (formData.editPassword.length < 4) {
      return "Edit Password must contain at least 4 characters.";
    }

    if (!formData.confirmEditPassword) {
      return "Confirm Edit Password is required.";
    }

    if (
      formData.editPassword !==
      formData.confirmEditPassword
    ) {
      return "Edit Password and Confirm Edit Password do not match.";
    }

    return "";
  };

  /*
  ===========================================================
  SUBMIT
  ===========================================================
  */

  const handleSubmit = async (e) => {
    e.preventDefault();

    setError("");
    setSuccess("");

    const validationError =
      validateForm();

    if (validationError) {
      setError(validationError);

      window.scrollTo({
        top: 0,
        behavior: "smooth",
      });

      return;
    }

    setLoading(true);

    try {
      /*
      =======================================================
      IMPORTANT

      Role is NOT sent from frontend.

      Backend controller sets:
      dto.Role = "Employee";

      CompanyId is also NOT sent.

      Backend gets the logged-in Admin's CompanyId.
      =======================================================
      */

      const payload = {
        fullName:
          formData.fullName.trim(),

        userName:
          formData.userName.trim(),

        email:
          formData.email.trim(),

        phoneNumber:
          formData.phoneNumber.trim(),

        password:
          formData.password,

        confirmPassword:
          formData.confirmPassword,

        editPassword:
          formData.editPassword,

        confirmEditPassword:
          formData.confirmEditPassword,
      };

      await createEmployee(payload);

      setSuccess(
        "Employee account created successfully."
      );

      setFormData(INITIAL_FORM);

      setShowPassword(false);
      setShowConfirmPassword(false);
      setShowEditPassword(false);
      setShowConfirmEditPassword(false);

      window.scrollTo({
        top: 0,
        behavior: "smooth",
      });
    } catch (err) {
      console.error(
        "Create Employee Error:",
        err
      );

      setError(
        getErrorMessage(
          err,
          "Unable to create employee."
        )
      );

      window.scrollTo({
        top: 0,
        behavior: "smooth",
      });
    } finally {
      setLoading(false);
    }
  };

  /*
  ===========================================================
  RENDER
  ===========================================================
  */

  return (
    <div className="employee-registration-page">

      <div className="employee-registration-container">

        {/* =================================================
            HEADER
        ================================================= */}

        <div className="employee-registration-header">

          <div className="employee-title-wrapper">

            <div className="employee-title-icon">
              <span>👨‍💼</span>
            </div>

            <div>
              <h1>
                Create Employee
              </h1>

              <p>
                Create and configure a new employee account
              </p>
            </div>

          </div>

        </div>

        {/* =================================================
            ALERTS
        ================================================= */}

        {error && (
          <div
            className="employee-alert employee-alert-error"
            role="alert"
          >
            <div className="employee-alert-icon">
              !
            </div>

            <div className="employee-alert-content">

              <strong>
                Unable to complete request
              </strong>

              <span>
                {error}
              </span>

            </div>

            <button
              type="button"
              className="employee-alert-close"
              onClick={() => setError("")}
              aria-label="Close error"
            >
              ×
            </button>

          </div>
        )}

        {success && (
          <div
            className="employee-alert employee-alert-success"
            role="status"
          >
            <div className="employee-alert-icon">
              ✓
            </div>

            <div className="employee-alert-content">

              <strong>
                Employee Created
              </strong>

              <span>
                {success}
              </span>

            </div>

            <button
              type="button"
              className="employee-alert-close"
              onClick={() => setSuccess("")}
              aria-label="Close success message"
            >
              ×
            </button>

          </div>
        )}

        {/* =================================================
            MAIN CARD
        ================================================= */}

        <div className="employee-registration-card">

          <form
            onSubmit={handleSubmit}
            noValidate
          >

            {/* =============================================
                ACCOUNT INFORMATION
            ============================================== */}

            <section className="employee-form-section">

              <div className="employee-section-heading">

                <div className="employee-section-icon account">
                  <span>👤</span>
                </div>

                <div>

                  <h2>
                    Account Information
                  </h2>

                  <p>
                    Enter the employee's basic account details.
                  </p>

                </div>

              </div>

              <div className="employee-form-grid">

                {/* Full Name */}

                <div className="employee-field">

                  <label htmlFor="fullName">
                    Full Name
                    <span>*</span>
                  </label>

                  <div className="employee-input-wrapper">

                    <span className="employee-input-icon">
                      👤
                    </span>

                    <input
                      id="fullName"
                      type="text"
                      name="fullName"
                      value={formData.fullName}
                      onChange={handleChange}
                      placeholder="Enter full name"
                      autoComplete="name"
                      maxLength={150}
                      disabled={loading}
                    />

                  </div>

                </div>

                {/* Username */}

                <div className="employee-field">

                  <label htmlFor="userName">
                    Username
                    <span>*</span>
                  </label>

                  <div className="employee-input-wrapper">

                    <span className="employee-input-icon">
                      @
                    </span>

                    <input
                      id="userName"
                      type="text"
                      name="userName"
                      value={formData.userName}
                      onChange={handleChange}
                      placeholder="Enter username"
                      autoComplete="username"
                      maxLength={100}
                      disabled={loading}
                    />

                  </div>

                </div>

                {/* Email */}

                <div className="employee-field">

                  <label htmlFor="email">
                    Email Address
                    <span>*</span>
                  </label>

                  <div className="employee-input-wrapper">

                    <span className="employee-input-icon">
                      ✉
                    </span>

                    <input
                      id="email"
                      type="email"
                      name="email"
                      value={formData.email}
                      onChange={handleChange}
                      placeholder="name@company.com"
                      autoComplete="email"
                      maxLength={150}
                      disabled={loading}
                    />

                  </div>

                </div>

                {/* Phone */}

                <div className="employee-field">

                  <label htmlFor="phoneNumber">
                    Phone Number
                    <span>*</span>
                  </label>

                  <div className="employee-input-wrapper">

                    <span className="employee-input-icon">
                      ☎
                    </span>

                    <input
                      id="phoneNumber"
                      type="tel"
                      name="phoneNumber"
                      value={formData.phoneNumber}
                      onChange={handleChange}
                      placeholder="Enter phone number"
                      autoComplete="tel"
                      maxLength={20}
                      disabled={loading}
                    />

                  </div>

                </div>

              </div>

            </section>

            {/* =============================================
                SECURITY
            ============================================== */}

            <section className="employee-form-section">

              <div className="employee-section-heading">

                <div className="employee-section-icon security">
                  <span>🔐</span>
                </div>

                <div>

                  <h2>
                    Security
                  </h2>

                  <p>
                    Configure login and protected edit credentials.
                  </p>

                </div>

              </div>

              <div className="employee-form-grid">

                {/* Password */}

                <div className="employee-field">

                  <label htmlFor="password">
                    Password
                    <span>*</span>
                  </label>

                  <div className="employee-input-wrapper">

                    <span className="employee-input-icon">
                      🔒
                    </span>

                    <input
                      id="password"
                      type={
                        showPassword
                          ? "text"
                          : "password"
                      }
                      name="password"
                      value={formData.password}
                      onChange={handleChange}
                      placeholder="Enter password"
                      autoComplete="new-password"
                      disabled={loading}
                    />

                    <button
                      type="button"
                      className="employee-password-toggle"
                      onClick={() =>
                        setShowPassword(
                          (previous) =>
                            !previous
                        )
                      }
                      disabled={loading}
                      aria-label={
                        showPassword
                          ? "Hide password"
                          : "Show password"
                      }
                    >
                      {showPassword
                        ? "🙈"
                        : "👁"}
                    </button>

                  </div>

                  <small className="employee-field-help">
                    Minimum 6 characters.
                  </small>

                </div>

                {/* Confirm Password */}

                <div className="employee-field">

                  <label htmlFor="confirmPassword">
                    Confirm Password
                    <span>*</span>
                  </label>

                  <div className="employee-input-wrapper">

                    <span className="employee-input-icon">
                      🔒
                    </span>

                    <input
                      id="confirmPassword"
                      type={
                        showConfirmPassword
                          ? "text"
                          : "password"
                      }
                      name="confirmPassword"
                      value={
                        formData.confirmPassword
                      }
                      onChange={handleChange}
                      placeholder="Re-enter password"
                      autoComplete="new-password"
                      disabled={loading}
                    />

                    <button
                      type="button"
                      className="employee-password-toggle"
                      onClick={() =>
                        setShowConfirmPassword(
                          (previous) =>
                            !previous
                        )
                      }
                      disabled={loading}
                      aria-label={
                        showConfirmPassword
                          ? "Hide confirm password"
                          : "Show confirm password"
                      }
                    >
                      {showConfirmPassword
                        ? "🙈"
                        : "👁"}
                    </button>

                  </div>

                </div>

                {/* Edit Password */}

                <div className="employee-field">

                  <label htmlFor="editPassword">
                    Edit Password
                    <span>*</span>
                  </label>

                  <div className="employee-input-wrapper">

                    <span className="employee-input-icon">
                      🛡
                    </span>

                    <input
                      id="editPassword"
                      type={
                        showEditPassword
                          ? "text"
                          : "password"
                      }
                      name="editPassword"
                      value={
                        formData.editPassword
                      }
                      onChange={handleChange}
                      placeholder="Enter edit password"
                      autoComplete="new-password"
                      disabled={loading}
                    />

                    <button
                      type="button"
                      className="employee-password-toggle"
                      onClick={() =>
                        setShowEditPassword(
                          (previous) =>
                            !previous
                        )
                      }
                      disabled={loading}
                      aria-label={
                        showEditPassword
                          ? "Hide edit password"
                          : "Show edit password"
                      }
                    >
                      {showEditPassword
                        ? "🙈"
                        : "👁"}
                    </button>

                  </div>

                  <small className="employee-field-help">
                    Used for protected editing operations.
                  </small>

                </div>

                {/* Confirm Edit Password */}

                <div className="employee-field">

                  <label htmlFor="confirmEditPassword">
                    Confirm Edit Password
                    <span>*</span>
                  </label>

                  <div className="employee-input-wrapper">

                    <span className="employee-input-icon">
                      🛡
                    </span>

                    <input
                      id="confirmEditPassword"
                      type={
                        showConfirmEditPassword
                          ? "text"
                          : "password"
                      }
                      name="confirmEditPassword"
                      value={
                        formData.confirmEditPassword
                      }
                      onChange={handleChange}
                      placeholder="Re-enter edit password"
                      autoComplete="new-password"
                      disabled={loading}
                    />

                    <button
                      type="button"
                      className="employee-password-toggle"
                      onClick={() =>
                        setShowConfirmEditPassword(
                          (previous) =>
                            !previous
                        )
                      }
                      disabled={loading}
                      aria-label={
                        showConfirmEditPassword
                          ? "Hide confirm edit password"
                          : "Show confirm edit password"
                      }
                    >
                      {showConfirmEditPassword
                        ? "🙈"
                        : "👁"}
                    </button>

                  </div>

                </div>

              </div>

              {/* Security Information */}

              <div className="employee-security-note">

                <div className="employee-security-note-icon">
                  ✓
                </div>

                <div>

                  <strong>
                    Security credentials
                  </strong>

                  <p>
                    The login password is used for
                    authentication. The Edit Password
                    provides an additional verification
                    layer for protected editing operations.
                  </p>

                </div>

              </div>

            </section>

            {/* =============================================
                ACTIONS
            ============================================== */}

            <div className="employee-form-actions">

              <Link
                to="/"
                className="employee-cancel-btn"
              >
                Cancel
              </Link>

              <button
                type="submit"
                className="employee-submit-btn"
                disabled={loading}
              >
                {loading ? (
                  <>
                    <span className="employee-spinner" />

                    Creating Employee...
                  </>
                ) : (
                  <>
                    <span className="employee-submit-icon">
                      ✓
                    </span>

                    Create Employee
                  </>
                )}
              </button>

            </div>

          </form>

        </div>

        {/* =================================================
            FOOTER
        ================================================= */}

        <div className="employee-page-footer">
          Employee access is controlled by
          role-based authorization.
        </div>

      </div>

    </div>
  );
}

/*
===========================================================
HELPER FUNCTIONS
===========================================================
*/

function isValidEmail(email) {
  return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(
    email
  );
}

function isValidPhone(phone) {
  return /^[0-9+\-\s()]{7,20}$/.test(
    phone
  );
}

function getErrorMessage(
  err,
  fallbackMessage
) {
  if (err?.response) {
    const data = err.response.data;

    if (typeof data === "string") {
      return data;
    }

    if (data?.message) {
      return data.message;
    }

    if (data?.title) {
      return data.title;
    }

    if (data?.errors) {
      const validationMessages =
        Object.values(data.errors)
          .flat()
          .filter(Boolean);

      if (
        validationMessages.length > 0
      ) {
        return validationMessages.join(" ");
      }
    }

    return fallbackMessage;
  }

  if (err?.request) {
    return "Unable to connect to the server. Please check your connection.";
  }

  return (
    err?.message ||
    fallbackMessage
  );
}