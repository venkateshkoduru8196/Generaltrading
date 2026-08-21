import "./PaymentButtons.css";

export default function PaymentButtons({
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
    <div className="payment-buttons">
      <button
        type="button"
        className="payment-btn  payment-btn-new"
        onClick={onNew}
        disabled={loading || mode === "new" || mode === "edit"}
      >
        New
      </button>

      <button
        type="button"
        className="payment-btn  payment-btn-save"
        onClick={onSave}
        disabled={loading || mode !== "new"}
      >
        Save
      </button>

      <button
        type="button"
        className="payment-btn  payment-btn-edit"
        onClick={onEdit}
        disabled={loading || mode !== "saved"}
      >
        Edit
      </button>

      <button
        type="button"
        className="payment-btn  payment-btn-update"
        onClick={onUpdate}
        disabled={loading || mode !== "edit"}
      >
        Update
      </button>

      <button
        type="button"
        className="payment-btn  payment-btn-delete"
        onClick={onDelete}
        disabled={loading || mode !== "saved"}
      >
        Delete
      </button>

      <button
        type="button"
        className="payment-btn  payment-btn-print"
        onClick={onPrint}
        disabled={loading || mode !== "saved"}
      >
        Print
      </button>
    </div>
  );
}
