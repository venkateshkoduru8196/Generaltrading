import "./AccountPagination.css";

export default function AccountPagination({

    totalRecords,

    currentPage,

    rowsPerPage,

    totalPages,

    onPageChange,

    onRowsPerPageChange

}) {

    //==========================================================
    // Start Record
    //==========================================================

    const startRecord =
        totalRecords === 0
            ? 0
            : (currentPage - 1) *
              rowsPerPage +
              1;


    //==========================================================
    // End Record
    //==========================================================

    const endRecord =
        Math.min(
            currentPage * rowsPerPage,
            totalRecords
        );


    return (

        <div className="account-pagination-container">

            {/*==================================================
                Left
            ==================================================*/}

            <div className="account-pagination-info">

                Showing

                <strong>

                    {" "}
                    {startRecord}-{endRecord}

                </strong>

                {" "}of{" "}

                <strong>

                    {totalRecords}

                </strong>

            </div>


            {/*==================================================
                Center
            ==================================================*/}

            <div className="account-pagination-buttons">

                {/* First */}

                <button

                    disabled={
                        currentPage === 1
                    }

                    onClick={() =>
                        onPageChange(1)
                    }

                    title="First Page"

                >

                    ⏮

                </button>


                {/* Previous */}

                <button

                    disabled={
                        currentPage === 1
                    }

                    onClick={() =>
                        onPageChange(
                            currentPage - 1
                        )
                    }

                    title="Previous Page"

                >

                    ◀

                </button>


                {/* Page Numbers */}

                {

                    Array.from(

                        {
                            length:
                                totalPages
                        },

                        (_, i) =>
                            i + 1

                    ).map(page => (

                        <button

                            key={page}

                            className={

                                currentPage === page

                                    ? "account-active-page"

                                    : ""

                            }

                            onClick={() =>
                                onPageChange(page)
                            }

                        >

                            {page}

                        </button>

                    ))

                }


                {/* Next */}

                <button

                    disabled={

                        currentPage === totalPages ||

                        totalPages === 0

                    }

                    onClick={() =>
                        onPageChange(
                            currentPage + 1
                        )
                    }

                    title="Next Page"

                >

                    ▶

                </button>


                {/* Last */}

                <button

                    disabled={

                        currentPage === totalPages ||

                        totalPages === 0

                    }

                    onClick={() =>
                        onPageChange(
                            totalPages
                        )
                    }

                    title="Last Page"

                >

                    ⏭

                </button>

            </div>


            {/*==================================================
                Right
            ==================================================*/}

            <div className="account-rows-dropdown">

                <span>
                    Rows
                </span>

                <select

                    value={rowsPerPage}

                    onChange={(e) =>
                        onRowsPerPageChange(
                            Number(
                                e.target.value
                            )
                        )
                    }

                >

                    <option value={5}>
                        5
                    </option>

                    <option value={10}>
                        10
                    </option>

                    <option value={20}>
                        20
                    </option>

                    <option value={50}>
                        50
                    </option>

                    <option value={100}>
                        100
                    </option>

                </select>

            </div>

        </div>

    );

}