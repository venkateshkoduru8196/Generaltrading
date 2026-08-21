import { FaBoxes } from "react-icons/fa";

import "./StockItemHeader.css";

export default function StockItemHeader({
    form,
    setForm,
    mode
}) {

    //==========================================================
    // READ ONLY MODE
    //==========================================================

    const readOnly =
        mode !== "new" &&
        mode !== "edit";


    //==========================================================
    // HANDLE INPUT CHANGE
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


    //==========================================================
    // RENDER
    //==========================================================

    return (

        <div className="stock-header-card">

            {/*==================================================
                TITLE
            ==================================================*/}

            <div className="stock-header-title">

                <div className="stock-title-content">

                    <span className="stock-header-icon-wrapper">

                        <FaBoxes
                            className="stock-header-icon"
                        />

                    </span>

                    <h3>
                        STOCK ITEM MASTER
                    </h3>

                </div>

            </div>


            {/*==================================================
                BODY
            ==================================================*/}

            <div className="stock-header-body">


                {/*================================================
                    STOCK CODE
                =================================================*/}

                <div className="form-group">

                    <label>

                        Stock Code

                        <span>*</span>

                    </label>

                    <input
                        type="text"
                        name="stockCode"
                        value={form.stockCode}
                        onChange={handleChange}
                        disabled={readOnly}
                        maxLength={20}
                        placeholder="Stock Code"
                        autoComplete="off"
                    />

                </div>


                {/*================================================
                    STOCK NAME
                =================================================*/}

                <div className="form-group">

                    <label>

                        Stock Name

                        <span>*</span>

                    </label>

                    <input
                        type="text"
                        name="stockName"
                        value={form.stockName}
                        onChange={handleChange}
                        disabled={readOnly}
                        maxLength={150}
                        placeholder="Stock Name"
                        autoComplete="off"
                    />

                </div>


                {/*================================================
                    GST
                =================================================*/}

                <div className="form-group">

                    <label>
                        GST %
                    </label>

                    <input
                        type="number"
                        name="taxRate"
                        value={form.taxRate}
                        onChange={handleChange}
                        disabled={readOnly}
                        min="0"
                        step="0.01"
                    />

                </div>


                {/*================================================
                    ACTIVE
                =================================================*/}

                <div className="checkbox-group">

                    <label>

                        <input
                            type="checkbox"
                            name="isActive"
                            checked={form.isActive}
                            onChange={handleChange}
                            disabled={readOnly}
                        />

                        Active

                    </label>

                </div>


            </div>

        </div>

    );

}
























// import { FaBoxes } from "react-icons/fa";

// import "./StockItemHeader.css";

// export default function StockItemHeader({
//     form,
//     setForm,
//     mode
// }) {

//     //==========================================
//     // Read Only Mode
//     //==========================================

//     const readOnly =
//         mode !== "new" &&
//         mode !== "edit";


//     //==========================================
//     // Handle Input Change
//     //==========================================

//     const handleChange = ({ target }) => {

//         const {
//             name,
//             value,
//             type,
//             checked
//         } = target;

//         setForm(prev => ({

//             ...prev,

//             [name]:
//                 type === "checkbox"
//                     ? checked
//                     : value

//         }));

//     };


//     return (

//         <div className="stock-header-card">

//             {/*==========================================
//                 Title
//             ==========================================*/}

//             <div className="stock-header-title">

//                 <FaBoxes
//                     className="stock-header-icon"
//                 />

//                 <h3>
//                     STOCK ITEM MASTER
//                 </h3>

//             </div>


//             {/*==========================================
//                 Body
//             ==========================================*/}

//             <div className="stock-header-body">

//                 {/* Stock Code */}

//                 <div className="form-group">

//                     <label>

//                         Stock Code

//                         <span>*</span>

//                     </label>

//                     <input
//                         type="text"
//                         name="stockCode"
//                         value={form.stockCode}
//                         onChange={handleChange}
//                         disabled={readOnly}
//                         maxLength={20}
//                         placeholder="Stock Code"
//                         autoComplete="off"
//                     />

//                 </div>


//                 {/* Stock Name */}

//                 <div className="form-group">

//                     <label>

//                         Stock Name

//                         <span>*</span>

//                     </label>

//                     <input
//                         type="text"
//                         name="stockName"
//                         value={form.stockName}
//                         onChange={handleChange}
//                         disabled={readOnly}
//                         maxLength={150}
//                         placeholder="Stock Name"
//                         autoComplete="off"
//                     />

//                 </div>


//                 {/* GST */}

//                 <div className="form-group">

//                     <label>

//                         GST %

//                     </label>

//                     <input
//                         type="number"
//                         name="taxRate"
//                         value={form.taxRate}
//                         onChange={handleChange}
//                         disabled={readOnly}
//                         min="0"
//                         step="0.01"
//                     />

//                 </div>


//                 {/* Active */}

//                 <div className="checkbox-group">

//                     <label>

//                         <input
//                             type="checkbox"
//                             name="isActive"
//                             checked={form.isActive}
//                             onChange={handleChange}
//                             disabled={readOnly}
//                         />

//                         Active

//                     </label>

//                 </div>

//             </div>

//         </div>

//     );

// }





















// import "./StockItemHeader.css";

// export default function StockItemHeader({
//     form,
//     setForm,
//     mode
// }) {

//     //==========================================
//     // Read Only Mode
//     //==========================================

//     // const readOnly =
//     //     mode === "initial" ||
//     //     mode === "saved";


//     const readOnly =
//     mode !== "new" &&
//     mode !== "edit";

//     //==========================================
//     // Handle Input Change
//     //==========================================

//     const handleChange = ({ target }) => {

//         const { name, value, type, checked } = target;

//         setForm(prev => ({

//             ...prev,

//             [name]:
//                 type === "checkbox"
//                     ? checked
//                     : value

//         }));

//     };

//     return (

//         <div className="stock-header-card">

//             {/*==========================================
//                 Title
//             ==========================================*/}

//             <div className="stock-header-title">

//                 <h3>STOCK ITEM MASTER</h3>

//             </div>

//             {/*==========================================
//                 Body
//             ==========================================*/}

//             <div className="stock-header-body">

//                 {/* Stock Code */}

//                 <div className="form-group">

//                     <label>

//                         Stock Code

//                         <span>*</span>

//                     </label>

//                     <input

//                         type="text"

//                         name="stockCode"

//                         value={form.stockCode}

//                         onChange={handleChange}

//                         disabled={readOnly}

//                         maxLength={20}

//                         placeholder="Stock Code"

//                         autoComplete="off"

//                     />

//                 </div>

//                 {/* Stock Name */}

//                 <div className="form-group">

//                     <label>

//                         Stock Name

//                         <span>*</span>

//                     </label>

//                     <input

//                         type="text"

//                         name="stockName"

//                         value={form.stockName}

//                         onChange={handleChange}

//                         disabled={readOnly}

//                         maxLength={150}

//                         placeholder="Stock Name"

//                         autoComplete="off"

//                     />

//                 </div>

//                 {/* GST */}

//                 <div className="form-group">

//                     <label>

//                         GST %

//                     </label>

//                     <input

//                         type="number"

//                         name="taxRate"

//                         value={form.taxRate}

//                         onChange={handleChange}

//                         disabled={readOnly}

//                         min="0"

//                         step="0.01"

//                     />

//                 </div>

//                 {/* Active */}

//                 <div className="checkbox-group">

//                     <label>

//                         <input

//                             type="checkbox"

//                             name="isActive"

//                             checked={form.isActive}

//                             onChange={handleChange}

//                             disabled={readOnly}

//                         />

//                         Active

//                     </label>

//                 </div>

//             </div>

//         </div>

//     );

// }