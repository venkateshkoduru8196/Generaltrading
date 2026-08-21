import "./AdminSearch.css";

export default function AdminSearch({
    filters,
    onSearchChange,
    onStatusChange,
    onCompanyChange,
    onClear,
    showCompanyFilter = false
}) {

    return (

        <div className="admin-search">

            <div className="admin-search-main">

                <span className="admin-search-icon">
                    🔍
                </span>

                <input
                    type="text"
                    value={filters.search}
                    onChange={(e) =>
                        onSearchChange(e.target.value)
                    }
                    placeholder="Search administrators..."
                    aria-label="Search administrators"
                />

            </div>


            <select
                className="admin-filter-select"
                value={
                    filters.isActive === null
                        ? ""
                        : String(filters.isActive)
                }
                onChange={(e) =>
                    onStatusChange(e.target.value)
                }
                aria-label="Filter by status"
            >

                <option value="">
                    All Status
                </option>

                <option value="true">
                    Active
                </option>

                <option value="false">
                    Inactive
                </option>

            </select>


            {showCompanyFilter && (

                <select
                    className="admin-filter-select"
                    value={filters.companyId ?? ""}
                    onChange={(e) =>
                        onCompanyChange(e.target.value)
                    }
                    aria-label="Filter by company"
                >

                    <option value="">
                        All Companies
                    </option>

                    {/* Company options will be connected
                        to your Company lookup API. */}

                </select>

            )}


            <button
                type="button"
                className="admin-clear-btn"
                onClick={onClear}
            >
                Clear
            </button>

        </div>

    );
}