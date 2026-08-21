import "./AccountTable.css";

export default function AccountTable({
  accounts,

  selectedId,

  setSelectedId,

  searchText,

  setSearchText,

  searchBy,

  setSearchBy,

  mode,
}) {
  //==========================================================
  // Disable Search During New / Edit
  //==========================================================

  const disabled = mode === "new" || mode === "edit";

  return (
    <div className="account-table-card">
      {/*==================================================
                Header
            ==================================================*/}

      <div className="account-table-header">
        <h3>ACCOUNT LIST</h3>

        <span>Total Records : {accounts.length}</span>
      </div>

      {/*==================================================
                Toolbar
            ==================================================*/}

      <div className="account-table-toolbar">
        {/*================================================
                    Search
                =================================================*/}

        <div className="account-table-search-box">
          <span className="account-table-search-icon">🔍</span>

          <input
            type="text"
            placeholder="Search..."
            value={searchText}
            disabled={disabled}
            autoComplete="off"
            onChange={(e) => setSearchText(e.target.value)}
          />
        </div>

        {/*================================================
                    Search By
                =================================================*/}

        <div className="account-table-filter">
          <label>Search By</label>

          <select
            value={searchBy}
            disabled={disabled}
            onChange={(e) => setSearchBy(e.target.value)}
          >
            <option value="accountCode">Account Code</option>

            <option value="accountName">Account Name</option>

            <option value="actype">Account Type</option>

            <option value="status">Status</option>

            <option value="createdBy">Created By</option>

            <option value="createdOn">Created On</option>
          </select>
        </div>
      </div>

      {/*==================================================
                Table
            ==================================================*/}

      <div className="account-table-wrapper">
        <table className="account-table">
          <thead>
            <tr>
              <th width="70">Sl</th>

              <th width="220">Account Code</th>

              <th>Account Name</th>

              {/*================================================
                                Account Type
                            =================================================*/}

              <th width="150">Account Type</th>

              <th width="150">Status</th>

              <th width="180">Created By</th>

              <th width="170">Created On</th>
            </tr>
          </thead>

          <tbody>
            {accounts.length === 0 ? (
              <tr>
                <td colSpan="7" className="account-no-data">
                  No Accounts Found
                </td>
              </tr>
            ) : (
              accounts.map((item, index) => (
                <tr
                  key={item.id}
                  className={
                    selectedId === item.id ? "account-selected-row" : ""
                  }
                  onClick={() => setSelectedId(item.id)}
                >
                  {/* Sl */}

                  <td>{index + 1}</td>

                  {/* Account Code */}

                  <td className="account-code">{item.accountCode}</td>

                  {/* Account Name */}

                  <td>{item.accountName}</td>

                  {/*================================================
                                                    Account Type
                                                =================================================*/}

                  <td>
                    <span className="account-type-badge">
                      {item.actype === "G"
                        ? "General"
                        : item.actype === "B"
                          ? "Bank/Cash"
                          : item.actype === "C"
                            ? "Customer"
                            : item.actype === "S"
                              ? "Supplier"
                              : "-"}
                    </span>
                  </td>

                  {/* Status */}

                  <td>
                    <span
                      className={
                        item.isActive
                          ? "account-status account-active"
                          : "account-status account-inactive"
                      }
                    >
                      {item.isActive ? "Active" : "Inactive"}
                    </span>
                  </td>

                  {/* Created By */}

                  <td>{item.createdBy || "-"}</td>

                  {/* Created On */}

                  <td>
                    {item.createdOn
                      ? new Date(item.createdOn).toLocaleDateString()
                      : "-"}
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
