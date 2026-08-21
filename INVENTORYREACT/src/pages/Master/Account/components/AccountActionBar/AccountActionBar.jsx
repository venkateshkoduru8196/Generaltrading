import "./AccountActionBar.css";

import AccountButtons
    from "../AccountButtons/AccountButtons";

export default function AccountActionBar({

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

        <div className="account-action-bar">

            <AccountButtons

                mode={mode}

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