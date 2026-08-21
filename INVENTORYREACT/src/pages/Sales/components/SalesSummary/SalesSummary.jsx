import "./SalesSummary.css";

export default function SalesSummary({ summary }) {

    return (

        <div className="sales-summary">

            <div className="summary-card">

                <div className="summary-item">

                    <label>Total Qty</label>

                    <input
                        type="number"
                        value={summary.totalQty}
                        readOnly
                    />

                </div>

                <div className="summary-item">

                    <label>Total Amount</label>

                    <input
                        type="number"
                        value={summary.totalAmount.toFixed(2)}
                        readOnly
                    />

                </div>

                <div className="summary-item">

                    <label>Total Tax</label>

                    <input
                        type="number"
                        value={summary.totalTax.toFixed(2)}
                        readOnly
                    />

                </div>

                <div className="summary-item grand-total">

                    <label>Grand Total</label>

                    <input
                        type="number"
                        value={summary.grandTotal.toFixed(2)}
                        readOnly
                    />

                </div>

            </div>

        </div>

    );

}