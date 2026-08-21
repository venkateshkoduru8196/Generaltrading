import { FaRulerCombined } from "react-icons/fa";

import "./UnitHeader.css";

export default function UnitHeader({
    form,
    setForm,
    mode
}) {

    //==========================================================
    // READ ONLY
    //==========================================================

    const readOnly =
        mode !== "new" &&
        mode !== "edit";


    //==========================================================
    // HANDLE CHANGE
    //==========================================================

    const handleChange = ({ target }) => {

        const {
            name,
            value
        } = target;

        setForm(prev => ({
            ...prev,
            [name]: value
        }));

    };


    //==========================================================
    // RENDER
    //==========================================================

    return (

        <div className="unit-header-card">

            {/*==================================================
                TITLE
            ==================================================*/}

            <div className="unit-header-title">

                <div className="unit-title-content">

                    <span className="unit-header-icon-wrapper">

                        <FaRulerCombined
                            className="unit-header-icon"
                        />

                    </span>

                    <h3>
                        UNIT MASTER
                    </h3>

                </div>

            </div>


            {/*==================================================
                BODY
            ==================================================*/}

            <div className="unit-header-body">

                {/*================================================
                    Unit Code
                =================================================*/}

                <div className="form-group">

                    <label>

                        Unit Code

                        <span>*</span>

                    </label>

                    <input
                        type="text"
                        name="code"
                        value={form.code}
                        onChange={handleChange}
                        disabled={readOnly}
                        maxLength={50}
                        placeholder="Unit Code"
                        autoComplete="off"
                    />

                </div>


                {/*================================================
                    Description
                =================================================*/}

                <div className="form-group">

                    <label>

                        Description

                        <span>*</span>

                    </label>

                    <input
                        type="text"
                        name="description"
                        value={form.description}
                        onChange={handleChange}
                        disabled={readOnly}
                        maxLength={200}
                        placeholder="Unit Description"
                        autoComplete="off"
                    />

                </div>

            </div>

        </div>

    );
}