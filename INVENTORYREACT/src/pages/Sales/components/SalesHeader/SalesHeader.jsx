




// import "./SalesHeader.css";

// export default function SalesHeader({
//     header,
//     setHeader,
//     accounts,
//     searchDocNo,
//     setSearchDocNo,
//     onSearch,
//     readOnly
// }) {
//   const handleChange = ({ target }) => {
//     setHeader((prev) => ({
//       ...prev,
//       [target.name]: target.value,
//     }));
//   };

//   return (
//     <div className="sales-header">

//       {/* Search Invoice */}
//       <div className="form-group">
//         <label>Search Invoice</label>

//         <div className="invoice-search-box">
//           <input
//             type="text"
//             value={searchDocNo}
//             onChange={(e) => setSearchDocNo(e.target.value)}
//             placeholder="Enter Invoice No"
//           />

//           <button
//             type="button"
//             className="invoice-search-btn"
//             onClick={onSearch}
//             title="Search Invoice"
//           >
//             🔍
//           </button>
//         </div>
//       </div>

//       {/* Invoice Date */}
//       <div className="form-group">
//         <label>Invoice Date</label>

//         <input
//           type="date"
//           name="docDate"
//           value={header.docDate}
//           disabled={readOnly}
//           onChange={handleChange}
//         />
//       </div>

//       {/* Party */}
//       <div className="form-group party-group">
//         <label>Party</label>

//         <select
//           name="partyCode"
//           value={header.partyCode}
//           disabled={readOnly}
//           onChange={handleChange}
//         >
//           <option value="">
//             {accounts.length === 0
//               ? "Loading Parties..."
//               : "Select Party"}
//           </option>

//           {accounts.map((account) => (
//             <option
//               key={account.id}
//               value={account.accountCode}
//             >
//               {account.accountCode} - {account.accountName}
//             </option>
//           ))}
//         </select>
//       </div>

//     </div>
//   );
// }

import "./SalesHeader.css";

export default function SalesHeader({
    header,
    setHeader,
    accounts,
    readOnly
}) {

    const handleChange = ({ target }) => {

        setHeader(prev => ({
            ...prev,
            [target.name]: target.value
        }));

    };

    return (

        <div className="sales-header">

            {/* Invoice No */}

            <div className="form-group">

                <label>Invoice No</label>

                <input
                    type="text"
                    name="docNo"
                    value={header.docNo}
                    readOnly
                    placeholder="Auto Generated"
                />

            </div>

            {/* Invoice Date */}

            <div className="form-group">

                <label>Invoice Date</label>

                <input
                    type="date"
                    name="docDate"
                    value={header.docDate}
                    disabled={readOnly}
                    onChange={handleChange}
                />

            </div>

            {/* Party */}

            <div className="form-group party-group">

                <label>Party</label>

                <select
                    name="partyCode"
                    value={header.partyCode}
                    disabled={readOnly}
                    onChange={handleChange}
                >

                    <option value="">
                        {accounts.length === 0
                            ? "Loading Parties..."
                            : "Select Party"}
                    </option>

                    {accounts.map(account => (

                        <option
                            key={account.id}
                            value={account.accountCode}
                        >

                            {account.accountCode} - {account.accountName}

                        </option>

                    ))}

                </select>

            </div>

        </div>

    );

}



