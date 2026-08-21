import "./StockItemActionBar.css";

import StockItemButtons
    from "../StockItemButtons/StockItemButtons";

export default function StockItemActionBar({
    mode,
    selectedId,
    onNew,
    onSave,
    onView,
    onEdit,
    onUpdate,
    onDelete,
    onPrint
}) {

    return (
        <div className="stock-action-bar">

            <StockItemButtons
                mode={mode}
                selectedId={selectedId}
                onNew={onNew}
                onSave={onSave}
                onView={onView}
                onEdit={onEdit}
                onUpdate={onUpdate}
                onDelete={onDelete}
                onPrint={onPrint}
            />

        </div>
    );
}