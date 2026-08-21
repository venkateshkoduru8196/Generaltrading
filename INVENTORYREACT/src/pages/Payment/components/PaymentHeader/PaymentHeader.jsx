import "./PaymentHeader.css";

export default function PaymentHeader({
  header,
  setHeader,
  parties,
  readOnly,
  loading,
}) {
  const handleChange = ({ target }) => {
    setHeader((prev) => ({
      ...prev,
      [target.name]: target.value,
    }));
  };

  return (
    <div className="payment-header">
      <div className="payment-form-group">
        <label>Payment No</label>
        <input
          type="text"
          name="docNo"
          value={header.docNo}
          readOnly
          placeholder="Auto Generated"
        />
      </div>

      <div className="payment-form-group">
        <label>Payment Date</label>
        <input
          type="date"
          name="docDate"
          value={header.docDate}
          disabled={readOnly}
          onChange={handleChange}
        />
      </div>

      <div className="payment-form-group  payment-party-group">
        <label>Party</label>
        <select
          name="partyId"
          value={header.partyId}
          disabled={readOnly || loading}
          onChange={handleChange}
        >
          <option value="">
            {loading ? "Loading Parties..." : "Select Party"}
          </option>

          {parties.map((party) => (
            <option key={party.id} value={party.id}>
              {party.partyCode} - {party.partyName}
            </option>
          ))}
        </select>
      </div>
    </div>
  );
}
