import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

import { createAdmin } from "../../services/adminService";
import { getCompanyLookup } from "../../services/companyService";

import "./AdminRegistration.css";

/*
===========================================================
ADMIN REGISTRATION
===========================================================

Purpose:
Create a new Administrator.

Backend endpoint:
POST /api/Auth/create-admin

Backend automatically sets:
Role = "Admin"

Frontend sends:
- FullName
- UserName
- Email
- PhoneNumber
- Password
- ConfirmPassword
- EditPassword
- ConfirmEditPassword
- CompanyId
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
  companyId: "",
};

export default function AdminRegistration() {
  const [formData, setFormData] = useState(INITIAL_FORM);

  const [companies, setCompanies] = useState([]);

  const [loading, setLoading] = useState(false);
  const [companiesLoading, setCompaniesLoading] = useState(true);

  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] =
    useState(false);

  const [showEditPassword, setShowEditPassword] =
    useState(false);

  const [showConfirmEditPassword, setShowConfirmEditPassword] =
    useState(false);

  /*
  ===========================================================
  LOAD COMPANIES
  ===========================================================
  */

  useEffect(() => {
    loadCompanies();
  }, []);

  const loadCompanies = async () => {
    setCompaniesLoading(true);
    setError("");

    try {
      const result = await getCompanyLookup();

      setCompanies(
        Array.isArray(result) ? result : []
      );
    } catch (err) {
      console.error(
        "Failed to load companies:",
        err
      );

      setError(
        getErrorMessage(
          err,
          "Unable to load companies."
        )
      );
    } finally {
      setCompaniesLoading(false);
    }
  };

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

    /*
    Clear messages when user starts editing again.
    */
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
    const fullName = formData.fullName.trim();
    const userName = formData.userName.trim();
    const email = formData.email.trim();
    const phoneNumber = formData.phoneNumber.trim();

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

    if (!formData.companyId) {
      return "Please select a company.";
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

    const validationError = validateForm();

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

      Role is intentionally NOT sent from the page.

      Backend controller sets:
      dto.Role = "Admin";
      =======================================================
      */

      const payload = {
        fullName: formData.fullName.trim(),

        userName: formData.userName.trim(),

        email: formData.email.trim(),

        phoneNumber: formData.phoneNumber.trim(),

        password: formData.password,

        confirmPassword:
          formData.confirmPassword,

        editPassword:
          formData.editPassword,

        confirmEditPassword:
          formData.confirmEditPassword,

        companyId:
          Number(formData.companyId),
      };

      await createAdmin(payload);

      setSuccess(
        "Administrator account created successfully."
      );

      setFormData(INITIAL_FORM);

      /*
      Reset password visibility after successful creation.
      */
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
        "Create Admin Error:",
        err
      );

      setError(
        getErrorMessage(
          err,
          "Unable to create administrator."
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
    <div className="admin-registration-page">

      <div className="admin-registration-container">

        {/* =================================================
            HEADER
        ================================================= */}

        <div className="admin-registration-header">

          <div className="admin-title-wrapper">

            <div className="admin-title-icon">
              <span>👤</span>
            </div>

            <div>
              <h1>
                Create Administrator
              </h1>

              <p>
                Create and configure a new administrator account
              </p>
            </div>

          </div>

        </div>

        {/* =================================================
            ALERTS
        ================================================= */}

        {error && (
          <div
            className="admin-alert admin-alert-error"
            role="alert"
          >
            <div className="admin-alert-icon">
              !
            </div>

            <div className="admin-alert-content">
              <strong>
                Unable to complete request
              </strong>

              <span>
                {error}
              </span>
            </div>

            <button
              type="button"
              className="admin-alert-close"
              onClick={() => setError("")}
              aria-label="Close error"
            >
              ×
            </button>
          </div>
        )}

        {success && (
          <div
            className="admin-alert admin-alert-success"
            role="status"
          >
            <div className="admin-alert-icon">
              ✓
            </div>

            <div className="admin-alert-content">
              <strong>
                Administrator Created
              </strong>

              <span>
                {success}
              </span>
            </div>

            <button
              type="button"
              className="admin-alert-close"
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

        <div className="admin-registration-card">

          <form onSubmit={handleSubmit} noValidate>

            {/* =============================================
                ACCOUNT INFORMATION
            ============================================== */}

            <section className="admin-form-section">

              <div className="admin-section-heading">

                <div className="admin-section-icon account">
                  <span>👤</span>
                </div>

                <div>
                  <h2>
                    Account Information
                  </h2>

                  <p>
                    Enter the administrator's basic account details.
                  </p>
                </div>

              </div>

              <div className="admin-form-grid">

                {/* Full Name */}

                <div className="admin-field">

                  <label htmlFor="fullName">
                    Full Name
                    <span>*</span>
                  </label>

                  <div className="admin-input-wrapper">

                    <span className="admin-input-icon">
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

                <div className="admin-field">

                  <label htmlFor="userName">
                    Username
                    <span>*</span>
                  </label>

                  <div className="admin-input-wrapper">

                    <span className="admin-input-icon">
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

                <div className="admin-field">

                  <label htmlFor="email">
                    Email Address
                    <span>*</span>
                  </label>

                  <div className="admin-input-wrapper">

                    <span className="admin-input-icon">
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

                <div className="admin-field">

                  <label htmlFor="phoneNumber">
                    Phone Number
                    <span>*</span>
                  </label>

                  <div className="admin-input-wrapper">

                    <span className="admin-input-icon">
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

                {/* Company */}

                <div className="admin-field admin-field-full">

                  <label htmlFor="companyId">
                    Company
                    <span>*</span>
                  </label>

                  <div className="admin-input-wrapper">

                    <span className="admin-input-icon">
                      🏢
                    </span>

                    <select
                      id="companyId"
                      name="companyId"
                      value={formData.companyId}
                      onChange={handleChange}
                      disabled={
                        loading ||
                        companiesLoading
                      }
                    >
                      <option value="">
                        {companiesLoading
                          ? "Loading companies..."
                          : "Select company"}
                      </option>

                      {companies.map((company) => (
                        <option
                          key={company.companyId}
                          value={company.companyId}
                        >
                          {company.companyName}
                          {company.companyCode
                            ? ` (${company.companyCode})`
                            : ""}
                        </option>
                      ))}
                    </select>

                    <span className="admin-select-arrow">
                      ▾
                    </span>

                  </div>

                  {!companiesLoading &&
                    companies.length === 0 && (
                      <small className="admin-field-help error-text">
                        No active companies are available.
                      </small>
                    )}

                  {!companiesLoading &&
                    companies.length > 0 && (
                      <small className="admin-field-help">
                        Select the company this administrator
                        will manage.
                      </small>
                    )}

                </div>

              </div>

            </section>

            {/* =============================================
                SECURITY
            ============================================== */}

            <section className="admin-form-section">

              <div className="admin-section-heading">

                <div className="admin-section-icon security">
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

              <div className="admin-form-grid">

                {/* Password */}

                <div className="admin-field">

                  <label htmlFor="password">
                    Password
                    <span>*</span>
                  </label>

                  <div className="admin-input-wrapper">

                    <span className="admin-input-icon">
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
                      className="password-toggle"
                      onClick={() =>
                        setShowPassword(
                          (previous) => !previous
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

                  <small className="admin-field-help">
                    Minimum 6 characters.
                  </small>

                </div>

                {/* Confirm Password */}

                <div className="admin-field">

                  <label htmlFor="confirmPassword">
                    Confirm Password
                    <span>*</span>
                  </label>

                  <div className="admin-input-wrapper">

                    <span className="admin-input-icon">
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
                      value={formData.confirmPassword}
                      onChange={handleChange}
                      placeholder="Re-enter password"
                      autoComplete="new-password"
                      disabled={loading}
                    />

                    <button
                      type="button"
                      className="password-toggle"
                      onClick={() =>
                        setShowConfirmPassword(
                          (previous) => !previous
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

                <div className="admin-field">

                  <label htmlFor="editPassword">
                    Edit Password
                    <span>*</span>
                  </label>

                  <div className="admin-input-wrapper">

                    <span className="admin-input-icon">
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
                      value={formData.editPassword}
                      onChange={handleChange}
                      placeholder="Enter edit password"
                      autoComplete="new-password"
                      disabled={loading}
                    />

                    <button
                      type="button"
                      className="password-toggle"
                      onClick={() =>
                        setShowEditPassword(
                          (previous) => !previous
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

                  <small className="admin-field-help">
                    Used when protected records require
                    edit-password verification.
                  </small>

                </div>

                {/* Confirm Edit Password */}

                <div className="admin-field">

                  <label htmlFor="confirmEditPassword">
                    Confirm Edit Password
                    <span>*</span>
                  </label>

                  <div className="admin-input-wrapper">

                    <span className="admin-input-icon">
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
                      className="password-toggle"
                      onClick={() =>
                        setShowConfirmEditPassword(
                          (previous) => !previous
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

              <div className="admin-security-note">

                <div className="admin-security-note-icon">
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

            <div className="admin-form-actions">

              <Link
                to="/"
                className="admin-cancel-btn"
              >
                Cancel
              </Link>

              <button
                type="submit"
                className="admin-submit-btn"
                disabled={
                  loading ||
                  companiesLoading ||
                  companies.length === 0
                }
              >
                {loading ? (
                  <>
                    <span className="admin-spinner" />
                    Creating Administrator...
                  </>
                ) : (
                  <>
                    <span className="admin-submit-icon">
                      ✓
                    </span>
                    Create Administrator
                  </>
                )}
              </button>

            </div>

          </form>

        </div>

        {/* =================================================
            FOOTER
        ================================================= */}

        <div className="admin-page-footer">
          Administrator access is controlled by
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
      const validationMessages = Object.values(
        data.errors
      )
        .flat()
        .filter(Boolean);

      if (validationMessages.length > 0) {
        return validationMessages.join(" ");
      }
    }

    return fallbackMessage;
  }

  if (err?.request) {
    return "Unable to connect to the server. Please check your connection.";
  }

  return err?.message || fallbackMessage;
}