import "./AdminActionBar.css";

export default function AdminActionBar({
    totalRecords,
    onRefresh,
    loading
}) {

    return (

        <div className="admin-action-bar">

            <div className="admin-record-info">

                <span className="admin-record-label">
                    Administrators
                </span>

                <span className="admin-record-count">
                    {totalRecords}
                </span>

            </div>


            <button
                type="button"
                className="admin-refresh-btn"
                onClick={onRefresh}
                disabled={loading}
            >

                <span
                    className={
                        loading
                            ? "admin-refresh-icon spinning"
                            : "admin-refresh-icon"
                    }
                >
                    ↻
                </span>

                Refresh

            </button>

        </div>

    );
}