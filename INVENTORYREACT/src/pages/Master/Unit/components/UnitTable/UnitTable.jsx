import { FaSearch } from "react-icons/fa";

import "./UnitTable.css";

export default function UnitTable({

    units,

    selectedId,

    setSelectedId,

    searchText,

    setSearchText,

    searchBy,

    setSearchBy,

    mode

}) {

    //==========================================================
    // DISABLE TABLE SEARCH DURING NEW / EDIT
    //==========================================================

    const disabled =
        mode === "new" ||
        mode === "edit";


    //==========================================================
    // SAFE ARRAY
    //==========================================================

    const safeUnits =
        Array.isArray(units)
            ? units
            : [];


    //==========================================================
    // SEARCH
    //==========================================================

    const filteredUnits =
        safeUnits.filter(unit => {

            if (!searchText?.trim())
                return true;

            const search =
                searchText
                    .toLowerCase()
                    .trim();


            let value = "";


            switch (searchBy) {

                case "code":

                    value =
                        unit.code ?? "";

                    break;


                case "description":

                    value =
                        unit.description ?? "";

                    break;


                case "status":

                    value =
                        unit.isActive
                            ? "active"
                            : "inactive";

                    break;


                case "createdBy":

                    value =
                        unit.createdBy ?? "";

                    break;


                case "createdOn":

                    value =
                        unit.createdOn
                            ? new Date(
                                unit.createdOn
                            ).toLocaleDateString()
                            : "";

                    break;


                default:

                    value =
                        `${unit.code ?? ""} ${unit.description ?? ""}`;

                    break;
            }


            return value
                .toString()
                .toLowerCase()
                .includes(search);
        });


    //==========================================================
    // RENDER
    //==========================================================

    return (

        <div className="unit-table-card">

            {/*==================================================
                HEADER
            ==================================================*/}

            <div className="unit-table-header">

                <h3>
                    UNIT LIST
                </h3>

                <span>

                    Total Records :
                    {" "}
                    {filteredUnits.length}

                </span>

            </div>


            {/*==================================================
                TOOLBAR
            ==================================================*/}

            <div className="unit-table-toolbar">

                {/* Search */}

                <div className="table-search-box">

                    <FaSearch
                        className="table-search-icon"
                    />

                    <input

                        type="text"

                        placeholder="Search..."

                        value={searchText}

                        disabled={disabled}

                        autoComplete="off"

                        onChange={(e) =>
                            setSearchText(
                                e.target.value
                            )
                        }

                    />

                </div>


                {/* Search By */}

                <div className="table-filter">

                    <label>
                        Search By
                    </label>

                    <select

                        value={searchBy}

                        disabled={disabled}

                        onChange={(e) =>
                            setSearchBy(
                                e.target.value
                            )
                        }

                    >

                        <option value="code">
                            Unit Code
                        </option>

                        <option value="description">
                            Description
                        </option>

                        <option value="status">
                            Status
                        </option>

                        <option value="createdBy">
                            Created By
                        </option>

                        <option value="createdOn">
                            Created On
                        </option>

                    </select>

                </div>

            </div>


            {/*==================================================
                TABLE
            ==================================================*/}

            <div className="unit-table-wrapper">

                <table className="unit-table">

                    <thead>

                        <tr>

                            <th width="70">
                                Sl
                            </th>

                            <th width="180">
                                Unit Code
                            </th>

                            <th>
                                Description
                            </th>

                            <th width="140">
                                Status
                            </th>

                            <th width="180">
                                Created By
                            </th>

                            <th width="170">
                                Created On
                            </th>

                        </tr>

                    </thead>


                    <tbody>

                        {filteredUnits.length === 0

                            ?

                            (

                                <tr>

                                    <td
                                        colSpan="6"
                                        className="no-data"
                                    >

                                        No Units Found

                                    </td>

                                </tr>

                            )

                            :

                            (

                                filteredUnits.map(
                                    (unit, index) => (

                                        <tr

                                            key={unit.id}

                                            className={
                                                selectedId === unit.id
                                                    ? "selected-row"
                                                    : ""
                                            }

                                            onClick={() =>
                                                setSelectedId(
                                                    unit.id
                                                )
                                            }

                                        >

                                            <td>
                                                {index + 1}
                                            </td>


                                            <td>
                                                {unit.code}
                                            </td>


                                            <td>
                                                {unit.description}
                                            </td>


                                            <td>

                                                <span

                                                    className={
                                                        unit.isActive
                                                            ? "status active"
                                                            : "status inactive"
                                                    }

                                                >

                                                    {
                                                        unit.isActive
                                                            ? "Active"
                                                            : "Inactive"
                                                    }

                                                </span>

                                            </td>


                                            <td>

                                                {
                                                    unit.createdBy
                                                    || "-"
                                                }

                                            </td>


                                            <td>

                                                {
                                                    unit.createdOn

                                                        ?

                                                        new Date(
                                                            unit.createdOn
                                                        ).toLocaleDateString()

                                                        :

                                                        "-"
                                                }

                                            </td>

                                        </tr>

                                    )
                                )

                            )}

                    </tbody>

                </table>

            </div>

        </div>
    );
}