import "./ReceiptButtons.css";

export default function ReceiptButtons({
  mode,
  onNew,
  onSave,
  onEdit,
  onUpdate,
  onDelete,
  onPrint,
  loading,
}) {
  return (
    <div className="receipt-buttons">
      <button
        type="button"
        className="receipt-btn receipt-btn-new"
        onClick={onNew}
        disabled={loading || mode === "new" || mode === "edit"}
      >
        New
      </button>

      <button
        type="button"
        className="receipt-btn receipt-btn-save"
        onClick={onSave}
        disabled={loading || mode !== "new"}
      >
        Save
      </button>

      <button
        type="button"
        className="receipt-btn receipt-btn-edit"
        onClick={onEdit}
        disabled={loading || mode !== "saved"}
      >
        Edit
      </button>

      <button
        type="button"
        className="receipt-btn receipt-btn-update"
        onClick={onUpdate}
        disabled={loading || mode !== "edit"}
      >
        Update
      </button>

      <button
        type="button"
        className="receipt-btn receipt-btn-delete"
        onClick={onDelete}
        disabled={loading || mode !== "saved"}
      >
        Delete
      </button>

      <button
        type="button"
        className="receipt-btn receipt-btn-print"
        onClick={onPrint}
        disabled={loading || mode !== "saved"}
      >
        Print
      </button>
    </div>
  );
}
