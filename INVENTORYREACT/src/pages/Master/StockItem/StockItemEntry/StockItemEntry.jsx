import {
    useCallback,
    useEffect,
    useMemo,
    useState
} from "react";

import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import stockItemService
    from "../../../../services/stockItemService";

import {
    verifyEditPassword
} from "../../../../services/authService";

import EditPasswordModal
    from "../../../../Authentication/components/EditPasswordModal";

import StockItemHeader
    from "../components/StockItemHeader/StockItemHeader";

import StockItemTable
    from "../components/StockItemTable/StockItemTable";

import StockItemActionBar
    from "../components/StockItemActionBar/StockItemActionBar";

import StockItemPagination
    from "../components/StockItemPagination/StockItemPagination";


//==========================================================
// INITIAL FORM
//==========================================================

const INITIAL_FORM = {
    id: 0,
    stockCode: "",
    stockName: "",
    taxRate: 0,
    isActive: true
};


//==========================================================
// MODES
//==========================================================

const MODE = {
    INITIAL: "initial",
    NEW: "new",
    VIEW: "view",
    EDIT: "edit"
};


//==========================================================
// PASSWORD ACTIONS
//==========================================================

const PASSWORD_ACTION = {
    EDIT: "edit",
    DELETE: "delete"
};


//==========================================================
// STOCK ITEM ENTRY
//==========================================================

export default function StockItemEntry() {

    //======================================================
    // FORM
    //======================================================

    const [form, setForm] = useState({
        ...INITIAL_FORM
    });


    //======================================================
    // STOCK ITEM LIST
    //======================================================

    const [stockItems, setStockItems] = useState([]);


    //======================================================
    // SELECTED GRID RECORD
    //
    // Selecting a row DOES NOT automatically
    // display the record in the form.
    //======================================================

    const [selectedId, setSelectedId] = useState(null);


    //======================================================
    // MODE
    //======================================================

    const [mode, setMode] = useState(
        MODE.INITIAL
    );


    //======================================================
    // SEARCH
    //======================================================

    const [searchText, setSearchText] =
        useState("");

    const [searchBy, setSearchBy] =
        useState("stockCode");


    //======================================================
    // PAGINATION
    //======================================================

    const [currentPage, setCurrentPage] =
        useState(1);

    const [rowsPerPage, setRowsPerPage] =
        useState(20);


    //======================================================
    // LOADING
    //======================================================

    const [loading, setLoading] =
        useState(false);


    //======================================================
    // PASSWORD MODAL
    //======================================================

    const [showPasswordModal, setShowPasswordModal] =
        useState(false);


    //======================================================
    // PENDING PASSWORD ACTION
    //======================================================

    const [pendingAction, setPendingAction] =
        useState(null);


    //======================================================
    // RECORD WAITING FOR PASSWORD
    //======================================================

    const [pendingStockItemId, setPendingStockItemId] =
        useState(null);


    //==========================================================
    // NORMALIZE ID
    //==========================================================

    const normalizeId = useCallback(
        (value) => {

            if (
                value === null ||
                value === undefined ||
                value === ""
            ) {
                return null;
            }

            const number = Number(value);

            return Number.isFinite(number)
                ? number
                : null;
        },
        []
    );


    //==========================================================
    // EXTRACT API DATA
    //
    // Supports:
    // 1. Axios response
    // 2. API envelope
    // 3. Direct object
    // 4. Direct array
    //==========================================================

    const extractApiData = useCallback(
        (response) => {

            let value = response;


            //==================================================
            // Axios response
            //==================================================

            if (
                value &&
                value.data !== undefined &&
                (
                    value.status !== undefined ||
                    value.config !== undefined ||
                    value.headers !== undefined
                )
            ) {

                value = value.data;

            }


            //==================================================
            // API envelope
            //
            // {
            //     success: true,
            //     message: "...",
            //     data: {...}
            // }
            //==================================================

            if (
                value &&
                typeof value === "object" &&
                value.data !== undefined &&
                (
                    value.success !== undefined ||
                    value.Success !== undefined ||
                    value.message !== undefined ||
                    value.Message !== undefined
                )
            ) {

                return value.data;

            }


            return value;

        },
        []
    );


    //==========================================================
    // API SUCCESS
    //==========================================================

    const isApiSuccess = useCallback(
        (response) => {

            let value = response;


            //==================================================
            // Axios response
            //==================================================

            if (
                value &&
                value.data !== undefined &&
                (
                    value.status !== undefined ||
                    value.config !== undefined ||
                    value.headers !== undefined
                )
            ) {

                value = value.data;

            }


            if (
                value?.success !== undefined
            ) {

                return value.success === true;

            }


            if (
                value?.Success !== undefined
            ) {

                return value.Success === true;

            }


            return true;

        },
        []
    );


    //==========================================================
    // FIND STOCK ITEM
    //==========================================================

    const findStockItem = useCallback(
        (id) => {

            const normalizedId =
                normalizeId(id);


            if (
                normalizedId === null
            ) {

                return null;

            }


            return (
                stockItems.find(
                    item =>
                        normalizeId(item?.id) ===
                        normalizedId
                ) || null
            );

        },
        [
            stockItems,
            normalizeId
        ]
    );


    //==========================================================
    // LOAD STOCK ITEMS
    //==========================================================

    const loadStockItems = useCallback(
        async () => {

            try {

                setLoading(true);


                const response =
                    await stockItemService.getAll();


                const data =
                    extractApiData(response);


                setStockItems(
                    Array.isArray(data)
                        ? data
                        : []
                );

            }
            catch (error) {

                console.error(
                    "Stock Item Load Error:",
                    error
                );


                toast.error(
                    getErrorMessage(
                        error,
                        "Unable to load Stock Items."
                    )
                );

            }
            finally {

                setLoading(false);

            }

        },
        [
            extractApiData
        ]
    );


    //==========================================================
    // INITIAL LOAD
    //==========================================================

    useEffect(
        () => {

            loadStockItems();

        },
        [
            loadStockItems
        ]
    );


    //==========================================================
    // RESET FORM
    //==========================================================

    const resetForm = useCallback(
        () => {

            setForm({
                ...INITIAL_FORM
            });

            setSelectedId(null);

            setPendingAction(null);

            setPendingStockItemId(null);

        },
        []
    );


    //==========================================================
    // PUT STOCK ITEM INTO FORM
    //==========================================================

    const setStockItemToForm = useCallback(
        (stockItem) => {

            if (!stockItem) {

                return;

            }


            const id =
                normalizeId(stockItem.id);


            if (id === null) {

                return;

            }


            setForm({

                id,

                stockCode:
                    stockItem.stockCode ?? "",

                stockName:
                    stockItem.stockName ?? "",

                taxRate:
                    stockItem.taxRate ?? 0,

                isActive:
                    stockItem.isActive ?? true

            });

        },
        [
            normalizeId
        ]
    );


    //==========================================================
    // NEW
    //==========================================================

    const handleNew = useCallback(
        () => {

            resetForm();

            setMode(
                MODE.NEW
            );

            setCurrentPage(1);

            toast.info(
                "Ready for new Stock Item."
            );

        },
        [
            resetForm
        ]
    );


    //==========================================================
    // SAVE
    //
    // CREATE
    // ↓
    // RELOAD
    // ↓
    // FIND CREATED RECORD
    // ↓
    // DISPLAY
    // ↓
    // VIEW MODE
    //==========================================================

    const handleSave = useCallback(
        async () => {

            if (
                mode !== MODE.NEW
            ) {

                toast.warning(
                    "Please click New before saving."
                );

                return;

            }


            const stockCode =
                form.stockCode.trim();

            const stockName =
                form.stockName.trim();

            const taxRate =
                Number(form.taxRate);


            //==================================================
            // VALIDATION
            //==================================================

            if (!stockCode) {

                toast.warning(
                    "Stock Code is required."
                );

                return;

            }


            if (!stockName) {

                toast.warning(
                    "Stock Name is required."
                );

                return;

            }


            if (
                Number.isNaN(taxRate) ||
                taxRate < 0
            ) {

                toast.warning(
                    "Tax Rate cannot be negative."
                );

                return;

            }


            try {

                setLoading(true);


                //================================================
                // CREATE
                //================================================

                const response =
                    await stockItemService.create({

                        stockCode,

                        stockName,

                        taxRate

                    });


                //================================================
                // TRY TO GET CREATED RECORD
                //================================================

                let createdStockItem =
                    extractApiData(response);


                //================================================
                // RELOAD FROM SERVER
                //
                // This makes the frontend independent
                // of the POST response structure.
                //================================================

                const listResponse =
                    await stockItemService.getAll();


                const data =
                    extractApiData(
                        listResponse
                    );


                const refreshedItems =
                    Array.isArray(data)
                        ? data
                        : [];


                setStockItems(
                    refreshedItems
                );


                //================================================
                // If POST did not return entity,
                // find it from refreshed list.
                //================================================

                if (
                    !createdStockItem?.id
                ) {

                    createdStockItem =
                        refreshedItems.find(
                            item =>
                                String(
                                    item?.stockCode ?? ""
                                )
                                    .trim()
                                    .toLowerCase()
                                ===
                                stockCode
                                    .trim()
                                    .toLowerCase()
                        );

                }


                //================================================
                // CREATED RECORD FOUND
                //================================================

                if (
                    createdStockItem?.id
                ) {

                    const createdId =
                        normalizeId(
                            createdStockItem.id
                        );


                    setSelectedId(
                        createdId
                    );


                    setStockItemToForm(
                        createdStockItem
                    );


                    //================================================
                    // AUTOMATIC VIEW MODE
                    //================================================

                    setMode(
                        MODE.VIEW
                    );


                    toast.success(
                        "Stock Item created successfully."
                    );

                    return;

                }


                //================================================
                // CREATED BUT RECORD NOT FOUND
                //================================================

                resetForm();

                setMode(
                    MODE.INITIAL
                );


                toast.success(
                    "Stock Item created successfully."
                );

            }
            catch (error) {

                console.error(
                    "Stock Item Save Error:",
                    error
                );


                toast.error(
                    getErrorMessage(
                        error,
                        "Unable to create Stock Item."
                    )
                );

            }
            finally {

                setLoading(false);

            }

        },
        [
            mode,
            form.stockCode,
            form.stockName,
            form.taxRate,
            extractApiData,
            normalizeId,
            setStockItemToForm,
            resetForm
        ]
    );


    //==========================================================
    // VIEW
    //
    // View is ONLY for displaying a selected record.
    //==========================================================

    const handleView = useCallback(
        async () => {

            const id =
                normalizeId(
                    selectedId
                );


            if (
                id === null
            ) {

                toast.warning(
                    "Please select a Stock Item first."
                );

                return;

            }


            try {

                setLoading(true);


                const response =
                    await stockItemService.getById(
                        id
                    );


                const stockItem =
                    extractApiData(
                        response
                    );


                if (
                    !stockItem?.id
                ) {

                    toast.error(
                        "Stock Item not found."
                    );

                    return;

                }


                const stockItemId =
                    normalizeId(
                        stockItem.id
                    );


                //================================================
                // Refresh selected record in grid
                //================================================

                setStockItems(
                    previous =>
                        previous.map(
                            item =>
                                normalizeId(item.id) ===
                                stockItemId
                                    ? stockItem
                                    : item
                        )
                );


                setSelectedId(
                    stockItemId
                );


                //================================================
                // Display in form
                //================================================

                setStockItemToForm(
                    stockItem
                );


                //================================================
                // VIEW MODE
                //================================================

                setMode(
                    MODE.VIEW
                );


                toast.info(
                    "Stock Item loaded."
                );

            }
            catch (error) {

                console.error(
                    "Stock Item View Error:",
                    error
                );


                toast.error(
                    getErrorMessage(
                        error,
                        "Unable to load Stock Item."
                    )
                );

            }
            finally {

                setLoading(false);

            }

        },
        [
            selectedId,
            normalizeId,
            extractApiData,
            setStockItemToForm
        ]
    );


    //==========================================================
    // EDIT REQUEST
    //
    // IMPORTANT:
    //
    // Edit DOES NOT require View mode.
    //
    // Grid selection
    // ↓
    // Edit
    // ↓
    // Password
    // ↓
    // Edit mode
    //==========================================================

    const handleEdit = useCallback(
        () => {

            const id =
                normalizeId(
                    selectedId
                );


            if (
                id === null
            ) {

                toast.warning(
                    "Please select a Stock Item to edit."
                );

                return;

            }


            if (
                mode === MODE.NEW
            ) {

                toast.warning(
                    "Please finish the new Stock Item first."
                );

                return;

            }


            if (
                mode === MODE.EDIT
            ) {

                toast.info(
                    "Stock Item is already in edit mode."
                );

                return;

            }


            setPendingStockItemId(
                id
            );


            setPendingAction(
                PASSWORD_ACTION.EDIT
            );


            setShowPasswordModal(
                true
            );

        },
        [
            selectedId,
            mode,
            normalizeId
        ]
    );


    //==========================================================
    // PERFORM EDIT
    //==========================================================

    const performEdit = useCallback(
        async (stockItemId) => {

            const id =
                normalizeId(
                    stockItemId
                );


            if (
                id === null
            ) {

                toast.error(
                    "Invalid Stock Item selection."
                );

                return;

            }


            let stockItem =
                findStockItem(id);


            //==================================================
            // If not locally available,
            // load it from server.
            //==================================================

            if (!stockItem) {

                try {

                    const response =
                        await stockItemService.getById(
                            id
                        );


                    stockItem =
                        extractApiData(
                            response
                        );

                }
                catch (error) {

                    toast.error(
                        getErrorMessage(
                            error,
                            "Unable to load Stock Item for editing."
                        )
                    );

                    return;

                }

            }


            if (
                !stockItem?.id
            ) {

                toast.error(
                    "Stock Item not found."
                );

                return;

            }


            setSelectedId(
                id
            );


            setStockItemToForm(
                stockItem
            );


            setMode(
                MODE.EDIT
            );


            toast.success(
                "Edit mode enabled."
            );

        },
        [
            normalizeId,
            findStockItem,
            extractApiData,
            setStockItemToForm
        ]
    );


    //==========================================================
    // UPDATE
    //
    // UPDATE
    // ↓
    // RELOAD SERVER DATA
    // ↓
    // FIND UPDATED RECORD
    // ↓
    // DISPLAY
    // ↓
    // VIEW MODE
    //==========================================================

    const handleUpdate = useCallback(
        async () => {

            if (
                mode !== MODE.EDIT
            ) {

                toast.warning(
                    "Stock Item is not in edit mode."
                );

                return;

            }


            const id =
                normalizeId(
                    form.id
                );


            if (
                id === null
            ) {

                toast.warning(
                    "Invalid Stock Item Id."
                );

                return;

            }


            const stockCode =
                form.stockCode.trim();

            const stockName =
                form.stockName.trim();

            const taxRate =
                Number(form.taxRate);


            //==================================================
            // VALIDATION
            //==================================================

            if (!stockCode) {

                toast.warning(
                    "Stock Code is required."
                );

                return;

            }


            if (!stockName) {

                toast.warning(
                    "Stock Name is required."
                );

                return;

            }


            if (
                Number.isNaN(taxRate) ||
                taxRate < 0
            ) {

                toast.warning(
                    "Tax Rate cannot be negative."
                );

                return;

            }


            try {

                setLoading(true);


                //================================================
                // UPDATE
                //================================================

                await stockItemService.update({

                    id,

                    stockCode,

                    stockName,

                    taxRate,

                    isActive:
                        form.isActive

                });


                //================================================
                // RELOAD FROM SERVER
                //
                // IMPORTANT:
                // Do not depend on PUT response.
                //================================================

                const listResponse =
                    await stockItemService.getAll();


                const data =
                    extractApiData(
                        listResponse
                    );


                const refreshedItems =
                    Array.isArray(data)
                        ? data
                        : [];


                setStockItems(
                    refreshedItems
                );


                //================================================
                // FIND UPDATED RECORD
                //================================================

                const updatedStockItem =
                    refreshedItems.find(
                        item =>
                            normalizeId(
                                item?.id
                            ) === id
                    );


                //================================================
                // UPDATED RECORD FOUND
                //================================================

                if (
                    updatedStockItem
                ) {

                    setSelectedId(
                        id
                    );


                    setStockItemToForm(
                        updatedStockItem
                    );


                    //================================================
                    // AUTOMATIC VIEW MODE
                    //================================================

                    setMode(
                        MODE.VIEW
                    );


                    toast.success(
                        "Stock Item updated successfully."
                    );

                    return;

                }


                //================================================
                // UPDATE SUCCESS BUT RECORD NOT FOUND
                //================================================

                toast.success(
                    "Stock Item updated successfully."
                );


                setMode(
                    MODE.INITIAL
                );

            }
            catch (error) {

                console.error(
                    "Stock Item Update Error:",
                    error
                );


                toast.error(
                    getErrorMessage(
                        error,
                        "Unable to update Stock Item."
                    )
                );

            }
            finally {

                setLoading(false);

            }

        },
        [
            mode,
            form,
            normalizeId,
            extractApiData,
            setStockItemToForm
        ]
    );


    //==========================================================
    // DELETE REQUEST
    //
    // Grid selection
    // ↓
    // Delete
    // ↓
    // Password
    // ↓
    // Confirmation
    // ↓
    // Delete
    //==========================================================

    const handleDelete = useCallback(
        () => {

            const id =
                normalizeId(
                    selectedId
                );


            if (
                id === null
            ) {

                toast.warning(
                    "Please select a Stock Item to delete."
                );

                return;

            }


            if (
                mode === MODE.NEW
            ) {

                toast.warning(
                    "Please finish the new Stock Item first."
                );

                return;

            }


            if (
                mode === MODE.EDIT
            ) {

                toast.warning(
                    "Please update the current Stock Item first."
                );

                return;

            }


            setPendingStockItemId(
                id
            );


            setPendingAction(
                PASSWORD_ACTION.DELETE
            );


            setShowPasswordModal(
                true
            );

        },
        [
            selectedId,
            mode,
            normalizeId
        ]
    );


    //==========================================================
    // PERFORM DELETE
    //==========================================================

    const performDelete = useCallback(
        async (stockItemId) => {

            const id =
                normalizeId(
                    stockItemId
                );


            if (
                id === null
            ) {

                toast.error(
                    "Invalid Stock Item selection."
                );

                return;

            }


            const stockItem =
                findStockItem(id);


            const stockItemName =
                stockItem?.stockName ||
                stockItem?.stockCode ||
                "this Stock Item";


            //==================================================
            // CONFIRMATION
            //==================================================

            const confirmed =
                window.confirm(
                    `Are you sure you want to delete "${stockItemName}"?`
                );


            if (!confirmed) {

                return;

            }


            try {

                setLoading(true);


                //================================================
                // DELETE API
                //================================================

                await stockItemService.delete(
                    id
                );


                //================================================
                // REMOVE FROM GRID
                //================================================

                setStockItems(
                    previous =>
                        previous.filter(
                            item =>
                                normalizeId(
                                    item?.id
                                ) !== id
                        )
                );


                //================================================
                // CLEAR FORM
                //================================================

                setForm({
                    ...INITIAL_FORM
                });


                //================================================
                // CLEAR SELECTION
                //================================================

                setSelectedId(
                    null
                );


                //================================================
                // RETURN INITIAL MODE
                //================================================

                setMode(
                    MODE.INITIAL
                );


                toast.success(
                    "Stock Item deleted successfully."
                );

            }
            catch (error) {

                console.error(
                    "Stock Item Delete Error:",
                    error
                );


                toast.error(
                    getErrorMessage(
                        error,
                        "Unable to delete Stock Item."
                    )
                );

            }
            finally {

                setLoading(false);

            }

        },
        [
            normalizeId,
            findStockItem
        ]
    );


    //==========================================================
    // VERIFY EDIT PASSWORD
    //
    // SAME EDIT PASSWORD:
    //
    // EDIT
    // DELETE
    //==========================================================

    const handleVerifyPassword = useCallback(
        async (password) => {

            if (
                !password?.trim()
            ) {

                toast.warning(
                    "Please enter Edit Password."
                );

                return;

            }


            const id =
                normalizeId(
                    pendingStockItemId
                );


            if (
                id === null
            ) {

                toast.error(
                    "No Stock Item is selected."
                );

                return;

            }


            const action =
                pendingAction;


            try {

                setLoading(true);


                //================================================
                // VERIFY PASSWORD
                //================================================

                const response =
                    await verifyEditPassword(
                        password
                    );


                if (
                    !isApiSuccess(
                        response
                    )
                ) {

                    const data =
                        getResponseData(
                            response
                        );


                    toast.error(
                        data?.message ||
                        data?.Message ||
                        "Invalid Edit Password."
                    );

                    return;

                }


                //================================================
                // CLOSE MODAL
                //================================================

                setShowPasswordModal(
                    false
                );


                setPendingAction(
                    null
                );


                setPendingStockItemId(
                    null
                );


                //================================================
                // EDIT
                //================================================

                if (
                    action ===
                    PASSWORD_ACTION.EDIT
                ) {

                    await performEdit(
                        id
                    );

                    return;

                }


                //================================================
                // DELETE
                //================================================

                if (
                    action ===
                    PASSWORD_ACTION.DELETE
                ) {

                    await performDelete(
                        id
                    );

                    return;

                }


                toast.error(
                    "Invalid authorization action."
                );

            }
            catch (error) {

                console.error(
                    "Stock Item Password Error:",
                    error
                );


                toast.error(
                    getErrorMessage(
                        error,
                        "Invalid Edit Password."
                    )
                );

            }
            finally {

                setLoading(false);

            }

        },
        [
            pendingStockItemId,
            pendingAction,
            normalizeId,
            isApiSuccess,
            performEdit,
            performDelete
        ]
    );


    //==========================================================
    // CLOSE PASSWORD MODAL
    //==========================================================

    const handleClosePasswordDialog =
        useCallback(
            () => {

                setShowPasswordModal(
                    false
                );

                setPendingAction(
                    null
                );

                setPendingStockItemId(
                    null
                );

            },
            []
        );


    //==========================================================
    // GRID ROW SELECTION
    //
    // Selecting a row:
    //
    // DOES:
    // - select row
    //
    // DOES NOT:
    // - display form
    // - call API
    // - enter view mode
    // - open password
    //
    // This allows:
    //
    // Select row → Edit
    //
    // or
    //
    // Select row → Delete
    //==========================================================

    const handleSelectedId =
        useCallback(
            (value) => {

                if (
                    mode === MODE.NEW ||
                    mode === MODE.EDIT
                ) {

                    return;

                }


                let id =
                    value;


                //================================================
                // Supports:
                //
                // 100
                // "100"
                // { id: 100 }
                // { Id: 100 }
                //================================================

                if (
                    value &&
                    typeof value === "object"
                ) {

                    id =
                        value.id ??
                        value.Id;

                }


                const normalizedId =
                    normalizeId(id);


                if (
                    normalizedId === null
                ) {

                    return;

                }


                setSelectedId(
                    normalizedId
                );

            },
            [
                mode,
                normalizeId
            ]
        );


    //==========================================================
    // SEARCH
    //==========================================================

    const filteredStockItems =
        useMemo(
            () => {

                const search =
                    searchText
                        .trim()
                        .toLowerCase();


                if (!search) {

                    return stockItems;

                }


                return stockItems.filter(
                    item => {

                        let value = "";


                        switch (
                            searchBy
                        ) {

                            case "stockCode":

                                value =
                                    item.stockCode;

                                break;


                            case "stockName":

                                value =
                                    item.stockName;

                                break;


                            case "taxRate":

                                value =
                                    item.taxRate;

                                break;


                            case "status":

                                value =
                                    item.isActive
                                        ? "active"
                                        : "inactive";

                                break;


                            case "createdBy":

                                value =
                                    item.createdBy;

                                break;


                            case "createdOn":

                                value =
                                    item.createdOn
                                        ? new Date(
                                            item.createdOn
                                        ).toLocaleDateString()
                                        : "";

                                break;


                            default:

                                value =
                                    `${item.stockCode || ""} ${item.stockName || ""}`;

                                break;

                        }


                        return String(
                            value ?? ""
                        )
                            .toLowerCase()
                            .includes(search);

                    }
                );

            },
            [
                stockItems,
                searchText,
                searchBy
            ]
        );


    //==========================================================
    // RESET PAGE WHEN SEARCH CHANGES
    //==========================================================

    useEffect(
        () => {

            setCurrentPage(1);

        },
        [
            searchText,
            searchBy
        ]
    );


    //==========================================================
    // PAGINATION
    //==========================================================

    const totalRecords =
        filteredStockItems.length;


    const totalPages =
        Math.ceil(
            totalRecords /
            rowsPerPage
        );


    const currentRows =
        useMemo(
            () => {

                const start =
                    (currentPage - 1) *
                    rowsPerPage;


                return filteredStockItems.slice(
                    start,
                    start + rowsPerPage
                );

            },
            [
                filteredStockItems,
                currentPage,
                rowsPerPage
            ]
        );


    //==========================================================
    // PAGE VALIDATION
    //==========================================================

    useEffect(
        () => {

            if (
                totalPages > 0 &&
                currentPage > totalPages
            ) {

                setCurrentPage(
                    totalPages
                );

            }


            if (
                totalPages === 0 &&
                currentPage !== 1
            ) {

                setCurrentPage(
                    1
                );

            }

        },
        [
            currentPage,
            totalPages
        ]
    );


    //==========================================================
    // ROWS PER PAGE
    //==========================================================

    const handleRowsPerPageChange =
        useCallback(
            (value) => {

                const number =
                    Number(value);


                setRowsPerPage(
                    number > 0
                        ? number
                        : 20
                );


                setCurrentPage(1);

            },
            []
        );


    //==========================================================
    // PRINT
    //==========================================================

    const handlePrint =
        useCallback(
            () => {

                if (
                    !selectedId
                ) {

                    toast.warning(
                        "Please select a Stock Item first."
                    );

                    return;

                }


                window.print();

            },
            [
                selectedId
            ]
        );


    //==========================================================
    // RENDER
    //==========================================================

    return (

        <div
            className={
                loading
                    ? "stock-item-entry loading"
                    : "stock-item-entry"
            }
        >

            {/*==================================================
                PASSWORD MODAL
            ==================================================*/}

            <EditPasswordModal

                show={
                    showPasswordModal
                }

                title={
                    pendingAction ===
                    PASSWORD_ACTION.DELETE

                        ? "Delete Authorization"

                        : "Edit Authorization"
                }

                message={
                    pendingAction ===
                    PASSWORD_ACTION.DELETE

                        ? "Please enter Edit Password to delete this Stock Item."

                        : "Please enter Edit Password to edit this Stock Item."
                }

                onVerify={
                    handleVerifyPassword
                }

                onClose={
                    handleClosePasswordDialog
                }

            />


            {/*==================================================
                HEADER
            ==================================================*/}

            <StockItemHeader

                form={
                    form
                }

                setForm={
                    setForm
                }

                mode={
                    mode
                }

            />


            {/*==================================================
                ACTION BAR
            ==================================================*/}

            <StockItemActionBar

                mode={
                    mode
                }

                selectedId={
                    selectedId
                }

                onNew={
                    handleNew
                }

                onSave={
                    handleSave
                }

                onView={
                    handleView
                }

                onEdit={
                    handleEdit
                }

                onUpdate={
                    handleUpdate
                }

                onDelete={
                    handleDelete
                }

                onPrint={
                    handlePrint
                }

            />


            {/*==================================================
                TABLE
            ==================================================*/}

            <StockItemTable

                stockItems={
                    currentRows
                }

                selectedId={
                    selectedId
                }

                setSelectedId={
                    handleSelectedId
                }

                searchText={
                    searchText
                }

                setSearchText={
                    setSearchText
                }

                searchBy={
                    searchBy
                }

                setSearchBy={
                    setSearchBy
                }

                mode={
                    mode
                }

            />


            {/*==================================================
                PAGINATION
            ==================================================*/}

            <StockItemPagination

                totalRecords={
                    totalRecords
                }

                currentPage={
                    currentPage
                }

                rowsPerPage={
                    rowsPerPage
                }

                totalPages={
                    totalPages
                }

                onPageChange={
                    setCurrentPage
                }

                onRowsPerPageChange={
                    handleRowsPerPageChange
                }

            />


            {/*==================================================
                LOADING
            ==================================================*/}

            {
                loading && (

                    <div className="loading-overlay">

                        <div className="loading-box">

                            <div className="spinner" />

                            <span>
                                Processing...
                            </span>

                        </div>

                    </div>

                )
            }

        </div>

    );

}


//==========================================================
// RESPONSE DATA
//==========================================================

function getResponseData(
    response
) {

    let value =
        response;


    //==========================================================
    // Axios response
    //==========================================================

    if (
        value &&
        value.data !== undefined &&
        (
            value.status !== undefined ||
            value.config !== undefined ||
            value.headers !== undefined
        )
    ) {

        value =
            value.data;

    }


    return value;

}


//==========================================================
// USER-FRIENDLY ERROR MESSAGE
//==========================================================

function getErrorMessage(
    error,
    fallback
) {

    const data =
        error?.response?.data;


    //==========================================================
    // API OBJECT
    //==========================================================

    if (
        data &&
        typeof data === "object"
    ) {

        if (
            data.message
        ) {

            return data.message;

        }


        if (
            data.Message
        ) {

            return data.Message;

        }


        if (
            data.errors
        ) {

            const messages = [];


            Object.values(
                data.errors
            ).forEach(
                value => {

                    if (
                        Array.isArray(value)
                    ) {

                        messages.push(
                            ...value
                        );

                    }

                }
            );


            if (
                messages.length
            ) {

                return messages.join(
                    "\n"
                );

            }

        }

    }


    //==========================================================
    // STRING RESPONSE
    //==========================================================

    if (
        typeof data === "string"
    ) {

        const text =
            data.trim();


        if (!text) {

            return fallback;

        }


        //======================================================
        // Known business errors
        //======================================================

        if (
            text.includes(
                "Stock Code already exists"
            )
        ) {

            return "Stock Code already exists.";

        }


        if (
            text.includes(
                "Stock Item not found"
            )
        ) {

            return "Stock Item not found.";

        }


        if (
            text.includes(
                "Company is not assigned"
            )
        ) {

            return "Company is not assigned.";

        }


        if (
            text.includes(
                "Invalid Edit Password"
            )
        ) {

            return "Invalid Edit Password.";

        }


        //======================================================
        // Technical errors
        //======================================================

        const technicalErrors = [

            "Microsoft.EntityFrameworkCore",

            "Microsoft.Data.SqlClient",

            "SqlException",

            "DbUpdateException",

            "IX_StockMaster",

            "Cannot insert duplicate key",

            "Cannot insert duplicate",

            "at INVENTORYAPP."

        ];


        if (
            technicalErrors.some(
                item =>
                    text.includes(item)
            )
        ) {

            return fallback;

        }


        return text;

    }


    //==========================================================
    // AXIOS ERROR
    //==========================================================

    if (
        error?.message &&
        !error.message.includes(
            "Request failed with status code"
        )
    ) {

        return error.message;

    }


    return fallback;

}