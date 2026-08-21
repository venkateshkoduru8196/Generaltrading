import { FaSearch } from "react-icons/fa";

import "./AccountSearch.css";

export default function AccountSearch({

    searchText,

    setSearchText,

    onSearch,

    mode

}) {

    //==========================================================
    // Disable During New / Edit
    //==========================================================

    const disabled =
        mode === "new" ||
        mode === "edit";


    //==========================================================
    // Enter Key Search
    //==========================================================

    const handleKeyDown = (e) => {

        if (e.key === "Enter") {

            onSearch();

        }

    };


    //==========================================================
    // Handle Change
    //==========================================================

    const handleChange = (e) => {

        setSearchText(
            e.target.value
        );

    };


    return (

        <div className="account-search-card">

            {/*==================================================
                Title
            ==================================================*/}

            <div className="account-search-title">

                <h3>
                    SEARCH ACCOUNT
                </h3>

            </div>


            {/*==================================================
                Search Box
            ==================================================*/}

            <div className="account-search-body">

                <div className="account-search-input-wrapper">

                    <FaSearch
                        className="account-search-icon"
                    />


                    <input

                        type="text"

                        value={searchText}

                        placeholder="Search by Account Code / Account Name..."

                        autoComplete="off"

                        disabled={disabled}

                        onChange={
                            handleChange
                        }

                        onKeyDown={
                            handleKeyDown
                        }

                    />

                </div>

            </div>

        </div>

    );

}