import "./AccountHeader.css";

export default function AccountHeader({
    form,
    setForm,
    mode
}) {

    //==========================================================
    // Read Only Mode
    //==========================================================

    const readOnly =
        mode !== "new" &&
        mode !== "edit";


    //==========================================================
    // Handle Input Change
    //==========================================================

    const handleChange = ({ target }) => {

        const {
            name,
            value,
            type,
            checked
        } = target;


        setForm(prev => ({

            ...prev,

            [name]:
                type === "checkbox"
                    ? checked
                    : value

        }));

    };


    return (

        <div className="account-header-card">

            {/*==================================================
                Title
            ==================================================*/}

            <div className="account-header-title">

                <h3>
                    ACCOUNT MASTER
                </h3>

            </div>


            {/*==================================================
                Body
            ==================================================*/}

            <div className="account-header-body">

                {/*================================================
                    Account Code
                =================================================*/}

                <div className="account-form-group">

                    <label>

                        Account Code

                        <span>*</span>

                    </label>

                    <input

                        type="text"

                        name="accountCode"

                        value={
                            form.accountCode
                        }

                        onChange={
                            handleChange
                        }

                        disabled={
                            readOnly
                        }

                        maxLength={20}

                        placeholder="Account Code"

                        autoComplete="off"

                    />

                </div>


                {/*================================================
                    Account Name
                =================================================*/}

                <div className="account-form-group">

                    <label>

                        Account Name

                        <span>*</span>

                    </label>

                    <input

                        type="text"

                        name="accountName"

                        value={
                            form.accountName
                        }

                        onChange={
                            handleChange
                        }

                        disabled={
                            readOnly
                        }

                        maxLength={150}

                        placeholder="Account Name"

                        autoComplete="off"

                    />

                </div>


                {/*================================================
                    Account Type
                =================================================*/}

                <div className="account-form-group">

                    <label>

                        Account Type

                        <span>*</span>

                    </label>

                    <select

                        name="actype"
                        className="account-type-select"

                        value={
                            form.actype
                        }

                        onChange={
                            handleChange
                        }

                        disabled={
                            readOnly
                        }

                    >

                        <option value="G">
                            General
                        </option>

                        <option value="B">
                            Bank/Cash
                        </option>

                        <option value="C">
                            Customer
                        </option>

                        <option value="S">
                            Supplier
                        </option>

                    </select>

                </div>


                {/*================================================
                    Active
                =================================================*/}

                <div className="account-checkbox-group">

                    <label>

                        <input

                            type="checkbox"

                            name="isActive"

                            checked={
                                form.isActive
                            }

                            onChange={
                                handleChange
                            }

                            disabled={
                                readOnly
                            }

                        />

                        Active

                    </label>

                </div>

            </div>

        </div>

    );

}