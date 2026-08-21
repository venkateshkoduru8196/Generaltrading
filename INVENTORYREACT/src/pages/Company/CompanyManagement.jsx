import { useEffect, useMemo, useState } from "react";

import {
  createCompany,
  deleteCompany,
  getCompanies,
  updateCompany,
} from "../../services/companyService";

import "./CompanyManagement.css";


/*
===========================================================
INITIAL FORM
===========================================================
*/

const INITIAL_FORM = {
  companyId: 0,
  companyCode: "",
  companyName: "",
  ownerName: "",
  gstin: "",
  phoneNumber: "",
  email: "",
  address: "",
  isActive: true,
};


export default function CompanyManagement() {

  /*
  =========================================================
  STATE
  =========================================================
  */

  const [companies, setCompanies] =
    useState([]);

  const [loading, setLoading] =
    useState(true);

  const [saving, setSaving] =
    useState(false);

  const [error, setError] =
    useState("");

  const [success, setSuccess] =
    useState("");

  const [searchText, setSearchText] =
    useState("");

  const [statusFilter, setStatusFilter] =
    useState("all");

  const [showForm, setShowForm] =
    useState(false);

  const [editingCompany, setEditingCompany] =
    useState(null);

  const [formData, setFormData] =
    useState(INITIAL_FORM);

  const [deleteTarget, setDeleteTarget] =
    useState(null);


  /*
  =========================================================
  LOAD COMPANIES
  =========================================================
  */

  useEffect(() => {
    loadCompanies();
  }, []);


  const loadCompanies = async () => {

    setLoading(true);
    setError("");

    try {

      const result =
        await getCompanies();

      setCompanies(
        Array.isArray(result)
          ? result
          : []
      );

    } catch (err) {

      console.error(
        "Load companies error:",
        err
      );

      setError(
        getErrorMessage(
          err,
          "Unable to load companies."
        )
      );

    } finally {

      setLoading(false);

    }
  };


  /*
  =========================================================
  FILTERED COMPANIES
  =========================================================
  */

  const filteredCompanies = useMemo(() => {

    const search =
      searchText
        .trim()
        .toLowerCase();

    return companies.filter(
      (company) => {

        const matchesSearch =
          !search ||
          company.companyCode
            ?.toLowerCase()
            .includes(search) ||
          company.companyName
            ?.toLowerCase()
            .includes(search) ||
          company.ownerName
            ?.toLowerCase()
            .includes(search) ||
          company.gstin
            ?.toLowerCase()
            .includes(search) ||
          company.email
            ?.toLowerCase()
            .includes(search) ||
          company.phoneNumber
            ?.toLowerCase()
            .includes(search);

        const matchesStatus =
          statusFilter === "all" ||
          (statusFilter === "active" &&
            company.isActive) ||
          (statusFilter === "inactive" &&
            !company.isActive);

        return (
          matchesSearch &&
          matchesStatus
        );
      }
    );

  }, [
    companies,
    searchText,
    statusFilter,
  ]);


  /*
  =========================================================
  FORM CHANGE
  =========================================================
  */

  const handleChange = (e) => {

    const {
      name,
      value,
      type,
      checked,
    } = e.target;

    setFormData(
      (previous) => ({
        ...previous,
        [name]:
          type === "checkbox"
            ? checked
            : value,
      })
    );

    setError("");
    setSuccess("");
  };


  /*
  =========================================================
  OPEN CREATE
  =========================================================
  */

  const handleCreate = () => {

    setEditingCompany(null);

    setFormData(
      INITIAL_FORM
    );

    setError("");
    setSuccess("");

    setShowForm(true);

  };


  /*
  =========================================================
  OPEN EDIT
  =========================================================
  */

  const handleEdit = (company) => {

    setEditingCompany(company);

    setFormData({
      companyId:
        company.companyId,

      companyCode:
        company.companyCode || "",

      companyName:
        company.companyName || "",

      ownerName:
        company.ownerName || "",

      gstin:
        company.gstin || "",

      phoneNumber:
        company.phoneNumber || "",

      email:
        company.email || "",

      address:
        company.address || "",

      isActive:
        company.isActive,
    });

    setError("");
    setSuccess("");

    setShowForm(true);

    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };


  /*
  =========================================================
  CLOSE FORM
  =========================================================
  */

  const handleCloseForm = () => {

    if (saving) {
      return;
    }

    setShowForm(false);

    setEditingCompany(null);

    setFormData(
      INITIAL_FORM
    );

    setError("");

  };


  /*
  =========================================================
  VALIDATE
  =========================================================
  */

  const validateForm = () => {

    const companyCode =
      formData.companyCode.trim();

    const companyName =
      formData.companyName.trim();

    const ownerName =
      formData.ownerName.trim();

    const gstin =
      formData.gstin
        .trim()
        .toUpperCase();

    const phone =
      formData.phoneNumber.trim();

    const email =
      formData.email.trim();

    const address =
      formData.address.trim();


    if (!companyCode) {
      return "Company Code is required.";
    }

    if (companyCode.length > 20) {
      return "Company Code cannot exceed 20 characters.";
    }

    if (!companyName) {
      return "Company Name is required.";
    }

    if (companyName.length > 200) {
      return "Company Name cannot exceed 200 characters.";
    }

    if (
      ownerName &&
      ownerName.length > 150
    ) {
      return "Owner Name cannot exceed 150 characters.";
    }

    if (gstin) {

      if (
        !/^[0-9A-Z]{15}$/.test(
          gstin
        )
      ) {
        return "GSTIN must contain exactly 15 characters.";
      }

    }

    if (phone) {

      if (
        !/^[0-9+\-\s()]{7,20}$/.test(
          phone
        )
      ) {
        return "Please enter a valid phone number.";
      }

    }

    if (email) {

      if (
        !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(
          email
        )
      ) {
        return "Please enter a valid email address.";
      }

    }

    if (address.length > 500) {
      return "Address cannot exceed 500 characters.";
    }

    return "";

  };


  /*
  =========================================================
  SAVE
  =========================================================
  */

  const handleSubmit = async (e) => {

    e.preventDefault();

    setError("");
    setSuccess("");

    const validationError =
      validateForm();

    if (validationError) {

      setError(
        validationError
      );

      window.scrollTo({
        top: 0,
        behavior: "smooth",
      });

      return;
    }

    setSaving(true);

    try {

      const payload = {

        companyCode:
          formData.companyCode
            .trim()
            .toUpperCase(),

        companyName:
          formData.companyName
            .trim(),

        ownerName:
          formData.ownerName
            .trim(),

        gstin:
          formData.gstin
            .trim()
            .toUpperCase(),

        phoneNumber:
          formData.phoneNumber
            .trim(),

        email:
          formData.email
            .trim(),

        address:
          formData.address
            .trim(),

        isActive:
          formData.isActive,
      };


      /*
      =====================================================
      UPDATE
      =====================================================
      */

      if (editingCompany) {

        await updateCompany({
          companyId:
            formData.companyId,

          ...payload,
        });

        setSuccess(
          "Company updated successfully."
        );

      }

      /*
      =====================================================
      CREATE
      =====================================================
      */

      else {

        await createCompany(
          payload
        );

        setSuccess(
          "Company created successfully."
        );

      }


      /*
      =====================================================
      REFRESH
      =====================================================
      */

      await loadCompanies();

      setShowForm(false);

      setEditingCompany(null);

      setFormData(
        INITIAL_FORM
      );

      window.scrollTo({
        top: 0,
        behavior: "smooth",
      });

    } catch (err) {

      console.error(
        "Save company error:",
        err
      );

      setError(
        getErrorMessage(
          err,
          editingCompany
            ? "Unable to update company."
            : "Unable to create company."
        )
      );

    } finally {

      setSaving(false);

    }

  };


  /*
  =========================================================
  DELETE
  =========================================================
  */

  const handleDelete = async () => {

    if (!deleteTarget) {
      return;
    }

    setSaving(true);
    setError("");

    try {

      await deleteCompany(
        deleteTarget.companyId
      );

      setSuccess(
        "Company deleted successfully."
      );

      setDeleteTarget(null);

      await loadCompanies();

    } catch (err) {

      console.error(
        "Delete company error:",
        err
      );

      setError(
        getErrorMessage(
          err,
          "Unable to delete company."
        )
      );

    } finally {

      setSaving(false);

    }

  };


  /*
  =========================================================
  RENDER
  =========================================================
  */

  return (
    <div className="company-management-page">

      <div className="company-management-container">

        {/* =================================================
            HEADER
        ================================================= */}

        <div className="company-page-header">

          <div className="company-title-area">

            <div className="company-title-icon">
              🏢
            </div>

            <div>

              <h1>
                Company Management
              </h1>

              <p>
                Create and manage companies in the system
              </p>

            </div>

          </div>

          {!showForm && (
            <button
              type="button"
              className="company-primary-btn"
              onClick={handleCreate}
            >
              <span>+</span>
              Add Company
            </button>
          )}

        </div>


        {/* =================================================
            ALERT
        ================================================= */}

        {error && (
          <div className="company-alert company-alert-error">

            <span className="company-alert-symbol">
              !
            </span>

            <div>
              <strong>
                Error
              </strong>

              <p>
                {error}
              </p>
            </div>

            <button
              type="button"
              onClick={() =>
                setError("")
              }
            >
              ×
            </button>

          </div>
        )}


        {success && (
          <div className="company-alert company-alert-success">

            <span className="company-alert-symbol">
              ✓
            </span>

            <div>
              <strong>
                Success
              </strong>

              <p>
                {success}
              </p>
            </div>

            <button
              type="button"
              onClick={() =>
                setSuccess("")
              }
            >
              ×
            </button>

          </div>
        )}


        {/* =================================================
            FORM
        ================================================= */}

        {showForm ? (

          <div className="company-form-card">

            <div className="company-form-header">

              <div>

                <h2>
                  {editingCompany
                    ? "Edit Company"
                    : "Create Company"}
                </h2>

                <p>
                  {editingCompany
                    ? "Update company information and status."
                    : "Enter the company details to create a new company."}
                </p>

              </div>

              <button
                type="button"
                className="company-close-btn"
                onClick={
                  handleCloseForm
                }
                disabled={saving}
              >
                ×
              </button>

            </div>


            <form
              onSubmit={handleSubmit}
              noValidate
            >

              {/* =========================================
                  BASIC INFORMATION
              ========================================== */}

              <section className="company-form-section">

                <div className="company-section-title">

                  <div className="company-section-icon">
                    🏢
                  </div>

                  <div>

                    <h3>
                      Company Information
                    </h3>

                    <p>
                      Basic identification information
                    </p>

                  </div>

                </div>


                <div className="company-form-grid">

                  {/* Company Code */}

                  <div className="company-field">

                    <label htmlFor="companyCode">
                      Company Code
                      <span>*</span>
                    </label>

                    <input
                      id="companyCode"
                      name="companyCode"
                      value={
                        formData.companyCode
                      }
                      onChange={
                        handleChange
                      }
                      placeholder="e.g. ABC001"
                      maxLength={20}
                      disabled={saving}
                    />

                  </div>


                  {/* Company Name */}

                  <div className="company-field">

                    <label htmlFor="companyName">
                      Company Name
                      <span>*</span>
                    </label>

                    <input
                      id="companyName"
                      name="companyName"
                      value={
                        formData.companyName
                      }
                      onChange={
                        handleChange
                      }
                      placeholder="Enter company name"
                      maxLength={200}
                      disabled={saving}
                    />

                  </div>


                  {/* Owner */}

                  <div className="company-field">

                    <label htmlFor="ownerName">
                      Owner Name
                    </label>

                    <input
                      id="ownerName"
                      name="ownerName"
                      value={
                        formData.ownerName
                      }
                      onChange={
                        handleChange
                      }
                      placeholder="Enter owner name"
                      maxLength={150}
                      disabled={saving}
                    />

                  </div>


                  {/* GSTIN */}

                  <div className="company-field">

                    <label htmlFor="gstin">
                      GSTIN
                    </label>

                    <input
                      id="gstin"
                      name="gstin"
                      value={
                        formData.gstin
                      }
                      onChange={
                        handleChange
                      }
                      placeholder="15-character GSTIN"
                      maxLength={15}
                      style={{
                        textTransform:
                          "uppercase",
                      }}
                      disabled={saving}
                    />

                  </div>


                  {/* Phone */}

                  <div className="company-field">

                    <label htmlFor="phoneNumber">
                      Phone Number
                    </label>

                    <input
                      id="phoneNumber"
                      name="phoneNumber"
                      value={
                        formData.phoneNumber
                      }
                      onChange={
                        handleChange
                      }
                      placeholder="Enter phone number"
                      maxLength={20}
                      disabled={saving}
                    />

                  </div>


                  {/* Email */}

                  <div className="company-field">

                    <label htmlFor="email">
                      Email Address
                    </label>

                    <input
                      id="email"
                      name="email"
                      type="email"
                      value={
                        formData.email
                      }
                      onChange={
                        handleChange
                      }
                      placeholder="name@company.com"
                      maxLength={150}
                      disabled={saving}
                    />

                  </div>


                  {/* Address */}

                  <div className="company-field company-field-full">

                    <label htmlFor="address">
                      Address
                    </label>

                    <textarea
                      id="address"
                      name="address"
                      value={
                        formData.address
                      }
                      onChange={
                        handleChange
                      }
                      placeholder="Enter company address"
                      maxLength={500}
                      rows={4}
                      disabled={saving}
                    />

                  </div>


                  {/* Active */}

                  {editingCompany && (
                    <div className="company-status-field">

                      <label className="company-switch-label">

                        <input
                          type="checkbox"
                          name="isActive"
                          checked={
                            formData.isActive
                          }
                          onChange={
                            handleChange
                          }
                          disabled={saving}
                        />

                        <span className="company-switch">
                        </span>

                        <span>
                          Company Active
                        </span>

                      </label>

                    </div>
                  )}

                </div>

              </section>


              {/* =========================================
                  ACTIONS
              ========================================== */}

              <div className="company-form-actions">

                <button
                  type="button"
                  className="company-secondary-btn"
                  onClick={
                    handleCloseForm
                  }
                  disabled={saving}
                >
                  Cancel
                </button>

                <button
                  type="submit"
                  className="company-primary-btn"
                  disabled={saving}
                >

                  {saving ? (
                    <>
                      <span className="company-spinner" />
                      Saving...
                    </>
                  ) : (
                    <>
                      <span>✓</span>
                      {editingCompany
                        ? "Update Company"
                        : "Create Company"}
                    </>
                  )}

                </button>

              </div>

            </form>

          </div>

        ) : (

          /* =================================================
             COMPANY LIST
          ================================================= */

          <div className="company-list-card">

            {/* =============================================
                TOOLBAR
            ============================================== */}

            <div className="company-toolbar">

              <div className="company-search">

                <span>
                  🔍
                </span>

                <input
                  type="text"
                  value={
                    searchText
                  }
                  onChange={(e) =>
                    setSearchText(
                      e.target.value
                    )
                  }
                  placeholder="Search companies..."
                />

              </div>


              <select
                className="company-status-filter"
                value={
                  statusFilter
                }
                onChange={(e) =>
                  setStatusFilter(
                    e.target.value
                  )
                }
              >
                <option value="all">
                  All Status
                </option>

                <option value="active">
                  Active
                </option>

                <option value="inactive">
                  Inactive
                </option>
              </select>

            </div>


            {/* =============================================
                TABLE
            ============================================== */}

            <div className="company-table-wrapper">

              {loading ? (

                <div className="company-loading">

                  <span className="company-large-spinner" />

                  <p>
                    Loading companies...
                  </p>

                </div>

              ) : filteredCompanies.length === 0 ? (

                <div className="company-empty">

                  <div className="company-empty-icon">
                    🏢
                  </div>

                  <h3>
                    No companies found
                  </h3>

                  <p>
                    {searchText ||
                    statusFilter !== "all"
                      ? "Try changing your search or filter."
                      : "Create your first company to get started."}
                  </p>

                  {!searchText &&
                    statusFilter ===
                      "all" && (
                      <button
                        type="button"
                        className="company-primary-btn"
                        onClick={
                          handleCreate
                        }
                      >
                        + Add Company
                      </button>
                    )}

                </div>

              ) : (

                <table className="company-table">

                  <thead>

                    <tr>

                      <th>
                        Company
                      </th>

                      <th>
                        Owner
                      </th>

                      <th>
                        GSTIN
                      </th>

                      <th>
                        Contact
                      </th>

                      <th>
                        Status
                      </th>

                      <th className="company-action-column">
                        Actions
                      </th>

                    </tr>

                  </thead>

                  <tbody>

                    {filteredCompanies.map(
                      (company) => (

                        <tr
                          key={
                            company.companyId
                          }
                        >

                          {/* Company */}

                          <td>

                            <div className="company-name-cell">

                              <div className="company-avatar">
                                🏢
                              </div>

                              <div>

                                <strong>
                                  {
                                    company.companyName
                                  }
                                </strong>

                                <span>
                                  {
                                    company.companyCode
                                  }
                                </span>

                              </div>

                            </div>

                          </td>


                          {/* Owner */}

                          <td>

                            <span className="company-cell-text">

                              {company.ownerName ||
                                "—"}

                            </span>

                          </td>


                          {/* GSTIN */}

                          <td>

                            <span className="company-gstin">

                              {company.gstin ||
                                "—"}

                            </span>

                          </td>


                          {/* Contact */}

                          <td>

                            <div className="company-contact-cell">

                              {company.phoneNumber && (
                                <span>
                                  ☎{" "}
                                  {
                                    company.phoneNumber
                                  }
                                </span>
                              )}

                              {company.email && (
                                <span>
                                  ✉{" "}
                                  {
                                    company.email
                                  }
                                </span>
                              )}

                              {!company.phoneNumber &&
                                !company.email && (
                                  <span>
                                    —
                                  </span>
                                )}

                            </div>

                          </td>


                          {/* Status */}

                          <td>

                            <span
                              className={
                                company.isActive
                                  ? "company-status active"
                                  : "company-status inactive"
                              }
                            >

                              <span className="company-status-dot">
                              </span>

                              {company.isActive
                                ? "Active"
                                : "Inactive"}

                            </span>

                          </td>


                          {/* Actions */}

                          <td>

                            <div className="company-row-actions">

                              <button
                                type="button"
                                className="company-edit-btn"
                                onClick={() =>
                                  handleEdit(
                                    company
                                  )
                                }
                                title="Edit company"
                              >
                                ✎
                              </button>

                              <button
                                type="button"
                                className="company-delete-btn"
                                onClick={() =>
                                  setDeleteTarget(
                                    company
                                  )
                                }
                                title="Delete company"
                              >
                                🗑
                              </button>

                            </div>

                          </td>

                        </tr>

                      )
                    )}

                  </tbody>

                </table>

              )}

            </div>


            {/* =============================================
                FOOTER
            ============================================== */}

            {!loading &&
              filteredCompanies.length > 0 && (
                <div className="company-list-footer">

                  <span>
                    Showing{" "}
                    <strong>
                      {
                        filteredCompanies.length
                      }
                    </strong>{" "}
                    of{" "}
                    <strong>
                      {companies.length}
                    </strong>{" "}
                    companies
                  </span>

                </div>
              )}

          </div>

        )}

      </div>


      {/* ===================================================
          DELETE MODAL
      =================================================== */}

      {deleteTarget && (

        <div className="company-modal-overlay">

          <div className="company-delete-modal">

            <div className="company-delete-icon">
              !
            </div>

            <h2>
              Delete Company?
            </h2>

            <p>
              Are you sure you want to delete
              <strong>
                {" "}
                {deleteTarget.companyName}
              </strong>
              ?
            </p>

            <p className="company-delete-warning">
              This action may affect users and
              related records associated with
              this company.
            </p>

            <div className="company-delete-actions">

              <button
                type="button"
                className="company-secondary-btn"
                onClick={() =>
                  setDeleteTarget(null)
                }
                disabled={saving}
              >
                Cancel
              </button>

              <button
                type="button"
                className="company-danger-btn"
                onClick={
                  handleDelete
                }
                disabled={saving}
              >

                {saving
                  ? "Deleting..."
                  : "Delete Company"}

              </button>

            </div>

          </div>

        </div>

      )}

    </div>
  );
}


/*
===========================================================
ERROR HANDLER
===========================================================
*/

function getErrorMessage(
  err,
  fallbackMessage
) {

  if (err?.response) {

    const data =
      err.response.data;

    if (
      typeof data ===
      "string"
    ) {
      return data;
    }

    if (data?.message) {
      return data.message;
    }

    if (data?.title) {
      return data.title;
    }

    if (data?.errors) {

      const messages =
        Object.values(
          data.errors
        )
          .flat()
          .filter(Boolean);

      if (
        messages.length > 0
      ) {
        return messages.join(
          " "
        );
      }
    }

    return fallbackMessage;
  }

  if (err?.request) {

    return (
      "Unable to connect to the server. Please check your connection."
    );
  }

  return (
    err?.message ||
    fallbackMessage
  );
}