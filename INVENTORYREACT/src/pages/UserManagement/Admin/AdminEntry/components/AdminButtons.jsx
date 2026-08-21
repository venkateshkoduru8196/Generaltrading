import { useState } from "react";

import "./AdminButtons.css";

export default function AdminButtons({
    user,
    onEdit,
    onStatusChange,
    onDelete
}) {
    const [open, setOpen] = useState(false);

    //====================================================
    // USER ID
    // Backend property is: userId
    //====================================================

    const userId = user?.userId;

    //====================================================
    // STATUS
    //====================================================

    const handleStatus = () => {

        if (!userId) {
            console.error(
                "Cannot change administrator status. UserId is missing:",
                user
            );
            return;
        }

        setOpen(false);

        onStatusChange(
            userId,
            !user.isActive
        );
    };

    //====================================================
    // DELETE
    //====================================================

    const handleDelete = () => {

        if (!userId) {
            console.error(
                "Cannot delete administrator. UserId is missing:",
                user
            );
            return;
        }

        setOpen(false);

        onDelete(userId);
    };

    //====================================================
    // EDIT
    //====================================================

    const handleEdit = () => {

        if (!userId) {
            console.error(
                "Cannot edit administrator. UserId is missing:",
                user
            );
            return;
        }

        setOpen(false);

        onEdit(userId);
    };

    //====================================================
    // UI
    //====================================================

    return (
        <div className="admin-row-actions">

            <button
                type="button"
                className="admin-menu-trigger"
                onClick={() =>
                    setOpen(previous => !previous)
                }
                aria-label="Administrator actions"
            >
                ⋮
            </button>

            {open && (

                <div className="admin-action-menu">

                    {/* EDIT */}

                    <button
                        type="button"
                        onClick={handleEdit}
                    >
                        Edit
                    </button>


                    {/* STATUS */}

                    <button
                        type="button"
                        onClick={handleStatus}
                    >
                        {user.isActive
                            ? "Deactivate"
                            : "Activate"}
                    </button>


                    {/* DELETE */}

                    <button
                        type="button"
                        className="danger"
                        onClick={handleDelete}
                    >
                        Delete
                    </button>

                </div>

            )}

        </div>
    );
}