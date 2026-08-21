import "./ReceiptHeader.css";

export default function ReceiptHeader({
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
    <div className="receipt-header">
      <div className="receipt-form-group">
        <label>Receipt No</label>
        <input
          type="text"
          name="docNo"
          value={header.docNo}
          readOnly
          placeholder="Auto Generated"
        />
      </div>

      <div className="receipt-form-group">
        <label>Receipt Date</label>
        <input
          type="date"
          name="docDate"
          value={header.docDate}
          disabled={readOnly}
          onChange={handleChange}
        />
      </div>

      <div className="receipt-form-group receipt-party-group">
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
