import {
    FaPlus,
    FaSave,
    FaEye,
    FaEdit,
    FaSyncAlt,
    FaTrash,
    FaPrint
} from "react-icons/fa";

import "./StockItemButtons.css";


export default function StockItemButtons({

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

    //==========================================================
    // RECORD SELECTED
    //==========================================================

    const hasSelectedRecord =
        selectedId !== null &&
        selectedId !== undefined &&
        selectedId !== "" &&
        Number(selectedId) > 0;


    //==========================================================
    // NEW
    //==========================================================

    const newDisabled =
        mode === "new" ||
        mode === "edit";


    //==========================================================
    // SAVE
    //==========================================================

    const saveDisabled =
        mode !== "new";


    //==========================================================
    // VIEW
    //
    // View is only for displaying a selected record.
    //==========================================================

    const viewDisabled =
        !hasSelectedRecord ||
        mode === "new" ||
        mode === "edit";


    //==========================================================
    // EDIT
    //
    // IMPORTANT:
    // Edit does NOT require View mode.
    //
    // A selected grid record can be edited directly.
    // Password will be requested by StockItemEntry.
    //==========================================================

    const editDisabled =
        !hasSelectedRecord ||
        mode === "new" ||
        mode === "edit";


    //==========================================================
    // UPDATE
    //==========================================================

    const updateDisabled =
        mode !== "edit";


    //==========================================================
    // DELETE
    //
    // IMPORTANT:
    // Delete does NOT require View mode.
    //==========================================================

    const deleteDisabled =
        !hasSelectedRecord ||
        mode === "new" ||
        mode === "edit";


    //==========================================================
    // PRINT
    //==========================================================

    const printDisabled =
        !hasSelectedRecord ||
        mode === "new";


    return (

        <div className="stock-buttons">


            {/*==================================================
                NEW
            ==================================================*/}

            <button

                type="button"

                className="btn new-btn"

                onClick={onNew}

                disabled={newDisabled}

            >

                <FaPlus />

                New

            </button>


            {/*==================================================
                SAVE
            ==================================================*/}

            <button

                type="button"

                className="btn save-btn"

                onClick={onSave}

                disabled={saveDisabled}

            >

                <FaSave />

                Save

            </button>


            {/*==================================================
                VIEW
            ==================================================*/}

            <button

                type="button"

                className="btn view-btn"

                onClick={onView}

                disabled={viewDisabled}

            >

                <FaEye />

                View

            </button>


            {/*==================================================
                EDIT
            ==================================================*/}

            <button

                type="button"

                className="btn edit-btn"

                onClick={onEdit}

                disabled={editDisabled}

            >

                <FaEdit />

                Edit

            </button>


            {/*==================================================
                UPDATE
            ==================================================*/}

            <button

                type="button"

                className="btn update-btn"

                onClick={onUpdate}

                disabled={updateDisabled}

            >

                <FaSyncAlt />

                Update

            </button>


            {/*==================================================
                DELETE
            ==================================================*/}

            <button

                type="button"

                className="btn delete-btn"

                onClick={onDelete}

                disabled={deleteDisabled}

            >

                <FaTrash />

                Delete

            </button>


            {/*==================================================
                PRINT
            ==================================================*/}

            <button

                type="button"

                className="btn print-btn"

                onClick={onPrint}

                disabled={printDisabled}

            >

                <FaPrint />

                Print

            </button>


        </div>

    );

}