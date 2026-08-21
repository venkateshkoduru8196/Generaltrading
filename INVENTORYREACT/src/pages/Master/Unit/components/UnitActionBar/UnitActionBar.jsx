import "./UnitActionBar.css";

import UnitButtons
    from "../UnitButtons/UnitButtons";

export default function UnitActionBar({

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

        <div className="unit-footer">

            <UnitButtons

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