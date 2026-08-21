import AdminButtons from "./AdminButtons";

import "./AdminTable.css";

export default function AdminTable({
    admins,
    loading,
    onEdit,
    onStatusChange,
    onDelete
}) {

    //====================================================
    // LOADING
    //====================================================

    if (loading) {

        return (

            <div className="admin-table-container">

                <div className="admin-table-loading">

                    <div className="admin-loader" />

                    <span>
                        Loading administrators...
                    </span>

                </div>

            </div>

        );
    }


    //====================================================
    // EMPTY
    //====================================================

    if (!admins || !admins.length) {

        return (

            <div className="admin-empty-state">

                <div className="admin-empty-icon">
                    👥
                </div>

                <h3>
                    No administrators found
                </h3>

                <p>
                    No administrator accounts match your search.
                </p>

            </div>

        );
    }


    //====================================================
    // TABLE
    //====================================================

    return (

        <div className="admin-table-container">

            <div className="admin-table-scroll">

                <table className="admin-table">

                    <thead>

                        <tr>

                            <th>
                                Administrator
                            </th>

                            <th>
                                Username
                            </th>

                            <th>
                                Email
                            </th>

                            <th>
                                Phone
                            </th>

                            <th>
                                Company
                            </th>

                            <th>
                                Status
                            </th>

                            <th>
                                Last Login
                            </th>

                            <th className="actions-column">
                                Actions
                            </th>

                        </tr>

                    </thead>


                    <tbody>

                        {admins.map((admin) => (

                            <tr
                                key={admin.userId}
                            >

                                {/* =========================================
                                    ADMINISTRATOR
                                ========================================== */}

                                <td>

                                    <div className="admin-user-cell">

                                        <div className="admin-avatar">

                                            {getInitials(
                                                admin.fullName
                                            )}

                                        </div>


                                        <div className="admin-user-details">

                                            <strong>
                                                {admin.fullName || "-"}
                                            </strong>

                                            <span>
                                                Administrator
                                            </span>

                                        </div>

                                    </div>

                                </td>


                                {/* =========================================
                                    USERNAME
                                ========================================== */}

                                <td>
                                    {admin.userName || "-"}
                                </td>


                                {/* =========================================
                                    EMAIL
                                ========================================== */}

                                <td>
                                    {admin.email || "-"}
                                </td>


                                {/* =========================================
                                    PHONE
                                ========================================== */}

                                <td>
                                    {admin.phoneNumber || "-"}
                                </td>


                                {/* =========================================
                                    COMPANY
                                ========================================== */}

                                <td>
                                    {admin.companyName || "-"}
                                </td>


                                {/* =========================================
                                    STATUS
                                ========================================== */}

                                <td>

                                    <span
                                        className={
                                            admin.isActive
                                                ? "admin-status active"
                                                : "admin-status inactive"
                                        }
                                    >

                                        <span className="status-dot" />

                                        {admin.isActive
                                            ? "Active"
                                            : "Inactive"}

                                    </span>

                                </td>


                                {/* =========================================
                                    LAST LOGIN
                                ========================================== */}

                                <td>

                                    {formatDate(
                                        admin.lastLoginOn
                                    )}

                                </td>


                                {/* =========================================
                                    ACTIONS
                                ========================================== */}

                                <td className="actions-column">

                                    <AdminButtons
                                        user={admin}
                                        onEdit={onEdit}
                                        onStatusChange={
                                            onStatusChange
                                        }
                                        onDelete={
                                            onDelete
                                        }
                                    />

                                </td>

                            </tr>

                        ))}

                    </tbody>

                </table>

            </div>


            {/* =========================================================
                MOBILE CARDS
            ========================================================== */}

            <div className="admin-mobile-list">

                {admins.map((admin) => (

                    <div
                        className="admin-mobile-card"
                        key={admin.userId}
                    >

                        <div className="admin-mobile-top">

                            <div className="admin-user-cell">

                                <div className="admin-avatar">

                                    {getInitials(
                                        admin.fullName
                                    )}

                                </div>


                                <div className="admin-user-details">

                                    <strong>
                                        {admin.fullName || "-"}
                                    </strong>

                                    <span>
                                        {admin.userName || "-"}
                                    </span>

                                </div>

                            </div>


                            <AdminButtons
                                user={admin}
                                onEdit={onEdit}
                                onStatusChange={
                                    onStatusChange
                                }
                                onDelete={
                                    onDelete
                                }
                            />

                        </div>


                        <div className="admin-mobile-details">

                            {/* EMAIL */}

                            <div>

                                <span>
                                    Email
                                </span>

                                <strong>
                                    {admin.email || "-"}
                                </strong>

                            </div>


                            {/* PHONE */}

                            <div>

                                <span>
                                    Phone
                                </span>

                                <strong>
                                    {admin.phoneNumber || "-"}
                                </strong>

                            </div>


                            {/* COMPANY */}

                            <div>

                                <span>
                                    Company
                                </span>

                                <strong>
                                    {admin.companyName || "-"}
                                </strong>

                            </div>


                            {/* STATUS */}

                            <div>

                                <span>
                                    Status
                                </span>

                                <span
                                    className={
                                        admin.isActive
                                            ? "admin-status active"
                                            : "admin-status inactive"
                                    }
                                >

                                    <span className="status-dot" />

                                    {admin.isActive
                                        ? "Active"
                                        : "Inactive"}

                                </span>

                            </div>

                        </div>

                    </div>

                ))}

            </div>

        </div>

    );
}


//======================================================
// GET INITIALS
//======================================================

function getInitials(name) {

    if (!name)
        return "?";

    return name
        .trim()
        .split(/\s+/)
        .slice(0, 2)
        .map(
            word =>
                word
                    .charAt(0)
                    .toUpperCase()
        )
        .join("");
}


//======================================================
// FORMAT DATE
//======================================================

function formatDate(value) {

    if (!value)
        return "Never";

    const date =
        new Date(value);

    if (
        Number.isNaN(
            date.getTime()
        )
    ) {
        return "-";
    }

    return date.toLocaleDateString(
        "en-IN",
        {
            day: "2-digit",
            month: "short",
            year: "numeric"
        }
    );
}