import "./AdminSummary.css";

export default function AdminSummary({
    totalRecords = 0,
    activeCount = 0,
    inactiveCount = 0
}) {

    return (

        <div className="admin-summary">

            <div className="admin-summary-card">

                <div className="admin-summary-icon total">
                    👥
                </div>

                <div className="admin-summary-content">

                    <span>
                        Total Admins
                    </span>

                    <strong>
                        {totalRecords}
                    </strong>

                </div>

            </div>


            <div className="admin-summary-card">

                <div className="admin-summary-icon active">
                    ✓
                </div>

                <div className="admin-summary-content">

                    <span>
                        Active
                    </span>

                    <strong>
                        {activeCount}
                    </strong>

                </div>

            </div>


            <div className="admin-summary-card">

                <div className="admin-summary-icon inactive">
                    !
                </div>

                <div className="admin-summary-content">

                    <span>
                        Inactive
                    </span>

                    <strong>
                        {inactiveCount}
                    </strong>

                </div>

            </div>

        </div>

    );
}