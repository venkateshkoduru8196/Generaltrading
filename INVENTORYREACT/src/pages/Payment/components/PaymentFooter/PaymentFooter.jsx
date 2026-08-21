import "./PaymentFooter.css";
import PaymentButtons from "../PaymentButtons/PaymentButtons";

export default function PaymentFooter({
  searchDocNo,
  setSearchDocNo,
  onSearch,
  mode,
  onNew,
  onSave,
  onEdit,
  onUpdate,
  onDelete,
  onPrint,
  loading,
}) {
  const searchDisabled = loading || mode === "new" || mode === "edit";

  const handleKeyDown = (event) => {
    if (event.key === "Enter" && !searchDisabled) {
      onSearch();
    }
  };

  return (
    <div className="payment-footer">
      <div className="payment-search">
        <label>Search Payment</label>

        <div className="payment-search-box">
          <input
            type="text"
            value={searchDocNo}
            onChange={(e) => setSearchDocNo(e.target.value)}
            onKeyDown={handleKeyDown}
            placeholder="Enter Payment No"
            disabled={searchDisabled}
          />

          <button
            type="button"
            onClick={onSearch}
            disabled={searchDisabled}
            title="Search Payment"
          >
            🔍
          </button>
        </div>
      </div>

      <div className="payment-footer-buttons">
        <PaymentButtons
          mode={mode}
          onNew={onNew}
          onSave={onSave}
          onEdit={onEdit}
          onUpdate={onUpdate}
          onDelete={onDelete}
          onPrint={onPrint}
          loading={loading}
        />
      </div>
    </div>
  );
}
