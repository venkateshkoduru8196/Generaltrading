export default function ReportTable({ report }) {

    //-----------------------------------------
    // No Data
    //-----------------------------------------

    if (!report)
        return null;

    //-----------------------------------------
    // Gold
    //-----------------------------------------

    const goldRows =
        report.stockMovements.filter(
            x => x.metal === "Gold"
        );

    //-----------------------------------------
    // Silver
    //-----------------------------------------

    const silverRows =
        report.stockMovements.filter(
            x => x.metal === "Silver"
        );

    return (

        <div className="report-wrapper">

            {/*==========================================*/}
            {/* COMPANY HEADER                           */}
            {/*==========================================*/}

            <div className="company-card">

                <div>

                    <h2>

                        {report.companyName}

                    </h2>

                    <p>

                        {report.companyAddress}

                    </p>

                </div>

                <div className="generated-info">

                    <span>

                        Generated On

                    </span>

                    <strong>

                        {

                            new Date(

                                report.reportDateTime

                            ).toLocaleString()

                        }

                    </strong>

                </div>

            </div>

            {/*==========================================*/}
            {/* GOLD */}
            {/*==========================================*/}

            <ReportSection

                title="Gold Stock Movement"

                icon="🟡"

                tableClass="gold-theme"

                rows={goldRows}

            />

            {/*==========================================*/}
            {/* SILVER */}
            {/*==========================================*/}

            <ReportSection

                title="Silver Stock Movement"

                icon="⚪"

                tableClass="silver-theme"

                rows={silverRows}

            />

        </div>

    );

}

/*========================================================*/

function ReportSection({

    title,

    icon,

    tableClass,

    rows

}) {

    return (

        <div className="report-card">

            <div className={`section-header ${tableClass}`}>

                <span>

                    {icon}

                </span>

                {title}

            </div>

            <div className="table-container">

                <table className="report-table">

                    <thead>

                        <tr>

                            <th>Account</th>

                            <th>Opening</th>

                            <th>Move In</th>

                            <th>Move Out</th>

                            <th>Closing</th>

                        </tr>

                    </thead>

                    <tbody>

                        {

                            rows.map((row,index)=>(

                                <tr

                                    key={index}

                                    className={

                                        row.accountName==="Total"

                                        ?

                                        "total-row"

                                        :

                                        ""

                                    }

                                >

                                    <td>

                                        {row.accountName}

                                    </td>

                                    <td>

                                        {row.opening.toFixed(2)}

                                    </td>

                                    <td>

                                        {row.moveIn.toFixed(2)}

                                    </td>

                                    <td>

                                        {row.moveOut.toFixed(2)}

                                    </td>

                                    <td>

                                        {row.closing.toFixed(2)}

                                    </td>

                                </tr>

                            ))

                        }

                    </tbody>

                </table>

            </div>

        </div>

    );

}