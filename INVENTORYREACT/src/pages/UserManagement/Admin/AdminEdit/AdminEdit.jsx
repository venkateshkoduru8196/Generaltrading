import {
    useEffect,
    useState
} from "react";

import {
    useNavigate,
    useParams
} from "react-router-dom";

import {
    getAdminById,
    updateAdmin
} from "../../../../services/adminService";

import {
    getCompanyLookup
} from "../../../../services/companyService";

import "./AdminEdit.css";


//====================================================
// INITIAL FORM
//====================================================

const initialForm = {
    userId: "",
    fullName: "",
    userName: "",
    email: "",
    phoneNumber: "",
    companyId: "",
    isActive: true
};


//====================================================
// COMPONENT
//====================================================

export default function AdminEdit() {

    const navigate = useNavigate();

    const {
        userId
    } = useParams();


    //================================================
    // FORM
    //================================================

    const [
        formData,
        setFormData
    ] = useState(initialForm);


    //================================================
    // COMPANIES
    //================================================

    const [
        companies,
        setCompanies
    ] = useState([]);


    const [
        companiesLoading,
        setCompaniesLoading
    ] = useState(true);


    //================================================
    // PAGE STATE
    //================================================

    const [
        loading,
        setLoading
    ] = useState(true);


    const [
        saving,
        setSaving
    ] = useState(false);


    const [
        error,
        setError
    ] = useState("");


    const [
        success,
        setSuccess
    ] = useState("");


    //====================================================
    // LOAD ADMIN + COMPANIES
    //====================================================

    useEffect(() => {

        const loadData = async () => {

            if (!userId) {

                setError(
                    "Administrator ID is missing."
                );

                setLoading(false);

                return;
            }


            setLoading(true);

            setCompaniesLoading(true);

            setError("");


            try {

                //================================================
                // LOAD ADMIN
                //================================================

                const admin =
                    await getAdminById(
                        userId
                    );


                console.log(
                    "ADMIN RESPONSE:",
                    admin
                );


                setFormData({

                    userId:
                        admin?.userId ??
                        userId,

                    fullName:
                        admin?.fullName ??
                        "",

                    userName:
                        admin?.userName ??
                        "",

                    email:
                        admin?.email ??
                        "",

                    phoneNumber:
                        admin?.phoneNumber ??
                        "",

                    companyId:
                        admin?.companyId ??
                        "",

                    isActive:
                        admin?.isActive ??
                        true
                });


                //================================================
                // LOAD COMPANIES
                //================================================

                const companyResponse =
                    await getCompanyLookup();


                console.log(
                    "COMPANY LOOKUP RESPONSE:",
                    companyResponse
                );


                const companyList =
                    Array.isArray(companyResponse)
                        ? companyResponse
                        : companyResponse?.items ??
                          companyResponse?.data ??
                          [];


                setCompanies(
                    companyList
                );

            }
            catch (err) {

                console.error(
                    "Unable to load administrator:",
                    err
                );


                setError(
                    err?.response?.data?.message ||
                    err?.response?.data?.Message ||
                    err?.message ||
                    "Unable to load administrator."
                );

            }
            finally {

                setLoading(false);

                setCompaniesLoading(false);

            }

        };


        loadData();

    }, [userId]);


    //====================================================
    // HANDLE CHANGE
    //====================================================

    const handleChange = (
        event
    ) => {

        const {
            name,
            value
        } = event.target;


        setFormData(
            previous => ({
                ...previous,

                [name]:
                    value
            })
        );


        setError("");

        setSuccess("");

    };


    //====================================================
    // ACTIVE STATUS
    //====================================================

    const handleStatusChange = (
        event
    ) => {

        setFormData(
            previous => ({
                ...previous,

                isActive:
                    event.target.checked
            })
        );

    };


    //====================================================
    // VALIDATE
    //====================================================

    const validate = () => {

        if (
            !formData.fullName.trim()
        ) {

            return "Full name is required.";

        }


        if (
            !formData.userName.trim()
        ) {

            return "Username is required.";

        }


        if (
            !formData.email.trim()
        ) {

            return "Email address is required.";

        }


        if (
            !formData.phoneNumber.trim()
        ) {

            return "Phone number is required.";

        }


        return "";

    };


    //====================================================
    // SAVE
    //====================================================

      //====================================================
// SAVE
//====================================================

const handleSubmit = async (event) => {

    event.preventDefault();

    setError("");
    setSuccess("");

    //================================================
    // VALIDATION
    //================================================

    const validationError = validate();

    if (validationError) {

        setError(validationError);

        return;
    }


    //================================================
    // USER ID
    //================================================

    if (!formData.userId) {

        setError(
            "Administrator ID is missing."
        );

        return;
    }


    //================================================
    // COMPANY
    //================================================

    const companyId =
        formData.companyId
            ? Number(formData.companyId)
            : null;


    //================================================
    // REQUEST
    //================================================

    const request = {

        fullName:
            formData.fullName.trim(),

        userName:
            formData.userName.trim(),

        email:
            formData.email.trim(),

        phoneNumber:
            formData.phoneNumber.trim(),

        companyId:

            companyId,

        isActive:
            formData.isActive
    };


    console.log(
        "UPDATE ADMIN USER ID:",
        formData.userId
    );

    console.log(
        "UPDATE ADMIN REQUEST:",
        request
    );


    //================================================
    // SAVE
    //================================================

    setSaving(true);

    try {

        await updateAdmin(
            formData.userId,
            request
        );


        //================================================
        // SUCCESS
        //================================================

        setSuccess(
            "Administrator updated successfully."
        );


        //================================================
        // NAVIGATE
        //================================================

        setTimeout(() => {

            navigate(
                "/user-management/admin"
            );

        }, 800);

    }
    catch (err) {

        console.error(
            "Unable to update administrator:",
            err
        );


        setError(

            err?.response?.data?.message ||

            err?.response?.data?.Message ||

            err?.message ||

            "Unable to update administrator."
        );

    }
    finally {

        setSaving(false);

    }
};

    //====================================================
    // CANCEL
    //====================================================

    const handleCancel = () => {

        if (saving)
            return;


        navigate(
            "/user-management/admin"
        );

    };


    //====================================================
    // LOADING
    //====================================================

    if (loading) {

        return (

            <div className="admin-edit-page">

                <div className="admin-edit-loading">

                    <div className="admin-edit-spinner" />

                    <span>
                        Loading administrator...
                    </span>

                </div>

            </div>

        );

    }


    //====================================================
    // RENDER
    //====================================================

    return (

        <div className="admin-edit-page">

            <div className="admin-edit-card">


                {/*================================================
                    HEADER
                =================================================*/}

                <div className="admin-edit-header">

                    <button
                        type="button"
                        className="admin-edit-back"
                        onClick={handleCancel}
                    >
                        ←
                    </button>


                    <div className="admin-edit-title">

                        <div className="admin-edit-icon">
                            ✎
                        </div>

                        <div>

                            <h1>
                                Edit Administrator
                            </h1>

                            <p>
                                Update administrator account information
                            </p>

                        </div>

                    </div>

                </div>


                {/*================================================
                    ALERT
                =================================================*/}

                {error && (

                    <div className="admin-edit-alert error">

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


                {success && (

                    <div className="admin-edit-alert success">

                        <span className="alert-icon">
                            ✓
                        </span>

                        <span>
                            {success}
                        </span>

                    </div>

                )}


                <form
                    onSubmit={handleSubmit}
                    noValidate
                >


                    {/*================================================
                        ACCOUNT INFORMATION
                    =================================================*/}

                    <section className="admin-edit-section">

                        <div className="admin-edit-section-heading">

                            <div className="admin-edit-section-number">
                                01
                            </div>

                            <div>

                                <h2>
                                    Account Information
                                </h2>

                                <p>
                                    Administrator identity and contact details
                                </p>

                            </div>

                        </div>


                        <div className="admin-edit-grid">

                            <div className="admin-edit-group full">

                                <label htmlFor="fullName">
                                    Full Name
                                    <span>*</span>
                                </label>

                                <input
                                    id="fullName"
                                    name="fullName"
                                    value={
                                        formData.fullName
                                    }
                                    onChange={
                                        handleChange
                                    }
                                    placeholder="Enter full name"
                                    disabled={saving}
                                />

                            </div>


                            <div className="admin-edit-group">

                                <label htmlFor="userName">
                                    Username
                                    <span>*</span>
                                </label>

                                <input
                                    id="userName"
                                    name="userName"
                                    value={
                                        formData.userName
                                    }
                                    onChange={
                                        handleChange
                                    }
                                    placeholder="Enter username"
                                    disabled={saving}
                                />

                            </div>


                            <div className="admin-edit-group">

                                <label htmlFor="phoneNumber">
                                    Phone Number
                                    <span>*</span>
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
                                    disabled={saving}
                                />

                            </div>


                            <div className="admin-edit-group full">

                                <label htmlFor="email">
                                    Email Address
                                    <span>*</span>
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
                                    disabled={saving}
                                />

                            </div>

                        </div>

                    </section>


                    {/*================================================
                        COMPANY
                    =================================================*/}

                    <section className="admin-edit-section">

                        <div className="admin-edit-section-heading">

                            <div className="admin-edit-section-number">
                                02
                            </div>

                            <div>

                                <h2>
                                    Company Assignment
                                </h2>

                                <p>
                                    Associate this administrator with a company
                                </p>

                            </div>

                        </div>


                        <div className="admin-edit-grid">

                            <div className="admin-edit-group">

                                <label htmlFor="companyId">
                                    Company
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
                                        saving ||
                                        companiesLoading
                                    }
                                >

                                    <option value="">
                                        {companiesLoading
                                            ? "Loading companies..."
                                            : "Select company"}
                                    </option>


                                    {companies.map(
                                        company => (

                                            <option
                                                key={
                                                    company.companyId
                                                }
                                                value={
                                                    company.companyId
                                                }
                                            >
                                                {
                                                    company.companyName
                                                }
                                                {company.companyCode
                                                    ? ` (${company.companyCode})`
                                                    : ""}
                                            </option>

                                        )
                                    )}

                                </select>


                                {!companiesLoading &&
                                    companies.length === 0 && (

                                        <small>
                                            No companies available.
                                        </small>

                                    )}

                            </div>

                        </div>

                    </section>


                    {/*================================================
                        ACCOUNT STATUS
                    =================================================*/}

                    <section className="admin-edit-section">

                        <div className="admin-edit-section-heading">

                            <div className="admin-edit-section-number">
                                03
                            </div>

                            <div>

                                <h2>
                                    Account Status
                                </h2>

                                <p>
                                    Control whether this administrator can access the system
                                </p>

                            </div>

                        </div>


                        <label className="admin-status-toggle">

                            <input
                                type="checkbox"
                                checked={
                                    formData.isActive
                                }
                                onChange={
                                    handleStatusChange
                                }
                                disabled={saving}
                            />

                            <span className="admin-toggle-slider" />

                            <span className="admin-toggle-content">

                                <strong>
                                    {formData.isActive
                                        ? "Active"
                                        : "Inactive"}
                                </strong>

                                <small>
                                    {formData.isActive
                                        ? "Administrator can sign in."
                                        : "Administrator cannot sign in."}
                                </small>

                            </span>

                        </label>

                    </section>


                    {/*================================================
                        SECURITY NOTE
                    =================================================*/}

                    <div className="admin-edit-security-note">

                        <span>
                            🔒
                        </span>

                        <div>

                            <strong>
                                Security credentials
                            </strong>

                            <p>
                                Login and edit passwords are not changed
                                from this profile screen. Use the dedicated
                                password/security operation when required.
                            </p>

                        </div>

                    </div>


                    {/*================================================
                        ACTIONS
                    =================================================*/}

                    <div className="admin-edit-actions">

                        <button
                            type="button"
                            className="admin-edit-cancel"
                            onClick={handleCancel}
                            disabled={saving}
                        >
                            Cancel
                        </button>


                        <button
                            type="submit"
                            className="admin-edit-save"
                            disabled={saving}
                        >

                            {saving ? (

                                <>
                                    <span className="admin-edit-button-spinner" />
                                    Saving...
                                </>

                            ) : (

                                <>
                                    ✓
                                    Save Changes
                                </>

                            )}

                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

}