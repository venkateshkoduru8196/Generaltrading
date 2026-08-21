// export default function ReportFilter({

//     reportType,
//     setReportType,

//     reportDate,
//     setReportDate,

//     month,
//     setMonth,

//     year,
//     setYear,

//     fromDate,
//     setFromDate,

//     toDate,
//     setToDate

// }) {

//     return (

//         <div className="report-filter">

//             {/* Report Type */}

//             <div className="filter-group">

//                 <label>

//                     Report Type

//                 </label>

//                 <select

//                     value={reportType}

//                     onChange={(e)=>

//                         setReportType(e.target.value)

//                     }

//                 >

//                     <option value="Daily">

//                         Daily

//                     </option>

//                     <option value="Monthly">

//                         Monthly

//                     </option>

//                     <option value="Periodical">

//                         Periodical

//                     </option>

//                 </select>

//             </div>

//             {/* Daily */}

//             {

//                 reportType==="Daily" &&

//                 <div className="filter-group">

//                     <label>

//                         Report Date

//                     </label>

//                     <input

//                         type="date"

//                         value={reportDate}

//                         onChange={(e)=>

//                             setReportDate(e.target.value)

//                         }

//                     />

//                 </div>

//             }

//             {/* Monthly */}

//             {

//                 reportType==="Monthly" &&

//                 <>

//                     <div className="filter-group">

//                         <label>

//                             Month

//                         </label>

//                         <select

//                             value={month}

//                             onChange={(e)=>

//                                 setMonth(e.target.value)

//                             }

//                         >

//                             {

//                                 Array.from(

//                                     {length:12},

//                                     (_,i)=>

//                                     <option

//                                         key={i+1}

//                                         value={i+1}

//                                     >

//                                         {

//                                             new Date(

//                                                 2024,

//                                                 i

//                                             ).toLocaleString(

//                                                 "default",

//                                                 {

//                                                     month:"long"

//                                                 }

//                                             )

//                                         }

//                                     </option>

//                                 )

//                             }

//                         </select>

//                     </div>

//                     <div className="filter-group">

//                         <label>

//                             Year

//                         </label>

//                         <select

//                             value={year}

//                             onChange={(e)=>

//                                 setYear(e.target.value)

//                             }

//                         >

//                             {

//                                 Array.from(

//                                     {length:10},

//                                     (_,i)=>{

//                                         const y=2022+i;

//                                         return(

//                                             <option

//                                                 key={y}

//                                                 value={y}

//                                             >

//                                                 {y}

//                                             </option>

//                                         );

//                                     }

//                                 )

//                             }

//                         </select>

//                     </div>

//                 </>

//             }

//             {/* Periodical */}

//             {

//                 reportType==="Periodical" &&

//                 <>

//                     <div className="filter-group">

//                         <label>

//                             From Date

//                         </label>

//                         <input

//                             type="date"

//                             value={fromDate}

//                             onChange={(e)=>

//                                 setFromDate(e.target.value)

//                             }

//                         />

//                     </div>

//                     <div className="filter-group">

//                         <label>

//                             To Date

//                         </label>

//                         <input

//                             type="date"

//                             value={toDate}

//                             onChange={(e)=>

//                                 setToDate(e.target.value)

//                             }

//                         />

//                     </div>

//                 </>

//             }

//         </div>

//     );

// }





export default function ReportFilter({

    reportType,
    setReportType,

    reportDate,
    setReportDate,

    month,
    setMonth,

    year,
    setYear,

    fromDate,
    setFromDate,

    toDate,
    setToDate

}) {

    return (

        <div className="report-filter">

            {/*========================================*/}
            {/* Report Type */}
            {/*========================================*/}

            <div className="filter-group">

                <label>

                    Report Type

                </label>

                <select

                    value={reportType}

                    onChange={(e) =>
                        setReportType(e.target.value)
                    }

                >

                    <option value="Daily">

                        Daily

                    </option>

                    <option value="Monthly">

                        Monthly

                    </option>

                    <option value="Periodical">

                        Periodical

                    </option>

                </select>

            </div>

            {/*========================================*/}
            {/* Daily */}
            {/*========================================*/}

            {

                reportType === "Daily" &&

                <div className="filter-group">

                    <label>

                        Report Date

                    </label>

                    <input

                        type="date"

                        value={reportDate}

                        onChange={(e) =>
                            setReportDate(e.target.value)
                        }

                    />

                </div>

            }

            {/*========================================*/}
            {/* Monthly */}
            {/*========================================*/}

            {

                reportType === "Monthly" &&

                <>

                    <div className="filter-group">

                        <label>

                            Month

                        </label>

                        <select

                            value={month}

                            onChange={(e) =>
                                setMonth(Number(e.target.value))
                            }

                        >

                            {

                                Array.from(

                                    { length: 12 },

                                    (_, i) => (

                                        <option

                                            key={i + 1}

                                            value={i + 1}

                                        >

                                            {

                                                new Date(

                                                    2024,

                                                    i

                                                ).toLocaleString(

                                                    "default",

                                                    {

                                                        month: "long"

                                                    }

                                                )

                                            }

                                        </option>

                                    )

                                )

                            }

                        </select>

                    </div>

                    {/*==============================*/}
                    {/* Year */}
                    {/*==============================*/}

                    <div className="filter-group">

                        <label>

                            Year

                        </label>

                        <input

                            type="number"

                            min="1900"

                            max="2100"

                            step="1"

                            value={year}

                            placeholder="Enter Year"

                            onChange={(e) =>
                                setYear(Number(e.target.value))
                            }

                        />

                    </div>

                </>

            }

            {/*========================================*/}
            {/* Periodical */}
            {/*========================================*/}

            {

                reportType === "Periodical" &&

                <>

                    <div className="filter-group">

                        <label>

                            From Date

                        </label>

                        <input

                            type="date"

                            value={fromDate}

                            onChange={(e) =>
                                setFromDate(e.target.value)
                            }

                        />

                    </div>

                    <div className="filter-group">

                        <label>

                            To Date

                        </label>

                        <input

                            type="date"

                            value={toDate}

                            onChange={(e) =>
                                setToDate(e.target.value)
                            }

                        />

                    </div>

                </>

            }

        </div>

    );

}