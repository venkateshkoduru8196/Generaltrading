import "./UnitPagination.css";

export default function UnitPagination({

    totalRecords,

    currentPage,

    rowsPerPage,

    totalPages,

    onPageChange,

    onRowsPerPageChange

}) {

    //==========================================================
    // RECORD RANGE
    //==========================================================

    const startRecord =
        totalRecords === 0
            ? 0
            : (currentPage - 1) *
                rowsPerPage + 1;


    const endRecord =
        Math.min(
            currentPage * rowsPerPage,
            totalRecords
        );


    return (

        <div className="pagination-container">

            {/*==================================================
                LEFT
            ==================================================*/}

            <div className="pagination-info">

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
                CENTER
            ==================================================*/}

            <div className="pagination-buttons">

                <button

                    type="button"

                    disabled={
                        currentPage === 1
                    }

                    onClick={() =>
                        onPageChange(1)
                    }

                >
                    ⏮
                </button>


                <button

                    type="button"

                    disabled={
                        currentPage === 1
                    }

                    onClick={() =>
                        onPageChange(
                            currentPage - 1
                        )
                    }

                >
                    ◀
                </button>


                {

                    Array.from(

                        {
                            length: totalPages
                        },

                        (_, i) => i + 1

                    ).map(page => (

                        <button

                            type="button"

                            key={page}

                            className={
                                currentPage === page
                                    ? "active-page"
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


                <button

                    type="button"

                    disabled={
                        currentPage === totalPages ||
                        totalPages === 0
                    }

                    onClick={() =>
                        onPageChange(
                            currentPage + 1
                        )
                    }

                >
                    ▶
                </button>


                <button

                    type="button"

                    disabled={
                        currentPage === totalPages ||
                        totalPages === 0
                    }

                    onClick={() =>
                        onPageChange(
                            totalPages
                        )
                    }

                >
                    ⏭
                </button>

            </div>


            {/*==================================================
                RIGHT
            ==================================================*/}

            <div className="rows-dropdown">

                Rows

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