import "./AdminHeader.css";

export default function AdminHeader({
    onCreate
}) {

    return (

        <div className="admin-header">

            <div className="admin-header-left">

                <div className="admin-header-icon">
                    👥
                </div>

                <div>

                    <h1>
                        Administrator Management
                    </h1>

                    <p>
                        Manage administrator accounts and access
                    </p>

                </div>

            </div>


            <button
                type="button"
                className="admin-create-btn"
                onClick={onCreate}
            >
                <span className="admin-create-icon">
                    +
                </span>

                Create Admin
            </button>

        </div>

    );
}