import "./SalesDetailGrid.css";

export default function SalesDetailGrid({
  details,
  stockItems,
  units,
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

    //==========================================
    // Auto Fill Stock Name & Tax Rate
    //==========================================

    if (field === "stockCode") {
      const stock = stockItems.find(
        (x) => x.stockCode === value
      );

      if (stock) {
        updatedRows[rowIndex].stockName = stock.stockName;
        updatedRows[rowIndex].taxRate = stock.taxRate;
      } else {
        updatedRows[rowIndex].stockName = "";
        updatedRows[rowIndex].taxRate = 0;
      }
    }

    onChange(updatedRows);
  };

  return (
    <div className="sales-grid-card">
      <div className="grid-title">
        <h3>Item Details</h3>
      </div>

      <div className="table-responsive">
        <table className="sales-table">
          <thead>
            <tr>
              <th width="60">Sl</th>

              <th width="220">Stock</th>

              <th width="220">Stock Name</th>

              <th width="250">Description</th>

              <th width="150">Unit</th>

              <th width="100">Qty</th>

              <th width="120">Rate</th>

              <th width="120">Amount</th>

              <th width="120">Taxable</th>

              <th width="100">Tax %</th>

              <th width="120">Tax Amt</th>

              <th width="120">Action</th>
            </tr>
          </thead>

          <tbody>
            {details.map((row, index) => (
              <tr key={row.slNo}>
                {/* Sl No */}

                <td>{row.slNo}</td>

                {/* Stock */}

                <td>
                  <select
                    value={row.stockCode}
                    disabled={readOnly}
                    onChange={(e) =>
                      handleFieldChange(
                        index,
                        "stockCode",
                        e.target.value
                      )
                    }
                  >
                    <option value="">Select Stock</option>

                    {stockItems.map((stock) => (
                      <option
                        key={stock.id}
                        value={stock.stockCode}
                      >
                        {stock.stockCode} - {stock.stockName}
                      </option>
                    ))}
                  </select>
                </td>

                {/* Stock Name */}

                <td>
                  <input
                    type="text"
                    value={row.stockName}
                    readOnly
                  />
                </td>

                {/* Description */}

                <td>
                  <input
                    type="text"
                    value={row.description}
                    disabled={readOnly}
                    onChange={(e) =>
                      handleFieldChange(
                        index,
                        "description",
                        e.target.value
                      )
                    }
                  />
                </td>

                {/* Unit */}

                <td>
                  <select
                    value={row.unitCode}
                    disabled={readOnly}
                    onChange={(e) =>
                      handleFieldChange(
                        index,
                        "unitCode",
                        e.target.value
                      )
                    }
                  >
                    <option value="">Select Unit</option>

                    {units.map((unit) => (
                      <option
                        key={unit.id}
                        value={unit.code}
                      >
                        {unit.description}
                      </option>
                    ))}
                  </select>
                </td>

                                {/* Qty */}

                <td>
                  <input
                    type="number"
                    min="0"
                    value={row.qty}
                    disabled={readOnly}
                    onChange={(e) =>
                      handleFieldChange(
                        index,
                        "qty",
                        e.target.value
                      )
                    }
                  />
                </td>

                {/* Rate */}

                <td>
                  <input
                    type="number"
                    min="0"
                    value={row.rate}
                    disabled={readOnly}
                    onChange={(e) =>
                      handleFieldChange(
                        index,
                        "rate",
                        e.target.value
                      )
                    }
                  />
                </td>

                {/* Amount */}

                <td>
                  <input
                    type="number"
                    value={row.amount}
                    readOnly
                  />
                </td>

                {/* Taxable Amount */}

                <td>
                  <input
                    type="number"
                    value={row.taxableAmount}
                    readOnly
                  />
                </td>

                {/* Tax Rate */}

                <td>
                  <input
                    type="number"
                    value={row.taxRate}
                    readOnly
                  />
                </td>

                {/* Tax Amount */}

                <td>
                  <input
                    type="number"
                    value={row.taxAmount}
                    readOnly
                  />
                </td>

                {/* Action */}

                <td>
                  <div className="action-buttons">
                    <button
                      type="button"
                      className="add-btn"
                      onClick={onAdd}
                      disabled={readOnly}
                    >
                      +
                    </button>

                    <button
                      type="button"
                      className="delete-btn"
                      onClick={() => onDelete(index)}
                      disabled={
                        readOnly ||
                        details.length === 1
                      }
                    >
                      -
                    </button>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>


        



        <tfoot>


    <tr className="totals-row">

        <td colSpan="5" className="total-title">
            TOTALS
        </td>

        {/* Qty */}
        <td className="total-value">
            {summary.totalQty}
        </td>

        {/* Rate */}
        <td></td>

        {/* Amount */}
        <td className="total-value">
            ₹ {summary.totalAmount.toFixed(2)}
        </td>

        {/* Taxable */}
        <td className="total-value">
            ₹ {summary.totalAmount.toFixed(2)}
        </td>

        {/* Tax % */}
        <td></td>

        {/* Tax Amount */}
        <td className="total-value">
            ₹ {summary.totalTax.toFixed(2)}
        </td>

        {/* Action */}
        <td></td>

    </tr>

    {/*==========================================
        Grand Total Row
    ==========================================*/}

    <tr className="grand-total-row">

        <td
            colSpan="10"
            className="grand-title"
        >
            GRAND TOTAL
        </td>

        <td
            colSpan="2"
            className="grand-amount"
        >
            ₹ {summary.grandTotal.toFixed(2)}
        </td>

    </tr>

</tfoot> 









          
            
           
         

      




           

        </table>
      </div>
    </div>
  );
}
