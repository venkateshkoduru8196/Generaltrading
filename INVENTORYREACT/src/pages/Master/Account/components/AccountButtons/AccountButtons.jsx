import {
    FaPlus,
    FaSave,
    FaEye,
    FaEdit,
    FaSyncAlt,
    FaTrash,
    FaPrint
} from "react-icons/fa";

import "./AccountButtons.css";

export default function AccountButtons({

    mode,

    onNew,

    onSave,

    onView,

    onEdit,

    onUpdate,

    onDelete,

    onPrint

}) {

    return (

        <div className="account-buttons">

            {/*==================================================
                New
            ==================================================*/}

            <button

                className="account-btn account-new-btn"

                onClick={onNew}

                disabled={

                    mode === "new" ||

                    mode === "edit"

                }

            >

                <FaPlus />

                New

            </button>


            {/*==================================================
                Save
            ==================================================*/}

            <button

                className="account-btn account-save-btn"

                onClick={onSave}

                disabled={

                    mode !== "new"

                }

            >

                <FaSave />

                Save

            </button>


            {/*==================================================
                View
            ==================================================*/}

            <button

                className="account-btn account-view-btn"

                onClick={onView}

                disabled={

                    mode === "new" ||

                    mode === "edit"

                }

            >

                <FaEye />

                View

            </button>


            {/*==================================================
                Edit
            ==================================================*/}

            <button

                className="account-btn account-edit-btn"

                onClick={onEdit}

                disabled={

                    mode !== "view"

                }

            >

                <FaEdit />

                Edit

            </button>


            {/*==================================================
                Update
            ==================================================*/}

            <button

                className="account-btn account-update-btn"

                onClick={onUpdate}

                disabled={

                    mode !== "edit"

                }

            >

                <FaSyncAlt />

                Update

            </button>


            {/*==================================================
                Delete
            ==================================================*/}

            <button

                className="account-btn account-delete-btn"

                onClick={onDelete}

                disabled={

                    mode !== "view"

                }

            >

                <FaTrash />

                Delete

            </button>


            {/*==================================================
                Print
            ==================================================*/}

            <button

                className="account-btn account-print-btn"

                onClick={onPrint}

                disabled={

                    mode === "initial" ||

                    mode === "new"

                }

            >

                <FaPrint />

                Print

            </button>

        </div>

    );

}