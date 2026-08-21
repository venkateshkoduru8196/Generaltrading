import "./PaymentDetailGrid.css";

export default function PaymentDetailGrid({
  details,
  accounts,
  summary,
  onChange,
  onAdd,
  onDelete,
  readOnly,
}) {
  const handleFieldChange = (rowIndex, field, value) => {
    const updatedRows = [...details];

    updatedRows[rowIndex] = {
      ...updatedRows[rowIndex],
      [field]: value,
    };

    onChange(updatedRows);
  };

  const handleAmountKeyDown = (event, index) => {
    if (event.key !== "Enter" || readOnly) return;

    event.preventDefault();

    if (index === details.length - 1) {
      onAdd();
    }
  };

  return (
    <div className="payment-grid-card">
      <div className="payment-grid-title">
        <h3>Payment Details</h3>
      </div>

      <div className="payment-table-responsive">
        <table className="payment-table">
          <thead>
            <tr>
              <th width="70">Sl No</th>
              <th>Account</th>
              <th width="220">Amount</th>
              <th width="180">Action</th>
            </tr>
          </thead>

          <tbody>
            {details.map((row, index) => (
              <tr key={`${row.slNo}-${index}`}>
                <td>{row.slNo}</td>

                <td>
                  <select
                    value={row.accountId}
                    disabled={readOnly}
                    onChange={(e) =>
                      handleFieldChange(index, "accountId", e.target.value)
                    }
                  >
                    <option value="">Select Account</option>

                    {accounts.map((account) => (
                      <option key={account.id} value={account.id}>
                        {account.accountCode} - {account.accountName}
                      </option>
                    ))}
                  </select>
                </td>

                <td>
                  <input
                    type="number"
                    min="0"
                    step="0.01"
                    value={row.amount}
                    disabled={readOnly}
                    placeholder="Enter Amount"
                    onChange={(e) =>
                      handleFieldChange(index, "amount", e.target.value)
                    }
                    onKeyDown={(e) => handleAmountKeyDown(e, index)}
                  />
                </td>

                <td>
                  <div className="payment-action-buttons">
                    <button
                      type="button"
                      className="payment-add-btn"
                      onClick={onAdd}
                      disabled={readOnly}
                      title="Add Row"
                    >
                      +
                    </button>

                    <button
                      type="button"
                      className="payment-delete-row-btn"
                      onClick={() => onDelete(index)}
                      disabled={readOnly}
                      title="Delete Row"
                    >
                      Remove
                    </button>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className="payment-grid-bottom">
        <button
          type="button"
          className="payment-add-row-btn"
          onClick={onAdd}
          disabled={readOnly}
        >
          + Add Row
        </button>

        <div className="payment-total">
          Total : ₹ {Number(summary.totalAmount || 0).toFixed(2)}
        </div>
      </div>
    </div>
  );
}
