import "./AdminPagination.css";

export default function AdminPagination({
    pageNumber,
    pageSize,
    totalRecords,
    totalPages,
    onPageChange,
    onPageSizeChange
}) {

    if (totalRecords === 0)
        return null;


    const startRecord =
        ((pageNumber - 1) * pageSize) + 1;

    const endRecord =
        Math.min(
            pageNumber * pageSize,
            totalRecords
        );


    return (

        <div className="admin-pagination">

            <div className="admin-pagination-info">

                Showing{" "}

                <strong>
                    {startRecord}
                </strong>

                {" – "}

                <strong>
                    {endRecord}
                </strong>

                {" of "}

                <strong>
                    {totalRecords}
                </strong>

            </div>


            <div className="admin-pagination-controls">

                <select
                    value={pageSize}
                    onChange={(e) =>
                        onPageSizeChange(
                            Number(e.target.value)
                        )
                    }
                    className="admin-page-size"
                >

                    <option value="10">
                        10 / page
                    </option>

                    <option value="20">
                        20 / page
                    </option>

                    <option value="50">
                        50 / page
                    </option>

                    <option value="100">
                        100 / page
                    </option>

                </select>


                <button
                    type="button"
                    disabled={pageNumber <= 1}
                    onClick={() =>
                        onPageChange(
                            pageNumber - 1
                        )
                    }
                >
                    ‹
                </button>


                {getPages(
                    pageNumber,
                    totalPages
                ).map((page, index) => (

                    page === "..." ? (

                        <span
                            key={`ellipsis-${index}`}
                            className="admin-page-ellipsis"
                        >
                            ...
                        </span>

                    ) : (

                        <button
                            key={page}
                            type="button"
                            className={
                                page === pageNumber
                                    ? "active"
                                    : ""
                            }
                            onClick={() =>
                                onPageChange(page)
                            }
                        >
                            {page}
                        </button>

                    )

                ))}


                <button
                    type="button"
                    disabled={
                        pageNumber >= totalPages
                    }
                    onClick={() =>
                        onPageChange(
                            pageNumber + 1
                        )
                    }
                >
                    ›
                </button>

            </div>

        </div>

    );
}


//======================================================
// PAGE GENERATOR
//======================================================

function getPages(
    current,
    total
) {

    if (total <= 5) {

        return Array.from(
            { length: total },
            (_, i) => i + 1
        );

    }


    if (current <= 3) {

        return [
            1,
            2,
            3,
            4,
            "...",
            total
        ];

    }


    if (current >= total - 2) {

        return [
            1,
            "...",
            total - 3,
            total - 2,
            total - 1,
            total
        ];

    }


    return [
        1,
        "...",
        current - 1,
        current,
        current + 1,
        "...",
        total
    ];

}