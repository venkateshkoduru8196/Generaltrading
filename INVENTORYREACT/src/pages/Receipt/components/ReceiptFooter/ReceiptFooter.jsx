import "./ReceiptFooter.css";
import ReceiptButtons from "../ReceiptButtons/ReceiptButtons";

export default function ReceiptFooter({
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
    <div className="receipt-footer">
      <div className="receipt-search">
        <label>Search Receipt</label>

        <div className="receipt-search-box">
          <input
            type="text"
            value={searchDocNo}
            onChange={(e) => setSearchDocNo(e.target.value)}
            onKeyDown={handleKeyDown}
            placeholder="Enter Receipt No"
            disabled={searchDisabled}
          />

          <button
            type="button"
            onClick={onSearch}
            disabled={searchDisabled}
            title="Search Receipt"
          >
            🔍
          </button>
        </div>
      </div>

      <div className="receipt-footer-buttons">
        <ReceiptButtons
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
