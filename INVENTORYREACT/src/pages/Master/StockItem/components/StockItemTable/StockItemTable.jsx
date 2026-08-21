import { FaSearch } from "react-icons/fa";
import "./StockItemTable.css";

export default function StockItemTable({

    stockItems,

    selectedId,

    setSelectedId,

    searchText,

    setSearchText,

    searchBy,

    setSearchBy,

    mode

}) {

    const disabled =
        mode === "new" ||
        mode === "edit";

    return (

        <div className="stock-table-card">

            {/*==========================================
                Header
            ==========================================*/}

            <div className="stock-table-header">

                <h3>

                    STOCK ITEM LIST

                </h3>

                <span>

                    Total Records : {stockItems.length}

                </span>

            </div>

            {/*==========================================
                Toolbar
            ==========================================*/}

            <div className="stock-table-toolbar">

                {/* Search */}

                <div className="table-search-box">

                    <FaSearch className="table-search-icon"/>

                    <input

                        type="text"

                        placeholder="Search..."

                        value={searchText}

                        disabled={disabled}

                        autoComplete="off"

                        onChange={(e)=>

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

                        onChange={(e)=>

                            setSearchBy(

                                e.target.value

                            )

                        }

                    >

                        <option value="stockCode">

                            Stock Code

                        </option>

                        <option value="stockName">

                            Stock Name

                        </option>

                        <option value="taxRate">

                            GST %

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

            {/*==========================================
                Table
            ==========================================*/}

            <div className="stock-table-wrapper">

                <table className="stock-table">

                    <thead>

                        <tr>

                            <th width="70">

                                Sl

                            </th>

                            <th width="170">

                                Stock Code

                            </th>

                            <th>

                                Stock Name

                            </th>

                            <th width="120">

                                GST %

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

                        {

                            stockItems.length===0

                            ?

                            (

                                <tr>

                                    <td

                                        colSpan="7"

                                        className="no-data"

                                    >

                                        No Stock Items Found

                                    </td>

                                </tr>

                            )

                            :

                            (

                                stockItems.map((item,index)=>(

                                    <tr

                                        key={item.id}

                                        className={

                                            selectedId===item.id

                                            ?

                                            "selected-row"

                                            :

                                            ""

                                        }

                                        onClick={()=>

                                            setSelectedId(item.id)

                                        }

                                    >

                                        <td>

                                            {index+1}

                                        </td>

                                        <td>

                                            {item.stockCode}

                                        </td>

                                        <td>

                                            {item.stockName}

                                        </td>

                                        <td>

                                            {item.taxRate.toFixed(2)} %

                                        </td>

                                        <td>

                                            <span

                                                className={

                                                    item.isActive

                                                    ?

                                                    "status active"

                                                    :

                                                    "status inactive"

                                                }

                                            >

                                                {

                                                    item.isActive

                                                    ?

                                                    "Active"

                                                    :

                                                    "Inactive"

                                                }

                                            </span>

                                        </td>

                                        <td>

                                            {

                                                item.createdBy

                                                ||

                                                "-"

                                            }

                                        </td>

                                        <td>

                                            {

                                                item.createdOn

                                                ?

                                                new Date(item.createdOn)

                                                .toLocaleDateString()

                                                :

                                                "-"

                                            }

                                        </td>

                                    </tr>

                                ))

                            )

                        }

                    </tbody>

                </table>

            </div>

        </div>

    );

}