import {
    useEffect,
    useState
} from "react";

import {
    useNavigate
} from "react-router-dom";

import {
    createAdmin
} from "../../../../services/adminService";

import {
    getCompanyLookup
} from "../../../../services/companyService";

import "./AdminRegistration.css";


//==========================================================
// INITIAL FORM
//==========================================================

const initialForm = {

    fullName: "",

    userName: "",

    email: "",

    phoneNumber: "",

    password: "",

    confirmPassword: "",

    editPassword: "",

    confirmEditPassword: "",

    companyId: ""

};


//==========================================================
// ADMIN REGISTRATION
//==========================================================

export default function AdminRegistration() {

    const navigate = useNavigate();


    //======================================================
    // FORM
    //======================================================

    const [formData, setFormData] =
        useState(initialForm);


    //======================================================
    // LOADING
    //======================================================

    const [loading, setLoading] =
        useState(false);


    //======================================================
    // COMPANY LOADING
    //======================================================

    const [companyLoading, setCompanyLoading] =
        useState(false);


    //======================================================
    // COMPANIES
    //======================================================

    const [companies, setCompanies] =
        useState([]);


    //======================================================
    // ERROR
    //======================================================

    const [error, setError] =
        useState("");


    //======================================================
    // SUCCESS
    //======================================================

    const [success, setSuccess] =
        useState("");


    //======================================================
    // LOAD COMPANY LOOKUP
    //======================================================

    useEffect(() => {

        const loadCompanies = async () => {

            setCompanyLoading(true);

            try {

                const response =
                    await getCompanyLookup();


                /*
                ==================================================
                SUPPORT COMMON RESPONSE SHAPES
                ==================================================

                Direct array:

                [
                    {
                        companyId: 1,
                        companyName: "ABC"
                    }
                ]

                Or:

                {
                    data: [...]
                }
                */

                const items =
                    Array.isArray(response)
                        ? response
                        : response?.data ??
                          response?.items ??
                          [];


                setCompanies(
                    Array.isArray(items)
                        ? items
                        : []
                );

            }
            catch (err) {

                console.error(
                    "Error loading companies:",
                    err
                );


                setCompanies([]);


                setError(
                    err?.response?.data?.message ||
                    err?.response?.data?.Message ||
                    "Unable to load companies."
                );

            }
            finally {

                setCompanyLoading(false);

            }

        };


        loadCompanies();

    }, []);


    //======================================================
    // HANDLE CHANGE
    //======================================================

    const handleChange = (event) => {

        const {
            name,
            value
        } = event.target;


        setFormData(previous => ({

            ...previous,

            [name]: value

        }));


        if (error) {

            setError("");

        }

    };


    //======================================================
    // VALIDATION
    //======================================================

    const validateForm = () => {

        if (!formData.fullName.trim()) {

            return "Full name is required.";

        }


        if (!formData.userName.trim()) {

            return "Username is required.";

        }


        if (!formData.email.trim()) {

            return "Email is required.";

        }


        if (!formData.phoneNumber.trim()) {

            return "Phone number is required.";

        }


        if (!formData.password) {

            return "Password is required.";

        }


        if (
            formData.password !==
            formData.confirmPassword
        ) {

            return "Passwords do not match.";

        }


        if (!formData.editPassword) {

            return "Edit password is required.";

        }


        if (
            formData.editPassword !==
            formData.confirmEditPassword
        ) {

            return "Edit passwords do not match.";

        }


        /*
        ==================================================
        COMPANY VALIDATION
        ==================================================

        We require a company because your ApplicationUser
        supports CompanyId and this Admin belongs to a
        company.

        ==================================================
        */

        if (!formData.companyId) {

            return "Please select a company.";

        }


        return "";

    };


    //======================================================
    // SUBMIT
    //======================================================

    const handleSubmit = async (event) => {

        event.preventDefault();


        setError("");

        setSuccess("");


        const validationError =
            validateForm();


        if (validationError) {

            setError(
                validationError
            );

            return;

        }


        setLoading(true);


        try {

            await createAdmin({

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

                role: "Admin",

                companyId:
                    formData.companyId
                        ? Number(formData.companyId)
                        : null

            });


            setSuccess(
                "Administrator created successfully."
            );


            setFormData(
                initialForm
            );


            /*
            ==================================================
            REDIRECT
            ==================================================
            */

            setTimeout(() => {

                navigate(
                    "/user-management/admin"
                );

            }, 900);

        }
        catch (err) {

            console.error(
                "Create administrator error:",
                err
            );


            setError(

                err?.response?.data?.message ||

                err?.response?.data?.Message ||

                err?.response?.data?.errors
                    ? "Unable to create administrator. Please check the entered information."
                    : "Unable to create administrator."

            );

        }
        finally {

            setLoading(false);

        }

    };


    //======================================================
    // CANCEL
    //======================================================

    const handleCancel = () => {

        if (loading)
            return;


        navigate(
            "/user-management/admin"
        );

    };


    //======================================================
    // RENDER
    //======================================================

    return (

        <div className="admin-registration-page">

            <div className="admin-registration-card">


                {/*================================================
                    HEADER
                =================================================*/}

                <div className="admin-registration-header">

                    <button
                        type="button"
                        className="admin-registration-back"
                        onClick={handleCancel}
                        disabled={loading}
                    >
                        ←
                    </button>


                    <div className="admin-registration-title">

                        <div className="admin-registration-icon">

                            👤

                        </div>


                        <div>

                            <h1>
                                Create Administrator
                            </h1>

                            <p>
                                Create a new administrator account
                            </p>

                        </div>

                    </div>

                </div>


                {/*================================================
                    ALERT - ERROR
                =================================================*/}

                {error && (

                    <div className="admin-registration-alert error">

                        <span className="alert-icon">
                            !
                        </span>


                        <span>
                            {error}
                        </span>


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


                {/*================================================
                    ALERT - SUCCESS
                =================================================*/}

                {success && (

                    <div className="admin-registration-alert success">

                        <span className="alert-icon">
                            ✓
                        </span>


                        <span>
                            {success}
                        </span>

                    </div>

                )}


                {/*================================================
                    FORM
                =================================================*/}

                <form
                    onSubmit={handleSubmit}
                    noValidate
                >


                    {/*================================================
                        PERSONAL INFORMATION
                    =================================================*/}

                    <section className="admin-form-section">

                        <div className="admin-section-heading">

                            <div className="section-number">
                                01
                            </div>


                            <div>

                                <h2>
                                    Personal Information
                                </h2>

                                <p>
                                    Basic administrator account information
                                </p>

                            </div>

                        </div>


                        <div className="admin-form-grid">


                            {/* FULL NAME */}

                            <div className="admin-form-group full">

                                <label htmlFor="fullName">

                                    Full Name

                                    <span>
                                        *
                                    </span>

                                </label>


                                <input
                                    id="fullName"
                                    name="fullName"
                                    type="text"
                                    value={
                                        formData.fullName
                                    }
                                    onChange={
                                        handleChange
                                    }
                                    placeholder="Enter full name"
                                    autoComplete="name"
                                    disabled={loading}
                                />

                            </div>


                            {/* USERNAME */}

                            <div className="admin-form-group">

                                <label htmlFor="userName">

                                    Username

                                    <span>
                                        *
                                    </span>

                                </label>


                                <input
                                    id="userName"
                                    name="userName"
                                    type="text"
                                    value={
                                        formData.userName
                                    }
                                    onChange={
                                        handleChange
                                    }
                                    placeholder="Enter username"
                                    autoComplete="username"
                                    disabled={loading}
                                />

                            </div>


                            {/* PHONE */}

                            <div className="admin-form-group">

                                <label htmlFor="phoneNumber">

                                    Phone Number

                                    <span>
                                        *
                                    </span>

                                </label>


                                <input
                                    id="phoneNumber"
                                    name="phoneNumber"
                                    type="tel"
                                    value={
                                        formData.phoneNumber
                                    }
                                    onChange={
                                        handleChange
                                    }
                                    placeholder="Enter phone number"
                                    autoComplete="tel"
                                    disabled={loading}
                                />

                            </div>


                            {/* EMAIL */}

                            <div className="admin-form-group full">

                                <label htmlFor="email">

                                    Email Address

                                    <span>
                                        *
                                    </span>

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
                                    placeholder="Enter email address"
                                    autoComplete="email"
                                    disabled={loading}
                                />

                            </div>

                        </div>

                    </section>


                    {/*================================================
                        ACCOUNT SECURITY
                    =================================================*/}

                    <section className="admin-form-section">

                        <div className="admin-section-heading">

                            <div className="section-number">
                                02
                            </div>


                            <div>

                                <h2>
                                    Account Security
                                </h2>

                                <p>
                                    Configure login credentials
                                </p>

                            </div>

                        </div>


                        <div className="admin-form-grid">


                            {/* PASSWORD */}

                            <div className="admin-form-group">

                                <label htmlFor="password">

                                    Password

                                    <span>
                                        *
                                    </span>

                                </label>


                                <input
                                    id="password"
                                    name="password"
                                    type="password"
                                    value={
                                        formData.password
                                    }
                                    onChange={
                                        handleChange
                                    }
                                    placeholder="Enter password"
                                    autoComplete="new-password"
                                    disabled={loading}
                                />

                            </div>


                            {/* CONFIRM PASSWORD */}

                            <div className="admin-form-group">

                                <label htmlFor="confirmPassword">

                                    Confirm Password

                                    <span>
                                        *
                                    </span>

                                </label>


                                <input
                                    id="confirmPassword"
                                    name="confirmPassword"
                                    type="password"
                                    value={
                                        formData.confirmPassword
                                    }
                                    onChange={
                                        handleChange
                                    }
                                    placeholder="Confirm password"
                                    autoComplete="new-password"
                                    disabled={loading}
                                />

                            </div>

                        </div>

                    </section>


                    {/*================================================
                        EDIT AUTHORIZATION
                    =================================================*/}

                    <section className="admin-form-section">

                        <div className="admin-section-heading">

                            <div className="section-number">
                                03
                            </div>


                            <div>

                                <h2>
                                    Edit Authorization
                                </h2>

                                <p>
                                    Security credential used for protected changes
                                </p>

                            </div>

                        </div>


                        <div className="admin-form-grid">


                            {/* EDIT PASSWORD */}

                            <div className="admin-form-group">

                                <label htmlFor="editPassword">

                                    Edit Password

                                    <span>
                                        *
                                    </span>

                                </label>


                                <input
                                    id="editPassword"
                                    name="editPassword"
                                    type="password"
                                    value={
                                        formData.editPassword
                                    }
                                    onChange={
                                        handleChange
                                    }
                                    placeholder="Enter edit password"
                                    autoComplete="new-password"
                                    disabled={loading}
                                />

                            </div>


                            {/* CONFIRM EDIT PASSWORD */}

                            <div className="admin-form-group">

                                <label htmlFor="confirmEditPassword">

                                    Confirm Edit Password

                                    <span>
                                        *
                                    </span>

                                </label>


                                <input
                                    id="confirmEditPassword"
                                    name="confirmEditPassword"
                                    type="password"
                                    value={
                                        formData.confirmEditPassword
                                    }
                                    onChange={
                                        handleChange
                                    }
                                    placeholder="Confirm edit password"
                                    autoComplete="new-password"
                                    disabled={loading}
                                />

                            </div>

                        </div>

                    </section>


                    {/*================================================
                        COMPANY ASSIGNMENT
                    =================================================*/}

                    <section className="admin-form-section">

                        <div className="admin-section-heading">

                            <div className="section-number">
                                04
                            </div>


                            <div>

                                <h2>
                                    Company Assignment
                                </h2>

                                <p>
                                    Associate the administrator with a company
                                </p>

                            </div>

                        </div>


                        <div className="admin-form-grid">


                            <div className="admin-form-group">

                                <label htmlFor="companyId">

                                    Company

                                    <span>
                                        *
                                    </span>

                                </label>


                                <select
                                    id="companyId"
                                    name="companyId"
                                    value={
                                        formData.companyId
                                    }
                                    onChange={
                                        handleChange
                                    }
                                    disabled={
                                        loading ||
                                        companyLoading
                                    }
                                >

                                    {/* DEFAULT */}

                                    <option value="">

                                        {companyLoading
                                            ? "Loading companies..."
                                            : companies.length === 0
                                                ? "No companies available"
                                                : "Select company"}

                                    </option>


                                    {/* COMPANY OPTIONS */}

                                    {companies.map(
                                        (company) => (

                                            <option
                                                key={
                                                    company.companyId
                                                }
                                                value={
                                                    company.companyId
                                                }
                                            >

                                                {company.companyName}

                                                {company.companyCode
                                                    ? ` (${company.companyCode})`
                                                    : ""}

                                            </option>

                                        )
                                    )}

                                </select>


                                <small>

                                    {companyLoading

                                        ? "Loading active companies..."

                                        : companies.length > 0

                                            ? `${companies.length} active company${companies.length === 1 ? "" : "ies"} available.`

                                            : "No active companies are available."}

                                </small>

                            </div>

                        </div>

                    </section>


                    {/*================================================
                        ACTIONS
                    =================================================*/}

                    <div className="admin-registration-actions">


                        <button
                            type="button"
                            className="admin-cancel-btn"
                            onClick={handleCancel}
                            disabled={loading}
                        >
                            Cancel
                        </button>


                        <button
                            type="submit"
                            className="admin-save-btn"
                            disabled={
                                loading ||
                                companyLoading
                            }
                        >

                            {loading ? (

                                <>

                                    <span className="admin-button-spinner" />

                                    Creating...

                                </>

                            ) : (

                                <>

                                    <span>
                                        ✓
                                    </span>

                                    Create Administrator

                                </>

                            )}

                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

}