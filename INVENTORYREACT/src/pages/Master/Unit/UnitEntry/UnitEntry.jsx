import {
    useCallback,
    useEffect,
    useMemo,
    useState
} from "react";


//==========================================================
// REACT TOASTIFY
//==========================================================

import {
    toast,
    ToastContainer
} from "react-toastify";

import "react-toastify/dist/ReactToastify.css";


//==========================================================
// UNIT COMPONENTS
//==========================================================

import UnitHeader
    from "../components/UnitHeader/UnitHeader";

import UnitTable
    from "../components/UnitTable/UnitTable";

import UnitPagination
    from "../components/UnitPagination/UnitPagination";

import UnitActionBar
    from "../components/UnitActionBar/UnitActionBar";


//==========================================================
// SERVICES
//==========================================================

import unitService
    from "../../../../services/unitService";


//==========================================================
// EDIT PASSWORD MODAL
//==========================================================

import EditPasswordModal
    from "../../../../Authentication/components/EditPasswordModal";


//==========================================================
// CSS
//==========================================================

import "./UnitEntry.css";


//==========================================================
// INITIAL FORM
//==========================================================

const INITIAL_FORM = {
    id: null,

    code: "",

    description: "",

    isActive: true
};


//==========================================================
// ENTRY PAGE
//==========================================================

export default function UnitEntry() {

    //==========================================================
    // FORM
    //==========================================================

    const [form, setForm] =
        useState(INITIAL_FORM);


    //==========================================================
    // MODE
    //
    // initial = no record selected
    // new     = creating new record
    // view    = displaying selected record
    // edit    = editing selected record
    //==========================================================

    const [mode, setMode] =
        useState("initial");


    //==========================================================
    // UNITS
    //==========================================================

    const [units, setUnits] =
        useState([]);


    //==========================================================
    // SELECTED RECORD
    //==========================================================

    const [selectedId, setSelectedId] =
        useState(null);


    //==========================================================
    // SEARCH
    //==========================================================

    const [searchText, setSearchText] =
        useState("");

    const [searchBy, setSearchBy] =
        useState("code");


    //==========================================================
    // PAGINATION
    //==========================================================

    const [currentPage, setCurrentPage] =
        useState(1);

    const [rowsPerPage, setRowsPerPage] =
        useState(10);


    //==========================================================
    // LOADING
    //==========================================================

    const [loading, setLoading] =
        useState(false);


    //==========================================================
    // PASSWORD MODAL
    //==========================================================

    const [passwordModal, setPasswordModal] =
        useState({
            show: false,
            action: null
        });


    //==========================================================
    // TOAST HELPERS
    //==========================================================

    const showSuccess = (message) => {

        toast.success(message);

    };


    const showError = (message) => {

        toast.error(message);

    };


    const showWarning = (message) => {

        toast.warning(message);

    };


    //==========================================================
    // GET API ERROR MESSAGE
    //==========================================================

    const getErrorMessage = (error, fallback) => {

        return (
            error?.response?.data?.message ||
            error?.response?.data?.Message ||
            error?.message ||
            fallback
        );

    };


    //==========================================================
    // LOAD ALL UNITS
    //==========================================================

    const loadUnits = useCallback(async () => {

        try {

            setLoading(true);

            const data =
                await unitService.getAll();

            setUnits(
                Array.isArray(data)
                    ? data
                    : []
            );

        }
        catch (error) {

            console.error(
                "Failed to load units:",
                error
            );

            showError(
                getErrorMessage(
                    error,
                    "Unable to load units."
                )
            );

        }
        finally {

            setLoading(false);

        }

    }, []);


    //==========================================================
    // INITIAL LOAD
    //==========================================================

    useEffect(() => {

        loadUnits();

    }, [loadUnits]);


    //==========================================================
    // FILTER
    //==========================================================

    const filteredUnits =
        useMemo(() => {

            if (!searchText.trim()) {

                return units;

            }


            const search =
                searchText
                    .trim()
                    .toLowerCase();


            return units.filter(unit => {

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
                            `${unit.code ?? ""} ${
                                unit.description ?? ""
                            }`;

                        break;

                }


                return value
                    .toString()
                    .toLowerCase()
                    .includes(search);

            });

        }, [
            units,
            searchText,
            searchBy
        ]);


    //==========================================================
    // TOTAL PAGES
    //==========================================================

    const totalPages =
        Math.ceil(
            filteredUnits.length /
            rowsPerPage
        );


    //==========================================================
    // KEEP PAGE VALID
    //==========================================================

    useEffect(() => {

        if (
            totalPages > 0 &&
            currentPage > totalPages
        ) {

            setCurrentPage(totalPages);

        }


        if (
            totalPages === 0 &&
            currentPage !== 1
        ) {

            setCurrentPage(1);

        }

    }, [
        currentPage,
        totalPages
    ]);


    //==========================================================
    // PAGINATED DATA
    //==========================================================

    const paginatedUnits =
        useMemo(() => {

            const start =
                (currentPage - 1) *
                rowsPerPage;

            return filteredUnits.slice(
                start,
                start + rowsPerPage
            );

        }, [
            filteredUnits,
            currentPage,
            rowsPerPage
        ]);


    //==========================================================
    // NEW
    //==========================================================

    const handleNew = () => {

        setForm({
            ...INITIAL_FORM
        });

        setSelectedId(null);

        setMode("new");

        setSearchText("");

        setCurrentPage(1);

    };


    //==========================================================
    // SELECT RECORD
    //==========================================================

    const handleSelectRecord = (id) => {

        setSelectedId(id);

    };


    //==========================================================
    // GET SELECTED UNIT
    //==========================================================

    const getSelectedUnit = () => {

        if (!selectedId) {

            return null;

        }


        return units.find(
            x =>
                Number(x.id) ===
                Number(selectedId)
        ) || null;

    };


    //==========================================================
    // VIEW
    //
    // View is only for displaying the selected record.
    //==========================================================

    const handleView = async () => {

        if (!selectedId) {

            showWarning(
                "Please select a Unit."
            );

            return;

        }


        try {

            setLoading(true);


            const data =
                await unitService.getById(
                    selectedId
                );


            if (!data) {

                showError(
                    "Unit not found."
                );

                return;

            }


            setForm({

                id:
                    data.id,

                code:
                    data.code ?? "",

                description:
                    data.description ?? "",

                isActive:
                    data.isActive !== false

            });


            setMode("view");


            showSuccess(
                "Unit loaded successfully."
            );

        }
        catch (error) {

            console.error(
                "View Unit error:",
                error
            );

            showError(
                getErrorMessage(
                    error,
                    "Unable to view Unit."
                )
            );

        }
        finally {

            setLoading(false);

        }

    };


    //==========================================================
    // SAVE
    //==========================================================

    const handleSave = async () => {

        if (!form.code.trim()) {

            showWarning(
                "Please enter Unit Code."
            );

            return;

        }


        if (!form.description.trim()) {

            showWarning(
                "Please enter Unit Description."
            );

            return;

        }


        try {

            setLoading(true);


            await unitService.create({

                code:
                    form.code.trim(),

                description:
                    form.description.trim()

            });


            //==================================================
            // RELOAD DATA
            //==================================================

            const data =
                await unitService.getAll();


            const latestUnits =
                Array.isArray(data)
                    ? data
                    : [];


            setUnits(latestUnits);


            //==================================================
            // FIND CREATED UNIT
            //==================================================

            const created =
                latestUnits.find(
                    x =>
                        x.code?.toLowerCase() ===
                        form.code
                            .trim()
                            .toLowerCase()
                );


            if (created) {

                setSelectedId(
                    created.id
                );


                setForm({

                    id:
                        created.id,

                    code:
                        created.code ?? "",

                    description:
                        created.description ?? "",

                    isActive:
                        created.isActive !== false

                });

            }


            //==================================================
            // AUTOMATIC VIEW MODE
            //==================================================

            setMode("view");


            showSuccess(
                "Unit created successfully."
            );

        }
        catch (error) {

            console.error(
                "Create Unit error:",
                error
            );

            showError(
                getErrorMessage(
                    error,
                    "Unable to create Unit."
                )
            );

        }
        finally {

            setLoading(false);

        }

    };


    //==========================================================
    // EDIT
    //
    // Grid selection can directly trigger Edit.
    // Password is required.
    //==========================================================

    const handleEdit = () => {

        if (!selectedId) {

            showWarning(
                "Please select a Unit."
            );

            return;

        }


        setPasswordModal({

            show: true,

            action: "edit"

        });

    };


    //==========================================================
    // DELETE
    //
    // Grid selection can directly trigger Delete.
    // Password is required.
    //==========================================================

    const handleDelete = () => {

        if (!selectedId) {

            showWarning(
                "Please select a Unit."
            );

            return;

        }


        setPasswordModal({

            show: true,

            action: "delete"

        });

    };


    //==========================================================
    // PASSWORD VERIFIED
    //
    // The EditPasswordModal sends the password here.
    // We verify it through the backend before allowing
    // Edit/Delete.
    //==========================================================

    const handlePasswordVerified =
        async (password) => {

            try {

                setLoading(true);


                //================================================
                // VERIFY PASSWORD
                //================================================

                const { verifyEditPassword } =
                    await import(
                        "../../../../services/authService"
                    );


                const result =
                    await verifyEditPassword(
                        password
                    );


                if (
                    result?.success === false
                ) {

                    showError(
                        result.message ||
                        "Invalid Edit Password."
                    );

                    return;

                }


                //================================================
                // EDIT
                //================================================

                if (
                    passwordModal.action ===
                    "edit"
                ) {

                    const selected =
                        getSelectedUnit();


                    if (!selected) {

                        showError(
                            "Selected Unit not found."
                        );

                        return;

                    }


                    setForm({

                        id:
                            selected.id,

                        code:
                            selected.code ?? "",

                        description:
                            selected.description ?? "",

                        isActive:
                            selected.isActive !== false

                    });


                    setMode("edit");


                    setPasswordModal({

                        show: false,

                        action: null

                    });


                    showSuccess(
                        "Edit authorization successful."
                    );


                    return;

                }


                //================================================
                // DELETE
                //================================================

                if (
                    passwordModal.action ===
                    "delete"
                ) {

                    const confirmed =
                        window.confirm(
                            "Are you sure you want to delete this Unit?"
                        );


                    if (!confirmed) {

                        setPasswordModal({

                            show: false,

                            action: null

                        });

                        return;

                    }


                    await unitService.delete(
                        selectedId
                    );


                    //================================================
                    // RESET
                    //================================================

                    setPasswordModal({

                        show: false,

                        action: null

                    });


                    setSelectedId(null);


                    setForm({
                        ...INITIAL_FORM
                    });


                    setMode("initial");


                    await loadUnits();


                    showSuccess(
                        "Unit deleted successfully."
                    );

                }

            }
            catch (error) {

                console.error(
                    "Unit authorization/action error:",
                    error
                );

                showError(
                    getErrorMessage(
                        error,
                        "Authorization failed."
                    )
                );

            }
            finally {

                setLoading(false);

            }

        };


    //==========================================================
    // UPDATE
    //==========================================================

    const handleUpdate = async () => {

        if (!form.id) {

            showWarning(
                "No Unit selected."
            );

            return;

        }


        if (!form.code.trim()) {

            showWarning(
                "Please enter Unit Code."
            );

            return;

        }


        if (!form.description.trim()) {

            showWarning(
                "Please enter Unit Description."
            );

            return;

        }


        try {

            setLoading(true);


            await unitService.update({

                id:
                    form.id,

                code:
                    form.code.trim(),

                description:
                    form.description.trim()

            });


            //==================================================
            // RELOAD DATA
            //==================================================

            const data =
                await unitService.getAll();


            const latestUnits =
                Array.isArray(data)
                    ? data
                    : [];


            setUnits(latestUnits);


            //==================================================
            // FIND UPDATED UNIT
            //==================================================

            const updated =
                latestUnits.find(
                    x =>
                        Number(x.id) ===
                        Number(form.id)
                );


            if (updated) {

                setSelectedId(
                    updated.id
                );


                setForm({

                    id:
                        updated.id,

                    code:
                        updated.code ?? "",

                    description:
                        updated.description ?? "",

                    isActive:
                        updated.isActive !== false

                });

            }


            //==================================================
            // AUTOMATIC VIEW MODE
            //==================================================

            setMode("view");


            showSuccess(
                "Unit updated successfully."
            );

        }
        catch (error) {

            console.error(
                "Update Unit error:",
                error
            );

            showError(
                getErrorMessage(
                    error,
                    "Unable to update Unit."
                )
            );

        }
        finally {

            setLoading(false);

        }

    };


    //==========================================================
    // PRINT
    //==========================================================

    const handlePrint = () => {

        if (!selectedId) {

            showWarning(
                "Please select a Unit."
            );

            return;

        }


        window.print();

    };


    //==========================================================
    // PAGE CHANGE
    //==========================================================

    const handlePageChange =
        (page) => {

            if (page < 1) {

                return;

            }


            if (
                totalPages > 0 &&
                page > totalPages
            ) {

                return;

            }


            setCurrentPage(page);

        };


    //==========================================================
    // ROWS PER PAGE
    //==========================================================

    const handleRowsPerPageChange =
        (value) => {

            setRowsPerPage(value);

            setCurrentPage(1);

        };


    //==========================================================
    // SEARCH CHANGE
    //==========================================================

    const handleSearchTextChange =
        (value) => {

            setSearchText(value);

            setCurrentPage(1);

        };


    //==========================================================
    // SEARCH TYPE CHANGE
    //==========================================================

    const handleSearchByChange =
        (value) => {

            setSearchBy(value);

            setCurrentPage(1);

        };


    //==========================================================
    // CLOSE PASSWORD MODAL
    //==========================================================

    const handlePasswordClose = () => {

        setPasswordModal({

            show: false,

            action: null

        });

    };


    //==========================================================
    // RENDER
    //==========================================================

    return (

        <div
            className={
                `unit-entry-page ${
                    loading
                        ? "loading"
                        : ""
                }`
            }
        >

            {/*==================================================
                HEADER
            ==================================================*/}

            <UnitHeader

                form={form}

                setForm={setForm}

                mode={mode}

            />


            {/*==================================================
                ACTION BAR
            ==================================================*/}

            <UnitActionBar

                mode={mode}

                selectedId={selectedId}

                onNew={handleNew}

                onSave={handleSave}

                onView={handleView}

                onEdit={handleEdit}

                onUpdate={handleUpdate}

                onDelete={handleDelete}

                onPrint={handlePrint}

            />


            {/*==================================================
                TABLE
            ==================================================*/}

            <UnitTable

                units={paginatedUnits}

                selectedId={selectedId}

                setSelectedId={
                    handleSelectRecord
                }

                searchText={searchText}

                setSearchText={
                    handleSearchTextChange
                }

                searchBy={searchBy}

                setSearchBy={
                    handleSearchByChange
                }

                mode={mode}

            />


            {/*==================================================
                PAGINATION
            ==================================================*/}

            <UnitPagination

                totalRecords={
                    filteredUnits.length
                }

                currentPage={currentPage}

                rowsPerPage={rowsPerPage}

                totalPages={totalPages}

                onPageChange={
                    handlePageChange
                }

                onRowsPerPageChange={
                    handleRowsPerPageChange
                }

            />


            {/*==================================================
                EDIT PASSWORD MODAL
            ==================================================*/}

            <EditPasswordModal

                show={
                    passwordModal.show
                }

                title="Authorization Required"

                message={
                    passwordModal.action ===
                    "delete"

                        ?

                        "Please enter your Edit Password to delete this Unit."

                        :

                        "Please enter your Edit Password to edit this Unit."
                }

                onVerify={
                    handlePasswordVerified
                }

                onClose={
                    handlePasswordClose
                }

            />


            {/*==================================================
                REACT TOAST CONTAINER
            ==================================================*/}

            <ToastContainer

                position="top-right"

                autoClose={3000}

                hideProgressBar={false}

                newestOnTop

                closeOnClick

                pauseOnHover

                draggable

                theme="colored"

            />

        </div>

    );

}



















// import {
//     useCallback,
//     useEffect,
//     useMemo,
//     useState
// } from "react";

// //==========================================================
// // UNIT COMPONENTS
// //==========================================================

// import UnitHeader
//     from "../components/UnitHeader/UnitHeader";

// import UnitTable
//     from "../components/UnitTable/UnitTable";

// import UnitPagination
//     from "../components/UnitPagination/UnitPagination";

// import UnitActionBar
//     from "../components/UnitActionBar/UnitActionBar";


// import unitService
//     from "../../../../services/unitService";

// import EditPasswordModal
//     from "../../../../Authentication/components/EditPasswordModal";

// import "./UnitEntry.css";


// //==========================================================
// // INITIAL FORM
// //==========================================================

// const INITIAL_FORM = {
//     id: null,

//     code: "",

//     description: "",

//     isActive: true
// };


// //==========================================================
// // ENTRY PAGE
// //==========================================================

// export default function UnitEntry() {

//     //==========================================================
//     // FORM
//     //==========================================================

//     const [form, setForm] =
//         useState(INITIAL_FORM);


//     //==========================================================
//     // MODE
//     //
//     // initial = no record selected
//     // new     = creating new record
//     // view    = displaying selected record
//     // edit    = editing selected record
//     //==========================================================

//     const [mode, setMode] =
//         useState("initial");


//     //==========================================================
//     // UNITS
//     //==========================================================

//     const [units, setUnits] =
//         useState([]);


//     //==========================================================
//     // SELECTED RECORD
//     //==========================================================

//     const [selectedId, setSelectedId] =
//         useState(null);


//     //==========================================================
//     // SEARCH
//     //==========================================================

//     const [searchText, setSearchText] =
//         useState("");

//     const [searchBy, setSearchBy] =
//         useState("code");


//     //==========================================================
//     // PAGINATION
//     //==========================================================

//     const [currentPage, setCurrentPage] =
//         useState(1);

//     const [rowsPerPage, setRowsPerPage] =
//         useState(10);


//     //==========================================================
//     // LOADING
//     //==========================================================

//     const [loading, setLoading] =
//         useState(false);


//     //==========================================================
//     // PASSWORD MODAL
//     //==========================================================

//     const [passwordModal, setPasswordModal] =
//         useState({
//             show: false,
//             action: null
//         });


//     //==========================================================
//     // LOAD ALL UNITS
//     //==========================================================

//     const loadUnits = useCallback(async () => {

//         try {

//             setLoading(true);

//             const data =
//                 await unitService.getAll();

//             setUnits(
//                 Array.isArray(data)
//                     ? data
//                     : []
//             );

//         }
//         catch (error) {

//             console.error(
//                 "Failed to load units:",
//                 error
//             );

//             alert(
//                 error?.response?.data?.message ||
//                 "Unable to load units."
//             );

//         }
//         finally {

//             setLoading(false);

//         }

//     }, []);


//     //==========================================================
//     // INITIAL LOAD
//     //==========================================================

//     useEffect(() => {

//         loadUnits();

//     }, [loadUnits]);


//     //==========================================================
//     // FILTER
//     //==========================================================

//     const filteredUnits =
//         useMemo(() => {

//             if (!searchText.trim()) {

//                 return units;
//             }

//             const search =
//                 searchText
//                     .trim()
//                     .toLowerCase();


//             return units.filter(unit => {

//                 let value = "";


//                 switch (searchBy) {

//                     case "code":

//                         value =
//                             unit.code ?? "";

//                         break;


//                     case "description":

//                         value =
//                             unit.description ?? "";

//                         break;


//                     case "status":

//                         value =
//                             unit.isActive
//                                 ? "active"
//                                 : "inactive";

//                         break;


//                     case "createdBy":

//                         value =
//                             unit.createdBy ?? "";

//                         break;


//                     case "createdOn":

//                         value =
//                             unit.createdOn
//                                 ? new Date(
//                                     unit.createdOn
//                                 )
//                                     .toLocaleDateString()
//                                 : "";

//                         break;


//                     default:

//                         value =
//                             `${unit.code ?? ""} ${
//                                 unit.description ?? ""
//                             }`;

//                         break;
//                 }


//                 return value
//                     .toString()
//                     .toLowerCase()
//                     .includes(search);

//             });

//         }, [
//             units,
//             searchText,
//             searchBy
//         ]);


//     //==========================================================
//     // TOTAL PAGES
//     //==========================================================

//     const totalPages =
//         Math.ceil(
//             filteredUnits.length /
//             rowsPerPage
//         );


//     //==========================================================
//     // KEEP PAGE VALID
//     //==========================================================

//     useEffect(() => {

//         if (
//             totalPages > 0 &&
//             currentPage > totalPages
//         ) {

//             setCurrentPage(totalPages);

//         }

//         if (
//             totalPages === 0 &&
//             currentPage !== 1
//         ) {

//             setCurrentPage(1);

//         }

//     }, [
//         currentPage,
//         totalPages
//     ]);


//     //==========================================================
//     // PAGINATED DATA
//     //==========================================================

//     const paginatedUnits =
//         useMemo(() => {

//             const start =
//                 (currentPage - 1) *
//                 rowsPerPage;

//             return filteredUnits.slice(
//                 start,
//                 start + rowsPerPage
//             );

//         }, [
//             filteredUnits,
//             currentPage,
//             rowsPerPage
//         ]);


//     //==========================================================
//     // NEW
//     //==========================================================

//     const handleNew = () => {

//         setForm({
//             ...INITIAL_FORM
//         });

//         setSelectedId(null);

//         setMode("new");

//         setSearchText("");

//         setCurrentPage(1);

//     };


//     //==========================================================
//     // SELECT RECORD
//     //==========================================================

//     const handleSelectRecord = (id) => {

//         setSelectedId(id);

//     };


//     //==========================================================
//     // GET SELECTED UNIT
//     //==========================================================

//     const getSelectedUnit = () => {

//         if (!selectedId)
//             return null;

//         return units.find(
//             x => Number(x.id) === Number(selectedId)
//         ) || null;

//     };


//     //==========================================================
//     // VIEW
//     //==========================================================

//     const handleView = async () => {

//         if (!selectedId) {

//             alert(
//                 "Please select a Unit."
//             );

//             return;
//         }


//         try {

//             setLoading(true);

//             const data =
//                 await unitService.getById(
//                     selectedId
//                 );


//             if (!data) {

//                 alert(
//                     "Unit not found."
//                 );

//                 return;
//             }


//             setForm({
//                 id: data.id,

//                 code:
//                     data.code ?? "",

//                 description:
//                     data.description ?? "",

//                 isActive:
//                     data.isActive !== false
//             });


//             setMode("view");

//         }
//         catch (error) {

//             console.error(
//                 "View Unit error:",
//                 error
//             );

//             alert(
//                 error?.response?.data?.message ||
//                 "Unable to view Unit."
//             );

//         }
//         finally {

//             setLoading(false);

//         }

//     };


//     //==========================================================
//     // SAVE
//     //==========================================================

//     const handleSave = async () => {

//         if (!form.code.trim()) {

//             alert(
//                 "Please enter Unit Code."
//             );

//             return;
//         }


//         if (!form.description.trim()) {

//             alert(
//                 "Please enter Unit Description."
//             );

//             return;
//         }


//         try {

//             setLoading(true);


//             await unitService.create({

//                 code:
//                     form.code.trim(),

//                 description:
//                     form.description.trim()

//             });


//             alert(
//                 "Unit created successfully."
//             );


//             // Reload latest data

//             const data =
//                 await unitService.getAll();


//             const latestUnits =
//                 Array.isArray(data)
//                     ? data
//                     : [];


//             setUnits(latestUnits);


//             // Find newly created unit
//             // by code.

//             const created =
//                 latestUnits.find(
//                     x =>
//                         x.code?.toLowerCase() ===
//                         form.code
//                             .trim()
//                             .toLowerCase()
//                 );


//             if (created) {

//                 setSelectedId(
//                     created.id
//                 );


//                 setForm({

//                     id: created.id,

//                     code:
//                         created.code ?? "",

//                     description:
//                         created.description ?? "",

//                     isActive:
//                         created.isActive !== false

//                 });

//             }


//             // IMPORTANT:
//             // Automatically enter View mode.

//             setMode("view");

//         }
//         catch (error) {

//             console.error(
//                 "Create Unit error:",
//                 error
//             );

//             alert(
//                 error?.response?.data?.message ||
//                 "Unable to create Unit."
//             );

//         }
//         finally {

//             setLoading(false);

//         }

//     };


//     //==========================================================
//     // EDIT
//     //
//     // Password is required before entering edit mode.
//     //==========================================================

//     const handleEdit = () => {

//         if (!selectedId) {

//             alert(
//                 "Please select a Unit."
//             );

//             return;
//         }


//         setPasswordModal({

//             show: true,

//             action: "edit"

//         });

//     };


//     //==========================================================
//     // DELETE
//     //
//     // Password is required before deleting.
//     //==========================================================

//     const handleDelete = () => {

//         if (!selectedId) {

//             alert(
//                 "Please select a Unit."
//             );

//             return;
//         }


//         setPasswordModal({

//             show: true,

//             action: "delete"

//         });

//     };


//     //==========================================================
//     // PASSWORD VERIFIED
//     //==========================================================

//     const handlePasswordVerified =
//         async (password) => {

//             try {

//                 setLoading(true);


//                 //================================================
//                 // EDIT
//                 //================================================

//                 if (
//                     passwordModal.action ===
//                     "edit"
//                 ) {

//                     // The modal already verifies
//                     // the password through the
//                     // Auth API.

//                     const selected =
//                         getSelectedUnit();


//                     if (!selected) {

//                         alert(
//                             "Selected Unit not found."
//                         );

//                         return;
//                     }


//                     setForm({

//                         id: selected.id,

//                         code:
//                             selected.code ?? "",

//                         description:
//                             selected.description ?? "",

//                         isActive:
//                             selected.isActive !== false

//                     });


//                     setMode("edit");


//                     setPasswordModal({

//                         show: false,

//                         action: null

//                     });


//                     return;
//                 }


//                 //================================================
//                 // DELETE
//                 //================================================

//                 if (
//                     passwordModal.action ===
//                     "delete"
//                 ) {

//                     const confirmed =
//                         window.confirm(
//                             "Are you sure you want to delete this Unit?"
//                         );


//                     if (!confirmed) {

//                         setPasswordModal({

//                             show: false,

//                             action: null

//                         });

//                         return;
//                     }


//                     await unitService.delete(
//                         selectedId
//                     );


//                     alert(
//                         "Unit deleted successfully."
//                     );


//                     setPasswordModal({

//                         show: false,

//                         action: null

//                     });


//                     setSelectedId(null);

//                     setForm({
//                         ...INITIAL_FORM
//                     });


//                     setMode("initial");


//                     await loadUnits();

//                 }

//             }
//             catch (error) {

//                 console.error(
//                     "Unit authorization/action error:",
//                     error
//                 );

//                 alert(
//                     error?.response?.data?.message ||
//                     "Authorization failed."
//                 );

//             }
//             finally {

//                 setLoading(false);

//             }

//         };


//     //==========================================================
//     // UPDATE
//     //==========================================================

//     const handleUpdate = async () => {

//         if (!form.id) {

//             alert(
//                 "No Unit selected."
//             );

//             return;
//         }


//         if (!form.code.trim()) {

//             alert(
//                 "Please enter Unit Code."
//             );

//             return;
//         }


//         if (!form.description.trim()) {

//             alert(
//                 "Please enter Unit Description."
//             );

//             return;
//         }


//         try {

//             setLoading(true);


//             await unitService.update({

//                 id: form.id,

//                 code:
//                     form.code.trim(),

//                 description:
//                     form.description.trim()

//             });


//             alert(
//                 "Unit updated successfully."
//             );


//             // Reload latest data

//             const data =
//                 await unitService.getAll();


//             const latestUnits =
//                 Array.isArray(data)
//                     ? data
//                     : [];


//             setUnits(latestUnits);


//             const updated =
//                 latestUnits.find(
//                     x =>
//                         Number(x.id) ===
//                         Number(form.id)
//                 );


//             if (updated) {

//                 setSelectedId(
//                     updated.id
//                 );


//                 setForm({

//                     id: updated.id,

//                     code:
//                         updated.code ?? "",

//                     description:
//                         updated.description ?? "",

//                     isActive:
//                         updated.isActive !== false

//                 });

//             }


//             // IMPORTANT:
//             // Automatically return to View.

//             setMode("view");

//         }
//         catch (error) {

//             console.error(
//                 "Update Unit error:",
//                 error
//             );

//             alert(
//                 error?.response?.data?.message ||
//                 "Unable to update Unit."
//             );

//         }
//         finally {

//             setLoading(false);

//         }

//     };


//     //==========================================================
//     // PRINT
//     //==========================================================

//     const handlePrint = () => {

//         if (!selectedId) {

//             alert(
//                 "Please select a Unit."
//             );

//             return;
//         }


//         window.print();

//     };


//     //==========================================================
//     // PAGE CHANGE
//     //==========================================================

//     const handlePageChange = (page) => {

//         if (page < 1)
//             return;

//         if (
//             totalPages > 0 &&
//             page > totalPages
//         )
//             return;

//         setCurrentPage(page);

//     };


//     //==========================================================
//     // ROWS PER PAGE
//     //==========================================================

//     const handleRowsPerPageChange =
//         (value) => {

//             setRowsPerPage(value);

//             setCurrentPage(1);

//         };


//     //==========================================================
//     // SEARCH CHANGE
//     //==========================================================

//     const handleSearchTextChange =
//         (value) => {

//             setSearchText(value);

//             setCurrentPage(1);

//         };


//     //==========================================================
//     // SEARCH TYPE CHANGE
//     //==========================================================

//     const handleSearchByChange =
//         (value) => {

//             setSearchBy(value);

//             setCurrentPage(1);

//         };


//     //==========================================================
//     // CLOSE PASSWORD MODAL
//     //==========================================================

//     const handlePasswordClose = () => {

//         setPasswordModal({

//             show: false,

//             action: null

//         });

//     };


//     //==========================================================
//     // RENDER
//     //==========================================================

//     return (

//         <div
//             className={
//                 `unit-entry-page ${
//                     loading
//                         ? "loading"
//                         : ""
//                 }`
//             }
//         >

//             {/*==================================================
//                 HEADER / FORM
//             ==================================================*/}

//             <UnitHeader

//                 form={form}

//                 setForm={setForm}

//                 mode={mode}

//             />


           
//             {/*==================================================
//                 ACTION BAR
//             ==================================================*/}

//             <UnitActionBar

//                 mode={mode}

//                 selectedId={selectedId}

//                 onNew={handleNew}

//                 onSave={handleSave}

//                 onView={handleView}

//                 onEdit={handleEdit}

//                 onUpdate={handleUpdate}

//                 onDelete={handleDelete}

//                 onPrint={handlePrint}

//             />













//             {/*==================================================
//                 TABLE
//             ==================================================*/}

//             <UnitTable

//                 units={paginatedUnits}

//                 selectedId={selectedId}

//                 setSelectedId={
//                     handleSelectRecord
//                 }

//                 searchText={searchText}

//                 setSearchText={
//                     handleSearchTextChange
//                 }

//                 searchBy={searchBy}

//                 setSearchBy={
//                     handleSearchByChange
//                 }

//                 mode={mode}

//             />


//             {/*==================================================
//                 PAGINATION
//             ==================================================*/}

//             <UnitPagination

//                 totalRecords={
//                     filteredUnits.length
//                 }

//                 currentPage={currentPage}

//                 rowsPerPage={rowsPerPage}

//                 totalPages={totalPages}

//                 onPageChange={
//                     handlePageChange
//                 }

//                 onRowsPerPageChange={
//                     handleRowsPerPageChange
//                 }

//             />



//             {/*==================================================
//                 EDIT PASSWORD
//             ==================================================*/}

//             <EditPasswordModal

//                 show={
//                     passwordModal.show
//                 }

//                 title="Authorization Required"

//                 message={
//                     passwordModal.action ===
//                     "delete"

//                         ?

//                         "Please enter your Edit Password to delete this Unit."

//                         :

//                         "Please enter your Edit Password to edit this Unit."
//                 }

//                 onVerify={
//                     handlePasswordVerified
//                 }

//                 onClose={
//                     handlePasswordClose
//                 }

//             />

//         </div>
//     );
// }